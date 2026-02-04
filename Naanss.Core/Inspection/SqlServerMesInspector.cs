using Microsoft.Data.SqlClient;
using Naanss.Core.Contracts;
using Naanss.Core.Models;

namespace Naanss.Core.Inspection
{
    public class SqlServerMesInspector : IMesInspector
    {
        public MesDbProfile Inspect(string connectionString)
        {
            var profile = new MesDbProfile();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            profile.DatabaseName = connection.Database;

            profile.Tables = ReadTables(connection);
            PopulateColumns(connection, profile);
            PopulateIndexes(connection, profile);

            return profile;
        }

        private List<MesTable> ReadTables(SqlConnection connection)
        {
            var tables = new List<MesTable>();

            var cmd = new SqlCommand(@"
                SELECT 
                    t.name AS TableName,
                    SUM(p.rows) AS TotalRows
                FROM sys.tables t
                JOIN sys.partitions p 
                    ON t.object_id = p.object_id
                WHERE p.index_id IN (0,1)
                GROUP BY t.name
            ", connection);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(new MesTable
                {
                    Name = reader.GetString(0),
                    RowCount = reader.GetInt64(1)
                });
            }

            return tables;
        }

        private void PopulateColumns(SqlConnection connection, MesDbProfile profile)
        {
            var cmd = new SqlCommand(@"
                SELECT t.name AS TableName,
                       c.name AS ColumnName,
                       ty.name AS DataType,
                       c.is_nullable,
                       ISNULL(i.is_primary_key, 0) AS IsPrimaryKey
                FROM sys.tables t
                JOIN sys.columns c ON t.object_id = c.object_id
                JOIN sys.types ty ON c.user_type_id = ty.user_type_id
                LEFT JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                LEFT JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
            ", connection);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var table = profile.Tables.First(t => t.Name == reader.GetString(0));

                table.Columns.Add(new MesColumn
                {
                    Name = reader.GetString(1),
                    DataType = reader.GetString(2),
                    IsNullable = reader.GetBoolean(3),
                    IsPrimaryKey = reader.GetBoolean(4)
                });
            }
        }

        private void PopulateIndexes(SqlConnection connection, MesDbProfile profile)
        {
            var cmd = new SqlCommand(@"
                SELECT 
                    t.name AS TableName,
                    i.name AS IndexName,
                    i.index_id,
                    i.is_unique,
                    c.name AS ColumnName
                FROM sys.tables t
                JOIN sys.indexes i 
                    ON t.object_id = i.object_id
                JOIN sys.index_columns ic 
                    ON i.object_id = ic.object_id 
                AND i.index_id = ic.index_id
                JOIN sys.columns c 
                    ON ic.object_id = c.object_id 
                AND ic.column_id = c.column_id
                WHERE i.name IS NOT NULL
                ORDER BY t.name, i.name, ic.key_ordinal
            ", connection);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var tableName = reader.GetString(0);
                var indexName = reader.GetString(1);
                var indexId = reader.GetInt32(2);
                var isUnique = reader.GetBoolean(3);
                var columnName = reader.GetString(4);

                var table = profile.Tables.First(t => t.Name == tableName);

                var index = table.Indexes.FirstOrDefault(i => i.Name == indexName);
                if (index == null)
                {
                    index = new MesIndex
                    {
                        Name = indexName,
                        IsClustered = indexId == 1,
                        IsUnique = isUnique
                    };
                    table.Indexes.Add(index);
                }

                index.Columns.Add(columnName);
            }
        }
    }
}

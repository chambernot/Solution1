using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Infra
{
    [SetUpFixture]
    public class SetupTestDatabase
    {
        [OneTimeSetUp]
        public void SetUpAmbienteAceitacao()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString;
            var catalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
            var connectionStringSetup = ConfigurationManager.ConnectionStrings["SQLServerConnectionSetup"].ConnectionString;
            const string dbName = @"Provisao.Db";

            var conn = new SqlConnection(connectionStringSetup);
            conn.Open();
            var sql = File.ReadAllText($@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\{dbName}_Create.sql")
                    .Replace(dbName, catalog)
                    .Replace("$(DefaultDataPath)", $@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}")
                    .Replace("$(DefaultLogPath", $@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}")
                    .Replace("$(DefaultFilePrefix)", catalog)
                    .Replace("$(DatabaseName)", catalog)
                    .Replace("WITH (DATA_COMPRESSION = PAGE)", string.Empty)
                    .Replace("SET NOEXEC ON", string.Empty)
                    .Replace("GO\r\n", "|")
                ;

            ExecutaSql(conn, sql);

            //sql = File.ReadAllText($@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\Gecon_dominios.sql");

            //ExecutaSql(conn, sql);

            conn.Close();
        }

        private static void ExecutaSql(SqlConnection conn, string sql)
        {
            var command = conn.CreateCommand();

            foreach (var s in sql.Split('|'))
            {
                command.CommandText = s;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(s);
                    Console.WriteLine(e.Message);
                }
            }
        }

        [OneTimeTearDown]
        public void TearDownAmbienteAceitacao()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString;
            var catalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLServerConnectionSetup"].ConnectionString);
            conn.Open();

            var command = conn.CreateCommand();

            command.CommandText = $@"
USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('{catalog}')

EXEC(@kill);
";
            command.ExecuteNonQuery();

            command.CommandText = $"DROP DATABASE {catalog}";
            command.ExecuteNonQuery();

            conn.Close();
        }
    }
}
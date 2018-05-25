using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastrutura.Repository
{
    public class RepositoryBase: IDisposable
    {
        public IDbConnection ProvisaoConnection
        {
            get { return new SqlConnection(ConfigurationManager.ConnectionStrings["SQLServerConnection"].ConnectionString); }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

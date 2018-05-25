using Mongeral.Infrastructure.DataAccess;

namespace Mongeral.Provisao.V2.DAL
{
    public class PersistenceBase
    {
        protected DataAccessSessionManager Session { get; private set; }
        protected DataAccessCommand CreateCommand => Session.CreateCommand();
        protected PersistenceBase(DataAccessSessionManager session)
        {
            Session = session;
        }
    }
}

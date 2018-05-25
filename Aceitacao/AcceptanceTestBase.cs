using Mongeral.Infrastructure.UnitOfWork.Session;
using TechTalk.SpecFlow;
using Mongeral.Infrastructure.Ioc;

namespace Mongeral.Provisao.V2.Testes.Aceitacao
{
    [Binding]
    public class AcceptanceTestBase
    {
        private TransactionManager _transaction;        

        [BeforeScenario]
        protected void BeforeTestRun()
        {
            _transaction = InstanceFactory.Resolve<TransactionManager>();
            _transaction.Bind();
            _transaction.BeginTransaction();
        }

        [AfterScenario]
        protected void AfterStep()
        {
            _transaction.RollBack();
            //_transaction.Commit();
            _transaction.Unbind();
        }       
    }
}

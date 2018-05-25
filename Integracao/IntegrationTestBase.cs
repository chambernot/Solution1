using Mongeral.Infrastructure.UnitOfWork.Session;
using Ninject;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Assertion = Mongeral.Infrastructure.Assertions;

namespace Mongeral.Provisao.V2.Testes.Integracao
{
    public class IntegrationTestBase
    {
        private TransactionManager _transaction;
        private IKernel _kernel;

        [OneTimeSetUp]
        protected void FixtureSetUp()
        {
            _kernel = new StandardKernel(new IntegrationTestModule());
            _transaction = _kernel.Get<TransactionManager>();
            _transaction.Bind();
            _transaction.BeginTransaction();         
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            _transaction.RollBack();
            //_transaction.Commit();
            _transaction.Unbind();
        }

        protected T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

        protected IResolveConstraint GeraErro(string mensagemErro)
        {
            return Throws.TypeOf(typeof(Assertion.AssertionException))
                .And.Message.Contain(mensagemErro);
        }

        protected IResolveConstraint GeraErro(string format, params object[] args)
        {
            return GeraErro(string.Format(format, args));
        }
    }
}

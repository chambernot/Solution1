using NUnit.Framework.Constraints;
using NUnit.Framework;
using Assertion = Mongeral.Infrastructure.Assertions;
using Ninject;
using System;
using Ninject.MockingKernel.Moq;

namespace Mongeral.Provisao.V2.Testes.Unitarios
{
    public class UnitTesBase
    {
        private IKernel _kernel;
        protected MoqMockingKernel MockingKernel { get; private set; }

        [OneTimeSetUp]
        protected void FixtureOneTimeSetUp()
        {
            _kernel = new StandardKernel(new UnitTestModule());

            MockingKernel = new MoqMockingKernel();
        }

        [OneTimeTearDown]
        protected void FixtureTearDown()
        {
            MockingKernel.Reset();
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

        public int CalcularMeses(DateTime dataFinal, DateTime dataInicial)
        {
            int result;
            if (dataInicial >= dataFinal)
            {
                result = 0;
            }
            else
            {
                int num = dataFinal.Year - dataInicial.Year;
                int num2 = dataFinal.Month - dataInicial.Month;
                if (dataFinal.Day < dataInicial.Day)
                {
                    num2--;
                }
                result = num * 12 + num2;
            }
            return result;
        }
    }
}
using Mongeral.Provisao.V2.Testes.Infra;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Integracao
{
    [SetUpFixture]
    public class SetupDbAceitacao : SetupTestDatabase
    {
        [OneTimeSetUp]
        public void SeupDb()
        {
            base.SetUpAmbienteAceitacao();
        }
    }
}
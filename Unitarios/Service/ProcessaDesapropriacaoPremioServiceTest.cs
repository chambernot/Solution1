using NUnit.Framework;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Ninject;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaDesapropriacaoPremioServiceTest: UnitTesBase
    {
        private IParcelaDesapropriada _parcelaDesapropriada;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _parcelaDesapropriada = ParcelaDesapropriadaBuilder.UmBuilder().Padrao().Build();
        }

        [Test]
        public async Task AoDesapropriarUmPremioDeveSerProcessado()
        {
            //MockingKernel.GetMock<IPipe<DesapropriacaoPremioContext>>()
            //    .Setup(x => x.Send(It.IsAny<DesapropriacaoPremioContext>())).Returns(Task.FromResult(_parcelaDesapropriada));
            
            await MockingKernel.Get<DesapropriacaoPremioService>().Execute(_parcelaDesapropriada);

            MockingKernel.MockRepository.VerifyAll();
        }
    }
}

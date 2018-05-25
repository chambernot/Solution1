using GreenPipes;
using Mongeral.Integrador.Contratos.Premio;
using Ninject;
using NUnit.Framework;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Moq;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaApropriacaoPremioServiceTest: UnitTesBase
    {        
        private IParcelaApropriada _apropriacaoPremio;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _apropriacaoPremio = ParcelaApropriadaBuilder.UmBuilder().Padrao()
                .Com(ApropriacaoBuilder.UmBuilder()
                    .Com(PagamentoBuilder.UmBuilder().Padrao()))
                .Build();
        }

        [Test]
        public async Task AoApropriarumPremioDeveSerProcessado()
        {
            //MockingKernel.GetMock<IPipe<ApropriacaoPremioContext>>()
            //    .Setup(x => x.Send(It.IsAny<ApropriacaoPremioContext>())).Returns(Task.FromResult(_apropriacaoPremio));

            await MockingKernel.Get<ApropriacaoPremioService>().Execute(_apropriacaoPremio);

            //MockingKernel.GetMock<IPipe<ApropriacaoPremioContext>>().Verify();
        }
    }
}

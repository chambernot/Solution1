using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using Moq;
using Ninject;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaPortabilidadeServiceTest: UnitTesBase
    {   
        [Test]
        public async Task DadoUmaPortabilidadeDeveRetornarOContrato()
        {
            var portabilidadePremio = PortabilidadeApropriadaBuilder.UmBuilder().Padrao().Build();

            //MockingKernel.GetMock<IPipe<PortabilidadePremioContext>>()
            //    .Setup(x => x.Send(It.IsAny<PortabilidadePremioContext>()))
            //    .Returns(Task.FromResult(portabilidadePremio)).Verifiable();

            var contrato = await MockingKernel.Get<PortabilidadePremioService>().Execute(portabilidadePremio);

            Assert.That(contrato.Identificador, Is.EqualTo(portabilidadePremio.Identificador));
        }
    }
}

using System.Threading.Tasks;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Ninject;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    public class ProcessaAporteServiceTest: UnitTesBase
    {   
        [Test]
        public async Task DadoUmAporteDeveRetornarUmContrato()
        {
            var aporteApropriado = AporteApropriadoBuilder.UmBuilder().Padrao().Build();

            //MockingKernel.GetMock<IPipe<AportePremioContext>>()
            //    .Setup(x => x.Send(It.IsAny<AportePremioContext>()))
            //    .Returns(Task.FromResult(aporteApropriado)).Verifiable();

            var contrato = await MockingKernel.Get<AportePremioService>().Execute(aporteApropriado);

            Assert.That(contrato.Identificador, Is.EqualTo(aporteApropriado.Identificador));
        }
    }
}

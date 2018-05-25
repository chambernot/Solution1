using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Application.Filters;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Filters;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Testes.Infra.Fake.Filters;
using Ninject;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Application.Filters
{
    [TestFixture]
    public class AjustePremioFilterTest: UnitTesBase
    {
        [Test]
        public void DadoUmContextoDeveAdicionarDadosDeCobertura()
        {
            var coberturaDto = CoberturaContratadaBuilder.Uma().Build();

            var _ajustePremio = ParcelaAjustadaBuilder.UmBuilder().Padrao().Build();

            var lista = new List<Premio>
            {
                PremioEmitidoBuilder.Um().Padrao().Build()
            };

            var _contexto = new AjustePremioContext(_ajustePremio)
            {
                Premios = lista.AsEnumerable()
            };

            MockingKernel.GetMock<IPremios>()
                .Setup(x => x.VerificarUltimoPremio(It.IsAny<long>(), _contexto.Evento.MovimentosPermitidos, It.IsAny<int>())).Returns(Task.FromResult(true));

            MockingKernel.GetMock<ICoberturas>()
                .Setup(x => x.ObterPorItemCertificado(It.IsAny<long>())).Returns(Task.FromResult(coberturaDto));

            var pipeline = Pipe.New<AjustePremioContext>(cfg =>
            {
                cfg.AddFilter(() => MockingKernel.Get<AjustePremioFilter>());
                cfg.AddFilter(() => MockingKernel.Get<SalvarEventoFilterFake<AjustePremioContext, EventoAjustePremio>>());
            });

            pipeline.Send(_contexto);

            var dto = _contexto.Evento.Premios.First().Cobertura;

            Assert.That(dto.ItemCertificadoApoliceId, Is.EqualTo(coberturaDto.ItemCertificadoApoliceId));
        }
    }
}

using System;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades;
using Mongeral.Provisao.V2.Application;
using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder;
using Moq;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Application
{
    [TestFixture]
    public class ProcessaAlteracaoParametrosTest
    {
        private EventoAlteracao _eventoAlteracao;
        private Mock<IEventos<EventoAlteracao>> _eventos;
        private Mock<ICoberturas> _coberturas;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _eventoAlteracao = EventoAlteracaoBuilder.UmEvento()
                .ComDadosPadroes()
                .Build();
                        
            _eventos = new Mock<IEventos<EventoAlteracao>>();
            _eventos.Setup(x => x.Adicionar(It.IsAny<EventoAlteracao>())).Returns(Task.CompletedTask);

            _coberturas = new Mock<ICoberturas>();
            _coberturas.Setup(x => x.ObterPorItensCertificadosApolices(It.IsAny<IEnumerable<long>>()))
                .Returns(Task.FromResult<IEnumerable<CoberturaContratada>>(new List<CoberturaContratada>()
                {
                    new CoberturaContratada(IdentificadoresPadrao.ItemCertificadoApoliceId){Id = Guid.NewGuid()}
                }));
        }

        [Test]
        public async Task Quando_a_aplicacao_for_processada_deve_adicionar_o_evento()
        {
            var processarAlteracaoParametros = new ProcessaEventoAlteracaoParametros(_eventos.Object, _coberturas.Object);

            await processarAlteracaoParametros.Processar(_eventoAlteracao);

            _eventos.Verify(m => m.Adicionar(It.IsAny<EventoAlteracao>()));
        }     
    }
}

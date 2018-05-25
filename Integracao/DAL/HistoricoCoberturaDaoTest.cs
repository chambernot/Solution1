using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    [TestFixture]
    public class HistoricoCoberturaDaoTest: IntegrationTestBase
    {
        private IEventosBase<EventoImplantacao> _eventos;
        private ICoberturas _coberturas;
        private EventoImplantacao _eventoImplantacao;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _eventoImplantacao = EventoImplantacaoBuilder.UmEvento(Guid.NewGuid())
                .Com(CoberturaContratadaBuilder.Uma()
                    .Com(HistoricoCoberturaContratadaBuilder.UmHistorico().ComDadosPadroes())
                    .Com(DadosProdutoBuilder.Um()).Build())
                .Build();

            _eventos = GetInstance<IEventosBase<EventoImplantacao>>();
            _coberturas = GetInstance<ICoberturas>();
        }

        [Test]
        public async Task Dado_um_historico_de_implantacao_ao_salvar_deve_persistir_os_dados()
        {
            await _eventos.Salvar(_eventoImplantacao);

            var dadosCobertura = _eventoImplantacao.Coberturas.First();            

            var dto = await _coberturas.ObterPorItemCertificado(dadosCobertura.ItemCertificadoApoliceId);

            Assert.That(dto.Id, Is.EqualTo(dadosCobertura.Historico.CoberturaContratadaId));
            Assert.That(dto.Historico.DataNascimentoBeneficiario, Is.EqualTo(dadosCobertura.Historico.DataNascimentoBeneficiario));            
            Assert.That(dto.Historico.SexoBeneficiario, Is.EqualTo(dadosCobertura.Historico.SexoBeneficiario));
            Assert.That(dto.Historico.PeriodicidadeId, Is.EqualTo(dadosCobertura.Historico.PeriodicidadeId));
            Assert.That(dto.Historico.ValorBeneficio, Is.EqualTo(dadosCobertura.Historico.ValorBeneficio));
            Assert.That(dto.Historico.ValorCapital, Is.EqualTo(dadosCobertura.Historico.ValorCapital));
            Assert.That(dto.Historico.ValorContribuicao, Is.EqualTo(dadosCobertura.Historico.ValorContribuicao));
        }

        [Test]
        public async Task Dado_Um_Evento_O_Historico_Deve_Ser_Apagado()
        {
            await _eventos.Salvar(_eventoImplantacao);

            var dadosCobertura = _eventoImplantacao.Coberturas.First();

            var dto = await _coberturas.ObterPorItemCertificado(dadosCobertura.ItemCertificadoApoliceId);            

            Assert.NotNull(dto, "O Histórico de Cobertura não foi cadastrado.");

            await _eventos.Compensate(_eventoImplantacao.Identificador);

            dto = await _coberturas.ObterPorItemCertificado(dadosCobertura.ItemCertificadoApoliceId);            
            
            Assert.Null(dto, "O Histórico de Cobertura não foi excluído.");
        }      
    }
}

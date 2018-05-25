using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    public class PremioApropriadoDaoTest: PremioBaseTest
    {
        private EventoApropriacaoPremio _eventoApropriacao;
        private IEventosBase<EventoApropriacaoPremio> _eventos;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _eventos = GetInstance<IEventosBase<EventoApropriacaoPremio>>();
        }

        [Test]
        public async Task DadoUmEventoApropriacaoPremioDevePersistirDadosdoEvento()
        {
            var parcela = 1;
            _eventoApropriacao = ObterEventoApropriado(TipoRegimeFinanceiroEnum.Capitalizacao, parcela);

            _eventos.Salvar(_eventoApropriacao).Wait();

            var existe = await _eventos.ExisteEvento(_identificador);
            Assert.That(existe, Is.EqualTo(true));
            Assert.That(_eventoEmissao.Id, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public async Task DadoUmEventoApropriacaoPremioDevePersistirDadosDoPremio()
        {
            var parcela = 10;
            _eventoApropriacao = ObterEventoApropriado(TipoRegimeFinanceiroEnum.Capitalizacao, parcela);

            _eventos.Salvar(_eventoApropriacao).Wait();

            var premio = _eventoApropriacao.Premios.First();
            _premios = GetInstance<IPremios>();

            var dto = await _premios.ObterPorItemCertificado<PagamentoPremio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Apropriacao, parcela);

            Assert.That(dto.DataPagamento, Is.EqualTo(premio.Pagamento.DataPagamento));
            Assert.That(dto.DataApropriacao, Is.EqualTo(premio.Pagamento.DataApropriacao));
            Assert.That(dto.ValorPago, Is.EqualTo(premio.Pagamento.ValorPago));
            Assert.That(dto.Desconto, Is.EqualTo(premio.Pagamento.Desconto));
            Assert.That(dto.Multa, Is.EqualTo(premio.Pagamento.Multa));
            Assert.That(dto.IOFARecolher, Is.EqualTo(premio.Pagamento.IOFARecolher));
            Assert.That(dto.IOFRetido, Is.EqualTo(premio.Pagamento.IOFRetido));
            Assert.That(dto.IdentificadorCredito, Is.EqualTo(premio.Pagamento.IdentificadorCredito));
        }

        [Test]
        public async Task DadoUmEventoDeApropriacaoFundoAcumuladoDevePersistirPremioeNãoDeveCriarProvisoes()
        {
            var parcela = 11;
            _eventoApropriacao = ObterEventoApropriado(TipoRegimeFinanceiroEnum.FundoAcumulacao, parcela);

            _eventos.Salvar(_eventoApropriacao).Wait();

            var premio = _eventoApropriacao.Premios.First();

            _premios = GetInstance<IPremios>();
            var dtoPremio = await _premios.ObterPorItemCertificado<Premio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Apropriacao, parcela);

            _provisoes = GetInstance<IProvisoes>();
            var dtoProvisao = await _provisoes.Obter(premio.Id, premio.Cobertura.Id, (short)TipoProvisaoEnum.PMBAC, (short)TipoMovimentoEnum.Apropriacao, parcela);
            
            Assert.That(dtoPremio, Is.Not.Null);
            Assert.That(dtoProvisao, Is.Null);
        }

        [Test]
        public async Task DadoUmEventoDeApropriacaoRSNãoDevePersistirDados()
        {
            var parcela = IdentificadoresPadrao.NumeroParcela;

            _eventoApropriacao = ObterEventoApropriado(TipoRegimeFinanceiroEnum.FundoAcumulacao, parcela);

            _eventos.Salvar(_eventoApropriacao).Wait();

            var premio = _eventoApropriacao.Premios.First();

            _premios = GetInstance<IPremios>();
            var dtoPremio = await _premios.ObterPorItemCertificado<Premio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Apropriacao, parcela);

            _provisoes = GetInstance<IProvisoes>();
            var dtoProvisao = await _provisoes.Obter(premio.Id, premio.Cobertura.Id, (short)TipoProvisaoEnum.PMBAC, (short)TipoMovimentoEnum.Apropriacao, parcela);

            Assert.That(dtoPremio, Is.Not.Null);
            Assert.That(dtoProvisao, Is.Null);
        }

        private EventoApropriacaoPremio ObterEventoApropriado(TipoRegimeFinanceiroEnum regimeFinanceiro, int parcela)
        {
            return EventoApropriacaoPremioBuilder.UmEvento().ComIdentificador(_identificador).Padrao()
                .Com(PremioBuilder.Um().Padrao().ComNumeroParcela(parcela)
                    .Com(CoberturaContratadaBuilder.Uma()
                        .ComRegimeFinanceiro((short)regimeFinanceiro)
                        .ComTiposProvisao(TipoProvisaoEnum.PMBAC, TipoProvisaoEnum.PEF)
                        .ComId(_coberturaCadastrada.Id)
                        .Com(HistoricoCoberturaContratadaBuilder.UmHistorico().ComDadosPadroes().ComId(_historicoId))
                        )
                    .Com(MovimentoProvisaoBuilder.UmBuilder().Padrao()
                        .Com(ProvisaoCoberturaBuilder.UmBuilder())
                        )
                    .Com(PagamentoPremioBuilder.Um().Padrao())
                    )
                .Build();
        }
    }
}

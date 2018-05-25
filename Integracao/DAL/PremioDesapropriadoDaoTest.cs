using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    public class PremioDesapropriadoDaoTest : PremioBaseTest
    {
        private EventoDesapropriacaoPremio _eventoDesapropriacao;
        private IEventosBase<EventoDesapropriacaoPremio> _eventos;

        private Guid _identificadorDesapropriacao = Guid.NewGuid();

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _eventos = GetInstance<IEventosBase<EventoDesapropriacaoPremio>>();
        }

        [Test]
        public async Task DadoUmEventoDesapropriacaoDevePersistirDadosDoEvento()
        {
            var parcela = IdentificadoresPadrao.NumeroParcela;
            _eventoDesapropriacao = ObterEventoDesapropriado(TipoRegimeFinanceiroEnum.Capitalizacao, parcela);

            _eventos.Salvar(_eventoDesapropriacao).Wait();
            
            var existe = await _eventos.ExisteEvento(_identificadorDesapropriacao);
            Assert.That(existe, Is.EqualTo(true));
            Assert.That(_eventoEmissao.Id, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public async Task DadoUmEventoDesapropriacaoDevePersistirUmPremioComMovimentoDesapropriacao()
        {
            var parcela = 10;
            _eventoDesapropriacao = ObterEventoDesapropriado(TipoRegimeFinanceiroEnum.Capitalizacao, parcela);

            _eventos.Salvar(_eventoDesapropriacao).Wait();

            var premio = _eventoDesapropriacao.Premios.First();
            _premios = GetInstance<IPremios>();

            var dto = await _premios.ObterPorItemCertificado<Premio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Desapropriacao, parcela);

            Assert.That(dto.TipoMovimentoId, Is.EqualTo((short)TipoMovimentoEnum.Desapropriacao));
        }

        [Test]
        public async Task DadoUmEventoDeApropriacaoFundoAcumuladoDevePersistirPremioeNãoDeveCriarProvisoes()
        {
            var parcela = 11;
            _eventoDesapropriacao = ObterEventoDesapropriado(TipoRegimeFinanceiroEnum.FundoAcumulacao, parcela);

            _eventos.Salvar(_eventoDesapropriacao).Wait();

            var premio = _eventoDesapropriacao.Premios.First();

            _premios = GetInstance<IPremios>();
            var dtoPremio = await _premios.ObterPorItemCertificado<Premio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Desapropriacao, parcela);

            _provisoes = GetInstance<IProvisoes>();
            var dtoProvisao = await _provisoes.Obter(premio.Id, premio.Cobertura.Id, (short)TipoProvisaoEnum.PMBAC, (short)TipoMovimentoEnum.Desapropriacao, parcela);

            Assert.That(dtoPremio, Is.Not.Null);
            Assert.That(dtoProvisao, Is.Null);
        }

        [Test]
        public async Task AoPersistirUmPremioAoCompensarDeveSerExcluido()
        {
            DadoUmEventoDesapropriacaoDevePersistirDadosDoEvento().Wait();

            await _eventos.Compensate(_identificadorDesapropriacao);

            var existe = await _eventos.ExisteEvento(_identificadorDesapropriacao);

            Assert.That(existe, Is.EqualTo(false));            
        }

        private EventoDesapropriacaoPremio ObterEventoDesapropriado(TipoRegimeFinanceiroEnum regimeFinanceiro, int parcela)
        {
            return EventoDesParcelaApropriadaBuilder.UmBuilder().Padrao().ComIdentificador(_identificadorDesapropriacao)
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

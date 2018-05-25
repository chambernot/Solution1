using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Linq;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    public class PremioBaseTest : CoberturaContratadaBaseTest
    {
        protected EventoEmissaoPremio _eventoEmissao;
        protected IEventosBase<EventoEmissaoPremio> _eventos;

        protected IPremios _premios;
        protected IProvisoes _provisoes;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _eventos = GetInstance<IEventosBase<EventoEmissaoPremio>>();

            _eventoEmissao = EventoParcelaEmitidaBuilder
                .UmEventoComDataExecucao(_identificador, IdentificadoresPadrao.Competencia)
                .Padrao()
                .Com(PremioBuilder.Um().Padrao()
                    .Com(CoberturaContratadaBuilder.Uma()
                        .ComRegimeFinanceiro((short)TipoRegimeFinanceiroEnum.Capitalizacao)
                        .ComTiposProvisao(TipoProvisaoEnum.PMBAC, TipoProvisaoEnum.PEF)
                        .ComId(_coberturaCadastrada.Id)
                        .Com(HistoricoCoberturaContratadaBuilder.UmHistorico().ComId(_historicoId).ComDadosPadroes()))
                    .Com(MovimentoProvisaoBuilder.UmBuilder().Padrao()
                        .Com(ProvisaoCoberturaBuilder.UmBuilder())))
                .Build();

            _eventos.Salvar(_eventoEmissao).Wait();
        }
    }

    public class PremioDaoTest : PremioBaseTest
    {
        [Test]
        public async Task DadoUmEventoDePremioDevePersistirDadosDoEvento()
        {
            var existe = await _eventos.ExisteEvento(_identificador);
            Assert.That(existe, Is.EqualTo(true));
            Assert.That(_eventoEmissao.Id, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public async Task DadoUmEventoDePremioDevePersistirosDadosDoPremio()
        {
            _premios = GetInstance<IPremios>();

            var premio = _eventoEmissao.Premios.First();

            var dto = await _premios.ObterPorItemCertificado<Premio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Emissao, 12);

            Assert.That(dto.Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(dto.Numero, Is.EqualTo(premio.Numero));
            Assert.That(dto.Competencia, Is.EqualTo(premio.Competencia));
            Assert.That(dto.InicioVigencia, Is.EqualTo(premio.InicioVigencia));
            Assert.That(dto.FimVigencia, Is.EqualTo(premio.FimVigencia));
            Assert.That(dto.ValorPremio, Is.EqualTo(premio.ValorPremio));
            Assert.That(dto.ValorCarregamento, Is.EqualTo(premio.ValorCarregamento));
            Assert.That(dto.ValorBeneficio, Is.EqualTo(premio.ValorBeneficio));
            Assert.That(dto.ValorCapitalSegurado, Is.EqualTo(premio.ValorCapitalSegurado));
        }

        [Test]
        public async Task DadoUmEventoEmissaoPremioDevePersistirDadosDoMovimentoProvisao()
        {
            _provisoes = GetInstance<IProvisoes>();

            var premio = _eventoEmissao.Premios.First();
            var provisao = premio.MovimentosProvisao.First();

            var dto = await _provisoes.Obter(premio.Id, premio.Cobertura.Id, (short)TipoProvisaoEnum.PMBAC, (short)TipoMovimentoEnum.Emissao, IdentificadoresPadrao.NumeroParcela);

            Assert.That(dto.DataMovimentacao, Is.EqualTo(provisao.DataMovimentacao));
            Assert.That(dto.Fator, Is.EqualTo(provisao.Fator));
            Assert.That(dto.ValorBeneficioCorrigido, Is.EqualTo(provisao.ValorBeneficioCorrigido));
            Assert.That(dto.ValorJuros, Is.EqualTo(provisao.ValorJuros));
            Assert.That(dto.ValorSobrevivencia, Is.EqualTo(provisao.ValorSobrevivencia));
            Assert.That(dto.ValorDesvio, Is.EqualTo(provisao.ValorDesvio));
            Assert.That(dto.ValorProvisao, Is.EqualTo(provisao.ValorProvisao));
        }
    }
}

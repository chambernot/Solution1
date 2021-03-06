﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    public class PremioAporteDaoTest: PremioBaseTest
    {
        private EventoAportePremio _eventoAporte;
        private IEventosBase<EventoPremio> _eventos;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _eventos = GetInstance<IEventosBase<EventoPremio>>();
        }

        [Test]
        public async Task DadoUmEventoAportePremioDevePersistirDadosDoEvento()
        {
            var parcela = 1;

            _eventoAporte = ObterEventoAporte(TipoRegimeFinanceiroEnum.Capitalizacao, parcela);

            _eventos.Salvar(_eventoAporte).Wait();

            var existe = await _eventos.ExisteEvento(_identificador);
            Assert.That(existe, Is.EqualTo(true));
            Assert.That(_eventoEmissao.Id, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public async Task DadoUmEventoAportePremioDevePersistirDadosDoPremio()
        {
            var parcela = 10;
            _eventoAporte = ObterEventoAporte(TipoRegimeFinanceiroEnum.FundoAcumulacao, parcela);

            _eventos.Salvar(_eventoAporte).Wait();

            var premio = _eventoAporte.Premios.First();
            _premios = GetInstance<IPremios>();

            var dto = await _premios.ObterPorItemCertificado<PagamentoPremio>(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Aporte, parcela);

            Assert.That(dto.DataPagamento, Is.EqualTo(premio.Pagamento.DataPagamento));
            Assert.That(dto.DataApropriacao, Is.EqualTo(premio.Pagamento.DataApropriacao));
            Assert.That(dto.ValorPago, Is.EqualTo(premio.Pagamento.ValorPago));
            Assert.That(dto.Desconto, Is.EqualTo(premio.Pagamento.Desconto));
            Assert.That(dto.Multa, Is.EqualTo(premio.Pagamento.Multa));
            Assert.That(dto.IOFARecolher, Is.EqualTo(premio.Pagamento.IOFARecolher));
            Assert.That(dto.IOFRetido, Is.EqualTo(premio.Pagamento.IOFRetido));
            Assert.That(dto.IdentificadorCredito, Is.EqualTo(premio.Pagamento.IdentificadorCredito));
        }

        private EventoAportePremio ObterEventoAporte(TipoRegimeFinanceiroEnum regimeFinanceiro, int parcela)
        {
            return EventoAportePremioBuilder.UmEvento().ComIdentificador(_identificador).Padrao()
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
                ).Build();
        }
    }
}

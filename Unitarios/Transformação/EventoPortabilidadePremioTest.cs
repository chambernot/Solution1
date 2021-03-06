﻿using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Moq;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Transformação
{
    [TestFixture]
    public class EventoPortabilidadePremioTest: UnitTesBase
    {
        private EventoPremio _evento;
        private IPortabilidadeApropriada _portabilidade;
        private List<MovimentoProvisao> _retornoMovimento;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _portabilidade = PortabilidadeApropriadaBuilder.UmBuilder().Padrao().Build();
            var premioAnterior = PremioBuilder.Um().Padrao().ComTipoMovimento((short)TipoMovimentoEnum.Inexistente).Build();

            var retornoCobertura = CoberturaContratadaBuilder.Uma().Padrao().ComRegimeFinanceiro((short)TipoRegimeFinanceiroEnum.FundoAcumulacao);
            var movimento = MovimentoProvisaoBuilder.UmBuilder().Padrao();
            _retornoMovimento = new List<MovimentoProvisao>() { movimento.Build() };
            
            var retornoPremio = PremioBuilder.Um().Padrao()
                .Com(movimento)
                .Com(retornoCobertura)
                .Build();

            MockingKernel.GetMock<IPremioService>()
                .Setup(x => x.CriarPremio(It.IsAny<Premio>(), It.IsAny<EventoPremio>())).Returns(Task.FromResult(retornoPremio));

            MockingKernel.GetMock<ICoberturas>()
                .Setup(x => x.ObterPorItemCertificado(It.IsAny<long>())).Returns(Task.FromResult(retornoCobertura.Build()));

            MockingKernel.GetMock<ICalculadorProvisaoPremio>()
                .Setup(x => x.CriarProvisao(It.IsAny<Premio>())).Returns(_retornoMovimento.AsEnumerable());

            MockingKernel.GetMock<IPremios>()
                .Setup(x => x.ObterPremioAnterior(It.IsAny<long>(), It.IsAny<int>())).Returns(Task.FromResult(premioAnterior));

            _evento = MockingKernel.Get<EventoPremioFactory>().Fabricar(_portabilidade).Result;
        }

        [Test]
        public void MapearEventoPortabilidade()
        {
            Assert.That(_evento.GetType(), Is.EqualTo(typeof(EventoPortabilidadePremio)));

            Assert.That(_portabilidade.Identificador, Is.EqualTo(_evento.Identificador));
            Assert.That(_portabilidade.IdentificadorNegocio, Is.EqualTo(_evento.IdentificadorNegocio));
            Assert.That(_portabilidade.IdentificadorCorrelacao, Is.EqualTo(_evento.IdentificadorCorrelacao));
            Assert.That(_portabilidade.DataExecucaoEvento, Is.EqualTo(_evento.DataExecucaoEvento));
        }

        [Test]
        public void MapearPortabilidadeApropriado()
        {
            var parcela = _portabilidade.Portabilidades.First();
            var premio = _evento.Premios.First();

            Assert.That(parcela.ParcelaId.IdentificadorExternoCobertura, Is.EqualTo(premio.ItemCertificadoApoliceId.ToString()));
            Assert.That(0, Is.EqualTo(premio.Numero));
            Assert.That(parcela.Vigencia.Competencia, Is.EqualTo(premio.Competencia));
            Assert.That(parcela.Vigencia.Inicio, Is.EqualTo(premio.InicioVigencia));
            Assert.That(parcela.Vigencia.Fim, Is.EqualTo(premio.FimVigencia));
            Assert.That(parcela.Valores.Beneficio, Is.EqualTo(premio.ValorBeneficio));
            Assert.That(parcela.Valores.CapitalSegurado, Is.EqualTo(premio.ValorCapitalSegurado));
            Assert.That(parcela.Valores.Carregamento, Is.EqualTo(premio.ValorCarregamento));
            Assert.That(parcela.Valores.Contribuicao, Is.EqualTo(premio.ValorPremio));
            Assert.That(parcela.CodigoSusep, Is.EqualTo(premio.CodigoSusep));
        }

        [Test]
        public void MapearPortabilidade()
        {
            var pagamento = _portabilidade.Portabilidades.First().Pagamento;
            var premio = _evento.Premios.First();

            Assert.That(pagamento.DataPagamento, Is.EqualTo(premio.DataPagamento));
            Assert.That(pagamento.DataApropriacao, Is.EqualTo(premio.DataApropriacao));
            Assert.That(pagamento.ValorPago, Is.EqualTo(premio.ValorPago));
            Assert.That(pagamento.Multa, Is.EqualTo(premio.Multa));
            Assert.That(pagamento.Desconto, Is.EqualTo(premio.Desconto));
            Assert.That(pagamento.IdentificadorCredito, Is.EqualTo(premio.IdentificadorCredito));
            Assert.That(pagamento.IOFARecolher, Is.EqualTo(premio.IOFARecolher));
            Assert.That(pagamento.IOFRetido, Is.EqualTo(premio.IOFRetido));
        }
    }
}

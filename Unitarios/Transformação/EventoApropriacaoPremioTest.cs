using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
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
    public class EventoApropriacaoPremioTest: UnitTesBase
    {
        private EventoPremio _evento;
        private IParcelaApropriada _parcelaApropriada;
        private List<MovimentoProvisao> _retornoMovimento;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _parcelaApropriada = ParcelaApropriadaBuilder.UmBuilder().Padrao().Build();
            var premioAnterior = PremioBuilder.Um().Padrao().ComTipoMovimento((short)TipoMovimentoEnum.Emissao).Build();

            var retornoCobertura = CoberturaContratadaBuilder.Uma().Padrao();
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

            _evento = MockingKernel.Get<EventoPremioFactory>().Fabricar(_parcelaApropriada).Result;
        }

        [Test]
        public void MapearEventoApropriacao()
        {
            Assert.That(_evento.GetType(), Is.EqualTo(typeof(EventoApropriacaoPremio)));

            Assert.That(_parcelaApropriada.Identificador, Is.EqualTo(_evento.Identificador));
            Assert.That(_parcelaApropriada.IdentificadorNegocio, Is.EqualTo(_evento.IdentificadorNegocio));
            Assert.That(_parcelaApropriada.IdentificadorCorrelacao, Is.EqualTo(_evento.IdentificadorCorrelacao));
            Assert.That(_parcelaApropriada.DataExecucaoEvento, Is.EqualTo(_evento.DataExecucaoEvento));
        }

        [Test]
        public void MapearPremioApropriacao()
        {
            var parcela = _parcelaApropriada.Parcelas.First();
            var premio = _evento.Premios.First();

            Assert.That(parcela.ParcelaId.IdentificadorExternoCobertura, Is.EqualTo(premio.ItemCertificadoApoliceId.ToString()));
            Assert.That(parcela.ParcelaId.NumeroParcela, Is.EqualTo(premio.Numero));
            Assert.That(parcela.Vigencia.Competencia, Is.EqualTo(premio.Competencia));
            Assert.That(parcela.Vigencia.Inicio, Is.EqualTo(premio.InicioVigencia));
            Assert.That(parcela.Vigencia.Fim, Is.EqualTo(premio.FimVigencia));
            Assert.That(parcela.Valores.Beneficio, Is.EqualTo(premio.ValorBeneficio));
            Assert.That(parcela.Valores.CapitalSegurado, Is.EqualTo(premio.ValorCapitalSegurado));
            Assert.That(parcela.Valores.Carregamento, Is.EqualTo(premio.ValorCarregamento));
            Assert.That(parcela.Valores.Contribuicao, Is.EqualTo(premio.ValorPremio));            
        }

        [Test]
        public void MapearApropriacao()
        {
            var pagamento = _parcelaApropriada.Parcelas.First().Pagamento;
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

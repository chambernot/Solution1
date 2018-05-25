using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using NUnit.Framework;
using System;
using System.Linq;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class ApropriacaoPremioExtensionTest: UnitTesBase
    {
        private IParcelaApropriada _apropriacaoPremio;
        private IApropriacao _parcela;        

        [Test]
        public void DadoUmaApropriacaoComIdentificadorInvalidaDeveGerarErro()
        {
            _apropriacaoPremio = ParcelaApropriadaBuilder.UmBuilder().Build();

            Assert.That(() => _apropriacaoPremio.Validar(), GeraErro("O Identificador não pode ser nulo."));            
        }

        [Test]
        public void DadoUmaApropriacaoComDataExcucaoInvalidaDeveGerarErro()
        {
            _apropriacaoPremio = ParcelaApropriadaBuilder.UmBuilder().Build();

            Assert.That(() => _apropriacaoPremio.Validar(), GeraErro("A Data de Execução não pode ser nula."));
        }

        [Test]
        public void DadoUmaApropriacaoSemPremioDeveGerarErro()
        {
            _apropriacaoPremio = ParcelaApropriadaBuilder.UmBuilder().ComIdentificador(IdentificadoresPadrao.Identificador).ComDataExecucaoEvento(DateTime.Now).Build();

            Assert.That(() => _apropriacaoPremio.Validar(), GeraErro("Nenhum prêmio para Apropriar."));
        }

        [Test]
        public void DadoUmPremioComDataPagamentoInvalidaDeveGerarErro()
        {
            _parcela = ApropriacaoBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataApropriacao(DateTime.Now)
                            .ComValorPago(IdentificadoresPadrao.ValorPago))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Pagamento para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioComDataApropriacaoInvalidaDeveGerarErro()
        {
            _parcela = ApropriacaoBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataPagamento(DateTime.Now)
                            .ComValorPago(IdentificadoresPadrao.ValorPago))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Apropriação para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioComValorPagoInvalidoDeveGerarErro()
        {
            _parcela = ApropriacaoBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataPagamento(DateTime.Now)
                            .ComDataApropriacao(DateTime.Now))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("O Valor Pago para o ItemCertificadoApolice"));
        }

        [Test]
        [Ignore("TODO")]
        public void DadoUmaApropriacaoPremioDeveMapearOPremio()
        {
            _apropriacaoPremio = ParcelaApropriadaBuilder.UmBuilder().Padrao().Build();

            var premio = GetInstance<EventoPremioFactory>().Fabricar(_apropriacaoPremio).Premios.First();

            Assert.That(premio.Pagamento.DataPagamento , Is.EqualTo(IdentificadoresPadrao.DataPagamento));
            Assert.That(premio.Pagamento.DataApropriacao , Is.EqualTo(IdentificadoresPadrao.DataApropriacao));
            Assert.That(premio.Pagamento.ValorPago , Is.EqualTo(IdentificadoresPadrao.ValorPago));
            Assert.That(premio.Pagamento.Desconto , Is.EqualTo(IdentificadoresPadrao.Desconto));
            Assert.That(premio.Pagamento.Multa , Is.EqualTo(IdentificadoresPadrao.Multa));
            Assert.That(premio.Pagamento.IOFARecolher , Is.EqualTo(IdentificadoresPadrao.IOFARecolher));
            Assert.That(premio.Pagamento.IOFRetido , Is.EqualTo(IdentificadoresPadrao.IOFRetido));
            Assert.That(premio.Pagamento.IdentificadorCredito , Is.EqualTo(IdentificadoresPadrao.IdentificadorCredito));            
        }
    }
}

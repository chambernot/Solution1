using Mongeral.Integrador.Contratos.Premio;
using System;
using System.Linq;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    public class AportePremioExtensionsTest: UnitTesBase
    {
        private IAporteApropriado _aporteApropriado;
        private IAporte _parcela;
        private EventoPremioFactory _factory;

        [Test]
        public void DadoUmAporteComIdentificadorInvalidaDeveGerarErro()
        {
            _aporteApropriado = AporteApropriadoBuilder.UmBuilder().Build();

            Assert.That(() => _aporteApropriado.Validar(), GeraErro("O Identificador não pode ser nulo."));
        }

        [Test]
        public void DadoUmAporteComDataExcucaoInvalidaDeveGerarErro()
        {
            _aporteApropriado = AporteApropriadoBuilder.UmBuilder().Build();

            Assert.That(() => _aporteApropriado.Validar(), GeraErro("A Data de Execução não pode ser nula."));
        }

        [Test]
        public void DadoUmAporteSemPremioDeveGerarErro()
        {
            _aporteApropriado = AporteApropriadoBuilder.UmBuilder()
                .ComIdentificador(IdentificadoresPadrao.Identificador)
                .ComDataExecucaoEvento(DateTime.Now).Build();

            Assert.That(() => _aporteApropriado.Validar(), GeraErro("Nenhum aporte informado."));
        }

        [Test]
        public void DadoUmAporteDePremioComDataPagamentoInvalidaDeveGerarErro()
        {
            _parcela = CriaParcela(DateTime.MinValue, DateTime.Now, IdentificadoresPadrao.ValorPago);

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Pagamento para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmAporteDePremioComDataApropriacaoInvalidaDeveGerarErro()
        {
            _parcela = CriaParcela(DateTime.Now, DateTime.MinValue, IdentificadoresPadrao.ValorPago);

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Portabilidade para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioDePortabilidadeComValorPagoInvalidoDeveGerarErro()
        {
            _parcela = CriaParcela(DateTime.Now, DateTime.Now, Decimal.MinValue);

            Assert.That(() => _parcela.Validar(), GeraErro("O Valor Pago para o ItemCertificadoApolice"));
        }

        private IAporte CriaParcela(DateTime dataPagamento, DateTime dataApropriacao, decimal valorPago)
        {
            return AporteBuilder.UmBuilder().Padrao()
                .Com(PagamentoBuilder.UmBuilder()
                    .ComDataPagamento(dataPagamento)
                    .ComDataApropriacao(dataApropriacao)
                    .ComValorPago(valorPago))
                .Build();
        }

        [Test]
        [Ignore("TODO")]
        public void DadoUmaPortabilidadePremioDeveMapearOPremio()
        {
            _aporteApropriado = AporteApropriadoBuilder.UmBuilder().Padrao().Build();
            
            var premios = GetInstance<EventoPremioFactory>().Fabricar(_aporteApropriado);

            var premio = premios.Premios.First();
            
            Assert.That(premio.Pagamento.DataPagamento, Is.EqualTo(IdentificadoresPadrao.DataPagamento));
            Assert.That(premio.Pagamento.DataApropriacao, Is.EqualTo(IdentificadoresPadrao.DataApropriacao));
            Assert.That(premio.Pagamento.ValorPago, Is.EqualTo(IdentificadoresPadrao.ValorPago));
            Assert.That(premio.Pagamento.Desconto, Is.EqualTo(IdentificadoresPadrao.Desconto));
            Assert.That(premio.Pagamento.Multa, Is.EqualTo(IdentificadoresPadrao.Multa));
            Assert.That(premio.Pagamento.IOFARecolher, Is.EqualTo(IdentificadoresPadrao.IOFARecolher));
            Assert.That(premio.Pagamento.IOFRetido, Is.EqualTo(IdentificadoresPadrao.IOFRetido));
            Assert.That(premio.Pagamento.IdentificadorCredito, Is.EqualTo(IdentificadoresPadrao.IdentificadorCredito));
        }
    }
}

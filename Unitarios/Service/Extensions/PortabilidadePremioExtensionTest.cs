using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class PortabilidadePremioExtensionTest: UnitTesBase
    {
        private IPortabilidadeApropriada _portabilidadePremio;
        private IPortabilidade _parcela;

        [Test]
        public void DadoUmPortabilidadeComIdentificadorInvalidaDeveGerarErro()
        {
            _portabilidadePremio = PortabilidadeApropriadaBuilder.UmBuilder().Build();

            Assert.That(() => _portabilidadePremio.Validar(), GeraErro("O Identificador não pode ser nulo."));
        }

        [Test]
        public void DadoUmaPortabilidadeComDataExcucaoInvalidaDeveGerarErro()
        {
            _portabilidadePremio = PortabilidadeApropriadaBuilder.UmBuilder().Build();

            Assert.That(() => _portabilidadePremio.Validar(), GeraErro("A Data de Execução não pode ser nula."));
        }

        [Test]
        public void DadoUmaPortabilidadeSemPremioDeveGerarErro()
        {
            _portabilidadePremio = PortabilidadeApropriadaBuilder.UmBuilder().ComIdentificador(IdentificadoresPadrao.Identificador).ComDataExecucaoEvento(DateTime.Now).Build();

            Assert.That(() => _portabilidadePremio.Validar(), GeraErro("Nenhum prêmio informado."));
        }

        [Test]
        public void DadoUmPremioDePortabilidadeComDataPagamentoInvalidaDeveGerarErro()
        {
            _parcela = PortabilidadeBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataApropriacao(DateTime.Now)
                            .ComValorPago(IdentificadoresPadrao.ValorPago))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Pagamento para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioDePortabilidadeComDataApropriacaoInvalidaDeveGerarErro()
        {
            _parcela = PortabilidadeBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataPagamento(DateTime.Now)
                            .ComValorPago(IdentificadoresPadrao.ValorPago))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Portabilidade para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioDePortabilidadeComValorPagoInvalidoDeveGerarErro()
        {
            _parcela = PortabilidadeBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataPagamento(DateTime.Now)
                            .ComDataApropriacao(DateTime.Now))
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("O Valor Pago para o ItemCertificadoApolice"));
        }

        [Test]
        [Ignore("TDO")]
        public void DadoUmaPortabilidadePremioDeveMapearOPremio()
        {
            _portabilidadePremio = PortabilidadeApropriadaBuilder.UmBuilder().Padrao().Build();

            var premio = GetInstance<EventoPremioFactory>().Fabricar(_portabilidadePremio).Premios.First();

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

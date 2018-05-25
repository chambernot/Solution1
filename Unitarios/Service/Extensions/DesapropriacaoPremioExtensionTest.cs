using Mongeral.Integrador.Contratos.Premio;
using NUnit.Framework;
using System;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using System.Linq;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class DesapropriacaoPremioExtensionTest: UnitTesBase
    {
        private IParcelaDesapropriada _desapropriacaoPremio;
        private IApropriacao _parcela;

        [Test]
        public void DadoUmContratoDesapropriacaoPremioComIdentificadorInvalidoDeveGerarErro()
        {
            _desapropriacaoPremio = ParcelaDesapropriadaBuilder.UmBuilder()
                .ComDataExecucaoEvento(DateTime.Now)
                .Build();

            Assert.That(() => _desapropriacaoPremio.Validar(), GeraErro("O Identificador não pode ser nulo."));
        }

        [Test]
        public void DadoUmContratoDesapropriacaoPremioComDataExecucaoInvalidaDeveGerarErro()
        {
            _desapropriacaoPremio = ParcelaDesapropriadaBuilder.UmBuilder().ComIdentificador(Guid.NewGuid()).Build();

            Assert.That(() => _desapropriacaoPremio.Validar(), GeraErro("A Data de Execução não pode ser nula."));
        }

        [Test]
        public void DadoUmaDesapropriacaoSemPremioDeveGerarErro()
        {
            _desapropriacaoPremio = ParcelaDesapropriadaBuilder.UmBuilder()
                .ComIdentificador(IdentificadoresPadrao.Identificador)
                .ComDataExecucaoEvento(DateTime.Now).Build();

            Assert.That(() => _desapropriacaoPremio.Validar(), GeraErro("Nenhum prêmio para Desapropriar."));
        }
        
        [Test]
        public void DadoUmPremioComDataPagamentoInvalidaDeveGerarErro()
        {
            _parcela = ApropriacaoBuilder.UmBuilder().Padrao()
                        .Com(PagamentoBuilder.UmBuilder()
                            .ComDataApropriacao(DateTime.Now)
                            .ComValorPago(IdentificadoresPadrao.ValorPago)
                            )
                        .Build();

            Assert.That(() => _parcela.Validar(), GeraErro("A Data de Pagamento para o ItemCertificadoApolice"));
        }

        [Test]
        public void DadoUmPremioComDataApropriacaoInvalidaDeveGerarErro()
        {
            _parcela = ApropriacaoBuilder.UmBuilder().Padrao()
                .Com(PagamentoBuilder.UmBuilder()
                    .ComDataPagamento(DateTime.Now)
                    .ComValorPago(IdentificadoresPadrao.ValorPago)
                )
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
        public void DadoUmaDesapropriacaoPremioDeveMapearOPremio()
        {
            _desapropriacaoPremio = ParcelaDesapropriadaBuilder.UmBuilder().Padrao().Build();

            var premio = GetInstance<EventoPremioFactory>().Fabricar(_desapropriacaoPremio).Premios.First();

            Assert.That(premio.ItemCertificadoApoliceId, Is.EqualTo(IdentificadoresPadrao.ItemCertificadoApoliceId));
            Assert.That(premio.InicioVigencia, Is.EqualTo(IdentificadoresPadrao.DataInicioVigencia));
            Assert.That(premio.FimVigencia, Is.EqualTo(IdentificadoresPadrao.DataFimVigencia));
            Assert.That(premio.ValorBeneficio, Is.EqualTo(IdentificadoresPadrao.ValorBeneficio));
            Assert.That(premio.ValorPremio, Is.EqualTo(IdentificadoresPadrao.ValorContribuicao));
            Assert.That(premio.Numero, Is.EqualTo(IdentificadoresPadrao.NumeroParcela));
            Assert.That(premio.ValorCapitalSegurado, Is.EqualTo(IdentificadoresPadrao.ValorCapitalSegurado));
            Assert.That(premio.ValorCarregamento, Is.EqualTo(IdentificadoresPadrao.ValorCarregamento));
        }
    }
}

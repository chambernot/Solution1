using NUnit.Framework;
using System;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class AjustePremioExtensionTest: UnitTesBase
    {
        private IParcelaAjustada _parcelaAjustada;
        private EventoPremioFactory _factory;

        [Test]
        public void DadoUmContratoDeAjusteComIdentificadorInvalidoDeveGerarErro()
        {
            _parcelaAjustada = ParcelaAjustadaBuilder.UmBuilder().ComIdentificador(default(Guid)).Build();            

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_parcelaAjustada), GeraErro("O Identificador não pode ser nulo."));
        }

        //[Test]
        //public void DadoUmContratoDeAjusteComDataExecucaoInvalidaDeveGerarErro()
        //{
        //    _parcelaAjustada = ParcelaAjustadaBuilder.UmBuilder().ComDataExecucao(DateTime.MinValue).Build();

        //    Assert.That(() => _parcelaAjustada.ValidarAjustePremio(), GeraErro("A Data de Execução não pode ser nula."));
        //}

        //[Test]
        //public void DadoUmContratoDeAjusteSemNenhumaParcelaDeveGerarErro()
        //{
        //    _parcelaAjustada = ParcelaAjustadaBuilder.UmBuilder()
        //                        .ComIdentificador(IdentificadoresPadrao.Identificador)
        //                        .ComDataExecucao(IdentificadoresPadrao.DataExecucaoEvento)
        //                        .Build();

        //    Assert.That(() => _parcelaAjustada.ValidarAjustePremio(), GeraErro("Nenhum prêmio para ajustar."));
        //}

        //[Test]
        //public void DadoUmContratoDeveMapear()
        //{
        //    _parcelaAjustada = ParcelaAjustadaBuilder.UmBuilder().Padrao().Build();

        //    var premio = _parcelaAjustada.ToEventoAjustePremio().First();

        //    Assert.That(IdentificadoresPadrao.Competencia, Is.EqualTo(premio.Competencia));
        //    Assert.That(IdentificadoresPadrao.ValorContribuicao, Is.EqualTo(premio.ValorPremio));
        //    Assert.That(IdentificadoresPadrao.ValorBeneficio, Is.EqualTo(premio.ValorBeneficio));
        //}
    }
}

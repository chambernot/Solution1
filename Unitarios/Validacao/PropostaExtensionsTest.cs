using System;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class PropostaExtensionsTest: UnitTesBase
    {
        private IProposta _proposta;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _proposta = PropostaBuilder.UmaProposta().ComNumeroProposta(IdentificadoresPadrao.NumeroProposta).Build();
        }

        [Test]
        public void ValidarDataImplantacao()
        {            
            Assert.That(() => _proposta.Validar(), GeraErro($"Proposta com Data de implantação inválida. Número da Proposta: {_proposta.Numero}."));
        }

        [Test]
        public void ValidarAssinatura()
        {
            Assert.That(() => _proposta.Validar(), GeraErro($"Proposta com Data de assinatura inválida. Número da Proposta: {_proposta.Numero}."));
        }

        [Test]
        public void ValidarNumero()
        {
            var proposta = PropostaBuilder.UmaProposta().ComIdentificador(Guid.NewGuid()).Build();
            Assert.That(() => proposta.Validar(), GeraErro($"Número da proposta inválido. Identificador: {proposta.Identificador}."));
        }        
    }
}

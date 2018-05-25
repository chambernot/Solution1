using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class CoberturaContratadaExtensions
    {
        public static IAssertion Validar(this ICobertura cobertura, long inscricaoCertificado)
        {
            var identificador = Assertion.NotNullOrEmpty(cobertura.IdentificadorExterno, $"Cobertura com identificador externo inválido. Inscrição Certificado: {inscricaoCertificado}.");
            var inicioVigencia = Assertion.IsFalse(cobertura.InicioVigencia.Equals(default(DateTime)), $"Cobertura com data de inicio de vigencia inválida: Identificador Externo: {cobertura.IdentificadorExterno}.");
            var itemProdutoId = Assertion.GreaterThan(cobertura.CodigoItemProduto, default(int), $"Cobertura com ItemProduto inválido. Identificador Externo: {cobertura.IdentificadorExterno}.");

            return identificador.and(inicioVigencia).and(itemProdutoId);
        }
    }
}

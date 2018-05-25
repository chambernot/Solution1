using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos.Premio;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class ParcelaExtensions
    {
        public static void Validar(this IParcela parcela)
        {
            Assertion.NotNull(parcela.ParcelaId, "A ParcelaId não foi informada.").Validate();
            Assertion.NotNull(parcela.Valores, $"A ParcelaId: {parcela.ParcelaId.ParcelaId}, com valores inválidos.").Validate();
            Assertion.NotNull(parcela.Vigencia, $"A ParcelaId: {parcela.ParcelaId.ParcelaId}, com vigência inválida.").Validate();
            Assertion.NotNull(parcela.ParcelaId.IdentificadorExternoCobertura, $"A ParcelaId: {parcela.ParcelaId.ParcelaId}, com vigência com Identificador Externo inválido.").Validate();

            var vigenciaInicio = Assertion.IsFalse(parcela.Vigencia.Inicio.Equals(DateTime.MinValue), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, com Inicio da Vigência inválido.");
            var vigenciaFim = Assertion.IsFalse(parcela.Vigencia.Fim.Equals(DateTime.MinValue), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, com Fim da Vigência inválido.");
            var competencia = Assertion.IsFalse(parcela.Vigencia.Competencia.Equals(DateTime.MinValue), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, com Competência inválido.");            
            var numeroParcela = Assertion.EqualsOrGreaterThan(parcela.ParcelaId.NumeroParcela, default(int), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, com Numero da Parcela inválido.");
            var valorPremio = Assertion.GreaterThan(parcela.Valores.Contribuicao, default(decimal), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, o Valor de Contribuição não foi informado.");
            var valorBeneficio = Assertion.GreaterThan(parcela.Valores.Beneficio, default(decimal), $"IdentificadorExterno: {parcela.ParcelaId.IdentificadorExternoCobertura}, o Valor de Benfício não foi informado.");
            
            vigenciaInicio
                .and(vigenciaFim)
                .and(competencia)                
                .and(numeroParcela)
                .and(valorPremio)
                .and(valorBeneficio)
                .Validate();
        }
    }
}

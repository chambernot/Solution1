using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class ProponenteExtensions
    {
        public static IAssertion Validar(this IProponente proponente, long numeroProposta)
        {
            var proponenteValidate = Assertion.NotNull(proponente, $"O proponente da proposta {numeroProposta} não foi informado");
            var matricula = Assertion.GreaterThan(proponente?.Matricula, default(long), $"Matrícula do proponente inválido. Número da Proposta: {numeroProposta}.");
            var dataNascimento = Assertion.GreaterThan(proponente?.DataNascimento, default(DateTime), $"Data Nascimento do proponente inválido. Número da Proposta: {numeroProposta}.");
            var sexo = Assertion.NotNullOrEmpty(proponente?.Sexo, "O Sexo do Proponente não preenchido.");

            return proponenteValidate.and(matricula).and(dataNascimento).and(sexo);
        }
    }
}

using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using System;
using Mongeral.Provisao.V2.Domain.Entidades;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class PremioExtension
    {
        public static void Validar<T>(this T evento) where T: IEvento
        {
            var identificadorValidator = Assertion.GreaterThan(evento.Identificador, default(Guid), "O Identificador não pode ser nulo.");
            var dataExecucaoValidade = Assertion.GreaterThan(evento.DataExecucaoEvento, default(DateTime), "A Data de Execução não pode ser nula.");            

            identificadorValidator.and(dataExecucaoValidade).Validate();
        }

        public static IAssertion ValidarPremio<T>(this T premio) where T : IParcela
        {
            var itemCertificadoApoliceValidator = Assertion.IsTrue(premio.ParcelaId?.IdentificadorExternoCobertura != string.Empty, "O ItemCerficadoApolice não foi informado");
            var numeroParcelaValidator = Assertion.EqualsOrGreaterThan(premio.ParcelaId?.NumeroParcela, default(int), "O número da Parcela não foi informado.");
            var competenciaValidator = Assertion.IsTrue(premio.Vigencia?.Competencia >= premio.Vigencia?.Inicio && premio.Vigencia?.Competencia <= premio.Vigencia?.Fim, "A data de competência inválida, a data não pode estar fora do período de Vigência.");
            var valorPremioValidator = Assertion.GreaterThan(premio.Valores.Contribuicao, default(decimal), "O valor de contribuição não foi informado.");
            var valorBeneficioValidator = Assertion.GreaterThan(premio.Valores.Beneficio, default(decimal), "O valor de Benfício não foi informado.");

            return itemCertificadoApoliceValidator
                .and(numeroParcelaValidator)
                .and(competenciaValidator)
                .and(valorPremioValidator)
                .and(valorBeneficioValidator);
        }
        
        public static T ToPremio<T>(this IParcela premio) where T : Domain.Premio
        {
            var domain =  new Domain.Premio()
            {
                ItemCertificadoApoliceId = long.Parse(premio.ParcelaId.IdentificadorExternoCobertura),
                Numero = premio.ParcelaId.NumeroParcela,
                Competencia = premio.Vigencia.Competencia,
                InicioVigencia = premio.Vigencia.Inicio,
                FimVigencia = premio.Vigencia.Fim,
                ValorPremio = premio.Valores.Contribuicao,
                ValorCarregamento = premio.Valores.Carregamento,
                ValorBeneficio = premio.Valores.Beneficio,
                ValorCapitalSegurado = premio.Valores.CapitalSegurado
            };

            return domain as T;
        }

        public static PagamentoPremio ToPagamentoPremio(this IPagamento pagamento)
        {
            return new PagamentoPremio()
            {
                DataPagamento = pagamento.DataPagamento,
                DataApropriacao = pagamento.DataApropriacao,
                ValorPago = pagamento.ValorPago,
                Desconto = pagamento.Desconto,
                Multa = pagamento.Multa,
                IOFRetido = pagamento.IOFRetido,
                IOFARecolher = pagamento.IOFARecolher,
                IdentificadorCredito = pagamento.IdentificadorCredito
            };
        }


    }
}

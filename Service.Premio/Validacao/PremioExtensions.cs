using System.Collections.Generic;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class PremioExtensions
    {
        public static void ValidaPremioAnterior(this Domain.Premio premio, Domain.Premio premioAnterior)
        {
            IDictionary<TipoMovimentoEnum, IDictionary<TipoMovimentoEnum, bool>> matrizValidacao = new Dictionary<TipoMovimentoEnum, IDictionary<TipoMovimentoEnum, bool>>()
            {
                {
                    TipoMovimentoEnum.Emissao, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Inexistente, true},
                        {TipoMovimentoEnum.Ajuste, true},
                        {TipoMovimentoEnum.CancelamentoAjuste, true},
                        {TipoMovimentoEnum.CancelamentoEmissao, true}
                    }
                },
                {
                    TipoMovimentoEnum.Apropriacao, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Emissao, true},
                        {TipoMovimentoEnum.Reemissao, true},
                        {TipoMovimentoEnum.Desapropriacao, true}
                    }
                },
                {
                    TipoMovimentoEnum.Desapropriacao, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Apropriacao, true}
                    }
                },
                {
                    TipoMovimentoEnum.Ajuste, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Emissao, true}
                    }
                },
                {
                    TipoMovimentoEnum.Aporte, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Inexistente, true},
                        {TipoMovimentoEnum.Emissao, false},
                        {TipoMovimentoEnum.CancelamentoEmissao, false}
                    }
                },
                {
                    TipoMovimentoEnum.Portabilidade, new Dictionary<TipoMovimentoEnum, bool>()
                    {
                        {TipoMovimentoEnum.Inexistente, true},
                        {TipoMovimentoEnum.Emissao, false},
                        {TipoMovimentoEnum.CancelamentoEmissao, false}
                    }
                }
            };

            var tipoMovimentoAnterior = premioAnterior == null ? TipoMovimentoEnum.Inexistente
                : (TipoMovimentoEnum)System.Enum.Parse(typeof(TipoMovimentoEnum), premioAnterior.TipoMovimentoId.ToString());

            var tipoMovimentoAtual = premio.EventoOperacional.TipoMovimento;

            Assertion.IsTrue(matrizValidacao.ContainsKey(tipoMovimentoAtual), $"Validação de movimentação não definida para {tipoMovimentoAtual}").Validate();

            Assertion.IsTrue(matrizValidacao[tipoMovimentoAtual].ContainsKey(tipoMovimentoAnterior), $"Impossivel validar movimento de {tipoMovimentoAtual} precedido de {tipoMovimentoAnterior}").Validate();

            Assertion.IsTrue(matrizValidacao[tipoMovimentoAtual][tipoMovimentoAnterior],
                    $"{premio}: Evento de {tipoMovimentoAtual} inválido para movimentação anterior ({tipoMovimentoAnterior}).").Validate();
        }
    }
}

using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class EventoOperacionalExtensions
    {
        public static IAssertion ValidarEvento(this IEvento evento)
        {
            var identificador = Assertion.IsFalse(evento.Identificador.Equals(Guid.Empty), "O identificador do evento não foi informado.");
            var dataExecucao = Assertion.IsFalse(evento.DataExecucaoEvento.Equals(default(DateTime)), "A data de execução do evento não foi informada.");

            return identificador.and(dataExecucao);
        }
    }
}

using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class EmissaoPremioExtension
    {
        public static void ValidarPremioEmitido(this IParcelaEmitida eventoEmissao)
        {
            eventoEmissao.Validar();

            Assertion.NotNull(eventoEmissao.Parcelas, "Nenhum prêmio para ser emitido.").Validate();

            eventoEmissao.Parcelas.Select(p => p.ValidarPremio()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }        

        public static IEnumerable<Domain.Premio> ToEventoPremio(this IParcelaEmitida premioEmitido)
        {
            var evento = new EventoEmissaoPremio(premioEmitido.Identificador, premioEmitido.IdentificadorCorrelacao, premioEmitido.IdentificadorNegocio, premioEmitido.DataExecucaoEvento);

            premioEmitido.Parcelas.SafeForEach(premio => evento.AdicionarPremiosEmitidos(premio.ToPremio<Domain.Premio>()));

            return evento.Premios;
        }

        public static void AdicionarProvisoes(this IParcelaEmitida premioEmitido, EventoEmissaoPremio eventoPremio)
        {
            premioEmitido.Parcelas.SafeForEach(p => MovimentoToProvisao(p, eventoPremio));
        }

        private static void MovimentoToProvisao(this IParcela premioEntrada, EventoEmissaoPremio eventoPremio)
        {
            var premioCadastrado = eventoPremio.Premios.First(p => p.ItemCertificadoApoliceId == long.Parse(premioEntrada.ParcelaId.IdentificadorExternoCobertura));

            Assertion.NotNull(premioCadastrado, "Não foi possível obter as coberturas para preencher as provisões a serem devolvidas no evento.").Validate();

            premioEntrada.Provisoes = premioCadastrado.MovimentosProvisao.ToList<IProvisao>();
        }
    }
}

using System;
using System.Linq;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoPortabilidadePremioBuilder: BaseBuilder<EventoPortabilidadePremio>
    {
        private EventoPortabilidadePremioBuilder(Guid identificador)
        {
            Instance = new EventoPortabilidadePremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoPortabilidadePremioBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoPortabilidadePremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoPortabilidadePremioBuilder UmEvento()
        {
            return new EventoPortabilidadePremioBuilder(Guid.NewGuid());
        }

        public EventoPortabilidadePremioBuilder ComIdentificador(Guid identificador)
        {
            return new EventoPortabilidadePremioBuilder(identificador);
        }
        public EventoPortabilidadePremioBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            return this;
        }

        public EventoPortabilidadePremioBuilder Padrao()
        {
            return new EventoPortabilidadePremioBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);
        }
    }
}

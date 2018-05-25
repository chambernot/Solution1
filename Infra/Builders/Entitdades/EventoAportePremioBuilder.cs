using System;
using System.Linq;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoAportePremioBuilder: BaseBuilder<EventoAportePremio>
    {
        private EventoAportePremioBuilder(Guid identificador)
        {
            Instance = new EventoAportePremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoAportePremioBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoAportePremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoAportePremioBuilder UmEvento()
        {
            return new EventoAportePremioBuilder(Guid.NewGuid());
        }

        public EventoAportePremioBuilder ComIdentificador(Guid identificador)
        {
            return new EventoAportePremioBuilder(identificador);
        }

        public EventoAportePremioBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            return this;
        }        

        public EventoAportePremioBuilder Padrao()
        {
            return new EventoAportePremioBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);
        }
    }
}

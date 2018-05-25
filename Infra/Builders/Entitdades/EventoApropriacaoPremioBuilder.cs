using Mongeral.Provisao.V2.Domain;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoApropriacaoPremioBuilder: BaseBuilder<EventoApropriacaoPremio>
    {
        private EventoApropriacaoPremioBuilder(Guid identificador)
        {
            Instance = new EventoApropriacaoPremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoApropriacaoPremioBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoApropriacaoPremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoApropriacaoPremioBuilder UmEvento()        
        {
            return new EventoApropriacaoPremioBuilder(Guid.NewGuid());
        }

        public EventoApropriacaoPremioBuilder ComIdentificador(Guid identificador)
        {
            return new EventoApropriacaoPremioBuilder(identificador);
        }

        public EventoApropriacaoPremioBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            return this;
        }

        public EventoApropriacaoPremioBuilder Padrao()
        {
            return new EventoApropriacaoPremioBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);
        }
    }
}

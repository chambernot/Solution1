using Mongeral.Provisao.V2.Domain;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoEmissaoPremioBuilder: BaseBuilder<EventoEmissaoPremio>
    {
        private EventoEmissaoPremioBuilder(Guid identificador)
        {
            Instance = new EventoEmissaoPremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoEmissaoPremioBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoEmissaoPremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoEmissaoPremioBuilder UmEvento()
        {
            return new EventoEmissaoPremioBuilder(Guid.NewGuid());
        }

        public static EventoEmissaoPremioBuilder ComIdentificador(Guid identificador)
        {
            return new EventoEmissaoPremioBuilder(identificador);
        }

        public static EventoEmissaoPremioBuilder ComDataExecucao(Guid identificador, DateTime data)
        {
            return new EventoEmissaoPremioBuilder(identificador, string.Empty, string.Empty, data);
        }

        public EventoEmissaoPremioBuilder Padrao()
        {
            return new EventoEmissaoPremioBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);            
        }

        public EventoEmissaoPremioBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            
            return this;
        }
    }
}

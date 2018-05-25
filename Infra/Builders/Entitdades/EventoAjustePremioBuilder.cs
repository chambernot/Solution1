using Mongeral.Provisao.V2.Domain;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoAjusteParcelaBuilder: BaseBuilder<EventoAjustePremio>
    {
        private EventoAjusteParcelaBuilder(Guid identificador)
        {
            Instance = new EventoAjustePremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoAjusteParcelaBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoAjustePremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoAjusteParcelaBuilder UmEvento()
        {
            return new EventoAjusteParcelaBuilder(Guid.NewGuid());
        }

        public EventoAjusteParcelaBuilder ComIdentificador(Guid identificador)
        {
            return new EventoAjusteParcelaBuilder(identificador);            
        }

        public EventoAjusteParcelaBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            return this;
        }
        
        public EventoAjusteParcelaBuilder Padrao()
        {
            new EventoAjusteParcelaBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);
            Com(PremioBuilder.Um().Padrao());
            return this;
        }
    }
}

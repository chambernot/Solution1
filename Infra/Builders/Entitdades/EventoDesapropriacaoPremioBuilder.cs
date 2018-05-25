using Mongeral.Provisao.V2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoDesParcelaApropriadaBuilder: BaseBuilder<EventoDesapropriacaoPremio>
    {
        private EventoDesParcelaApropriadaBuilder(Guid identificador)
        {
            Instance = new EventoDesapropriacaoPremio(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        private EventoDesParcelaApropriadaBuilder(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Instance = new EventoDesapropriacaoPremio(identificador, idCorrelacao, idNegocio, dataExecucao);
        }

        public static EventoDesParcelaApropriadaBuilder UmBuilder()
        {
            return new EventoDesParcelaApropriadaBuilder(Guid.NewGuid());
        }

        public EventoDesParcelaApropriadaBuilder ComIdentificador(Guid identificador)
        {
            return new EventoDesParcelaApropriadaBuilder(identificador);
        }

        public EventoDesParcelaApropriadaBuilder Padrao()
        {
            return new EventoDesParcelaApropriadaBuilder(IdentificadoresPadrao.Identificador, IdentificadoresPadrao.IdentificadorCorrelacao, IdentificadoresPadrao.IdentificadorNegocio, IdentificadoresPadrao.DataExecucaoEvento);
        }

        public EventoDesParcelaApropriadaBuilder Com(params PremioBuilder[] premios)
        {
            premios.ToList().ForEach(premio => Instance.AdicionarPremio(premio.Build()));
            return this;
        }
    }
}

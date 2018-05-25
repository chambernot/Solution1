using Mongeral.Provisao.V2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoAlteracaoBuilder: BaseBuilder<EventoAlteracao>
    {
        private EventoAlteracaoBuilder()
        {
            Instance = new EventoAlteracao(IdentificadoresPadrao.Identificador,null,null,DateTime.Today);
        }        

        public static EventoAlteracaoBuilder UmEvento()
        {
            return new EventoAlteracaoBuilder();
        }

        public EventoAlteracaoBuilder ComDadosPadroes()
        {
            var historico = HistoricoCoberturaContratadaBuilder.UmHistorico().ComDadosPadroes().Build();
                        
            Instance.ComHistorico((new List<HistoricoCoberturaContratada>() { historico }).AsEnumerable());

            return this;
        }
    }
}

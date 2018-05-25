using Mongeral.Provisao.V2.Domain;
using System;


namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class EventoImplantacaoBuilder: BaseBuilder<EventoImplantacao>
    {   
        private EventoImplantacaoBuilder(Guid identificador)
        {
            Instance = new EventoImplantacao(identificador, string.Empty, string.Empty, IdentificadoresPadrao.DataExecucaoEvento);
        }

        public static EventoImplantacaoBuilder UmEvento(Guid identificador)
        {
            return new EventoImplantacaoBuilder(identificador);
        }

        public EventoImplantacaoBuilder Com(params CoberturaContratada[] coberturas)
        {
            Instance.ComCoberturas(coberturas);
            return this;
        }       
    }
}

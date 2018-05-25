using System;
using System.Linq;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades;

namespace Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades
{
    public class EventoBuilder : BaseBuilder<EventoImplantacao>
    {
        private EventoBuilder()
        {
            Instance = new EventoImplantacao(Guid.NewGuid(), null, null, new DateTime(2015, 01, 01)) {Id = Guid.NewGuid()};
        }

        public static EventoBuilder UmEvento()
        {
            return new EventoBuilder();
        }

        public EventoBuilder Com(params CoberturaContratadaBuilder[] cobertura)
        {
            Instance.Coberturas = cobertura.Select(x => x.Build()).ToList();
            return this;
        }
    }
}

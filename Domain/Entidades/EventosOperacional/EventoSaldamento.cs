
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoSaldamento : EventoCobertura
    {
        public EventoSaldamento(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao) : base(identificador, idCorrelacao, idNegocio, dataExecucao)
        {
        }

        public override short TipoEventoId => (short)TipoEventoEnum.Saldamento;

        public override TipoEventoEnum TipoEvento => throw new NotImplementedException();
    }
}

using Mongeral.Infrastructure.Domain.Model;
using System;

namespace Mongeral.Provisao.V2.Domain
{
    public class ProvisaoCobertura: Entidade<Guid>
    {
        public ProvisaoCobertura() { }

        public ProvisaoCobertura(CoberturaContratada cobertura, short tipoProvisaoId)
        {
            _cobertura = cobertura;
            TipoProvisaoId = tipoProvisaoId;
        }

        public CoberturaContratada _cobertura { get; set; }
        public Guid CoberturaContrataId => _cobertura.Id;
        public short TipoProvisaoId { get; set; }
    }
}
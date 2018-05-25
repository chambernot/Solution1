using System;
using Mongeral.Provisao.V2.Domain.Enum;
using System.Collections.Generic;
using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Domain.Factories
{
    public interface ICalculadorProvisaoPremio
    {
        IEnumerable<MovimentoProvisao> CriarProvisao(Premio premio);
    }
}

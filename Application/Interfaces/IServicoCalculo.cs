using Mongeral.Provisao.V2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IServicoCalculo
    {
        void ValidarDadosCalculoPMBAC(CoberturaContratada cobertura);
    }
}

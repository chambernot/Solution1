using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Entidades
{
    public class ParametrosProvisaoCalculo
    {
        
        public long ItemCertificadoApoliceId { get; set; }

        public TipoProvisaoEnum tipoprovisao { get; set; }
        
    }
}

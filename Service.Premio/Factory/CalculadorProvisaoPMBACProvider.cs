using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class CalculadorProvisaoPMBACProvider:CalculadorProvisaoPovider
    {
        ParametrosProvisaoCalculo parametrosProvisaoCalculo;

        public CalculadorProvisaoPMBACProvider(CoberturaContratada coberturacontratada, ICalculoFacade calculo, EventoOperacional eventooperacional, IProvisoes provisao)
        {
            parametrosProvisaoCalculo = new ParametrosProvisaoCalculo();
            parametrosProvisaoCalculo.ItemCertificadoApoliceId = coberturacontratada.ItemCertificadoApoliceId;
            parametrosProvisaoCalculo.tipoprovisao = Domain.Enum.TipoProvisaoEnum.PMBAC;
            
            Calculadores.Add(new Premio.Calculadores.CalculadorProvisaoMatematicaBeneficioAConceder(coberturacontratada, calculo, eventooperacional, parametrosProvisaoCalculo, provisao));
        }
    }
}

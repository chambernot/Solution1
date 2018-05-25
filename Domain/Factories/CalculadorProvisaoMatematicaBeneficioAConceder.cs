using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Domain.Factories
{
    public class CalculadorProvisaoMatematicaBeneficioAConceder
    {
        private readonly ICalculoFacade _calculo;
        public CalculadorProvisaoMatematicaBeneficioAConceder(ICalculoFacade calculo)
        {
            _calculo = calculo;
        }

        public virtual IEnumerable<ProvisaoDto> CalculaProvisao(Premio premio, decimal valorUltimaProvisao)
        {
            var dataExecucao = premio.EventoOperacional.DataExecucaoEvento;
            var competenciaCalculo = dataExecucao.Date.AddDays(1 - dataExecucao.Day);

            var provisionavel = premio.Cobertura.TipoItemProdutoId != (short)TipoItemProdutoEnum.VGBL
                             && premio.Cobertura.TipoItemProdutoId != (short)TipoItemProdutoEnum.PGBL;

            var listaProvisao = new List<ProvisaoDto>();

            if (provisionavel)
            {
                var pmbac = _calculo.CalcularPMBAC(premio.Cobertura, competenciaCalculo, valorUltimaProvisao);

                pmbac.ProvisaoId = (short)TipoProvisaoEnum.PMBAC;
                pmbac.DataMovimentacao = pmbac.DataMovimentacao != default(DateTime) ? pmbac.DataMovimentacao : DateTime.Now;
                listaProvisao.Add(pmbac);
            }

            return listaProvisao.AsEnumerable();
        }
    }
}

using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Calculadores
{
    public class CalculadorProvisaoMatematicaBeneficioAConceder
    {

        private readonly CoberturaContratada _coberturacontratada;
        private readonly ICalculoFacade _calculo;
        private readonly EventoOperacional _eventooperacional;
        private readonly ParametrosProvisaoCalculo _parametrosProvisaoCalculo;
        private readonly IProvisoes _provisao;
        public CalculadorProvisaoMatematicaBeneficioAConceder(CoberturaContratada coberturacontratada, ICalculoFacade calculo,EventoOperacional eventooperacional, ParametrosProvisaoCalculo parametrosProvisaoCalculo, IProvisoes provisao)
        {
            _coberturacontratada = coberturacontratada;
            _calculo = calculo;
            _eventooperacional = eventooperacional;
            _parametrosProvisaoCalculo = parametrosProvisaoCalculo;
            _provisao = provisao;

        }

        public virtual IEnumerable<ProvisaoDto> CalcularProvisao()
        {
            var dataExecucao = _eventooperacional.DataExecucaoEvento;
            var competenciaCalculo = dataExecucao.Date.AddDays(1 - dataExecucao.Day);
            var valorUltimaProvisao = ObterUltimoValorPMBAC();
            var provisionavel = _coberturacontratada.TipoItemProdutoId != (short)TipoItemProdutoEnum.VGBL
                             && _coberturacontratada.TipoItemProdutoId != (short)TipoItemProdutoEnum.PGBL;

            var listaProvisao = new List<ProvisaoDto>();

            if (provisionavel)
            {
                var pmbac = _calculo.CalcularPMBAC(_coberturacontratada, competenciaCalculo, valorUltimaProvisao);

                pmbac.ProvisaoId = (short)TipoProvisaoEnum.PMBAC;
                pmbac.DataMovimentacao = pmbac.DataMovimentacao != default(DateTime) ? pmbac.DataMovimentacao : DateTime.Now;
                listaProvisao.Add(pmbac);
            }

            return listaProvisao.AsEnumerable();
        }


        public decimal ObterUltimoValorPMBAC()
        {
            var movimento = _provisao.ObterUltimoMovimento(_parametrosProvisaoCalculo.ItemCertificadoApoliceId, _parametrosProvisaoCalculo.tipoprovisao).Result;
            return movimento != null ? movimento.ValorProvisao : default(decimal);
        }


    }
}

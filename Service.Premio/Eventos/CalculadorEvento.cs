using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Service.Premio.Calculadores;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class CalculadorEvento : ICalculadorEvento
    { 
        private CoberturaContratada _coberturaContratada;
        private EventoOperacional _eventooperacional;
        private ICalculoFacade _calculo;
        private IProvisoes _provisao;
        public List<MovimentoProvisao> movimentosProvisao = new List<MovimentoProvisao>();

        public CalculadorEvento(CoberturaContratada coberturaContratada, EventoOperacional eventooperacional,ICalculoFacade calculo, IProvisoes provisao)
        {
            _coberturaContratada = coberturaContratada;
            _eventooperacional = eventooperacional;
            _calculo = calculo;
            _provisao = provisao;

        }

        public void CalcularProvisao()
        {
            CalculadorProvisaoPovider calculador;
            IEnumerable<CalculadorProvisaoMatematicaBeneficioAConceder> calculadores = null;
            foreach (var provisaocobertura in _coberturaContratada.ProvisaoCobertura)
            {
                switch ((TipoProvisaoEnum)provisaocobertura.TipoProvisaoId)
                {
                    case TipoProvisaoEnum.PMBAC:
                    {
                            calculador = new CalculadorProvisaoPMBACProvider(_coberturaContratada, _calculo, _eventooperacional, _provisao);
                            calculadores = calculador.ObterCalculadoresProvisao();
                    }
                        break;

                   
                    default:
                        break;
                }

                foreach (var calcula in calculadores)
                {
                    AdicionarProvisoes(calcula.CalcularProvisao().ToList());
                }
            }
            
        }

        public void AdicionarProvisoes(List<ProvisaoDto> provisoes)
        {
            foreach (var provisao in provisoes)
            {
                movimentosProvisao.Add(new MovimentoProvisao()
                    .ComProvisao(provisao)
                    .ComQuantidadeContribuicoes(_coberturaContratada.NumeroContribuicao));
            }
        }
    }
}

using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Factories
{
    public class CalculadorProvisaoPremio : ICalculadorProvisaoPremio
    {
        private CalculadorProvisaoPremioNaoGanhoPremio _calculaPPNG;
        private CalculadorProvisaoMatematicaBeneficioAConceder _calculaPMBAC;
        private readonly IProvisoesService _provisao;

        List<MovimentoProvisao> movimentosProvisao = new List<MovimentoProvisao>();

        public CalculadorProvisaoPremio(
            CalculadorProvisaoPremioNaoGanhoPremio calculaPPNG,
            CalculadorProvisaoMatematicaBeneficioAConceder calculaPMBAC,
            IProvisoesService provisao)
        {
            _calculaPPNG = calculaPPNG;
            _calculaPMBAC = calculaPMBAC;
            _provisao = provisao;
        }

        public virtual IEnumerable<MovimentoProvisao> CriarProvisao(Premio premio)
        {
            var tiposProvisaoPossiveis = premio.ObterTiposProvisaoPossiveis();

            foreach (var provisao in tiposProvisaoPossiveis)
            {
                switch (provisao)
                {
                    case TipoProvisaoEnum.PPNG:
                        {
                            if (premio.EventoOperacional.TipoEvento.Equals(TipoEventoEnum.ApropriacaoPremio))
                                return new List<MovimentoProvisao>();
                            AdicionarProvisoes(_calculaPPNG.CalcularProvisao(premio).ToList(), premio);
                        }
                        break;
                    case TipoProvisaoEnum.PMBAC:
                        {
                            var valorUltimaProvisao = ObterUltimoValorPMBAC(premio);
                            AdicionarProvisoes(_calculaPMBAC.CalculaProvisao(premio, valorUltimaProvisao).ToList(), premio);
                        }
                        break;
                }
            }

            return movimentosProvisao;
        }

        public decimal ObterUltimoValorPMBAC(Premio premio)
        {
            var movimento = _provisao.ObterUltimoMovimentoProvisao(premio, TipoProvisaoEnum.PMBAC).Result;
            return movimento != null? movimento.ValorProvisao : default(decimal);
        }

        public void AdicionarProvisoes(List<ProvisaoDto> provisoes, Premio premio)
        {
            foreach (var provisao in provisoes)
            {
                movimentosProvisao.Add(new MovimentoProvisao()
                    .ComProvisao(provisao)
                    .ComQuantidadeContribuicoes(premio.Cobertura.NumeroContribuicao));
            }
        }
    }
}

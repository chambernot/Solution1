using System.Collections.Generic;
using System.Linq;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.ESB.Produto.Contrato.v2.Dados;
using Mongeral.ESB.Produto.Contrato.v2.Operacoes;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Adapters
{
    internal class ProdutoAdapter: IProdutoAdapter
    {
        private readonly IProdutoService _produtoService;
        private readonly ICalculoService _calculoService;

        public ProdutoAdapter(IProdutoService produtoService, ICalculoService calculoService)
        {
            _produtoService = produtoService;
            _calculoService = calculoService;
        }

        public virtual DadosProduto ObterDadosProduto(ChaveProduto key)
        {
            var itemProduto = _produtoService.ObterItensDoCadastroPorIds(key.DataVigencia, new List<int>() {key.ItemProdutoId}.ToArray()).Valor?.First();

            Assertion.NotNull(itemProduto,$"Não foi encontrado cadastro do item produto {key.ItemProdutoId} vigente em {key.DataVigencia:yyyy-MM--dd}").Validate();

            var ambienteProvisao = _calculoService.ObterAmbienteDeProvisoes(new List<ParametrosAmbienteDeProvisao>() {ObterParametrosDeProvisao(key)});

            Assertion.IsTrue(ambienteProvisao.Valor.Any(), $"Não foram encontrado informações para o ItemProduto: {key.ItemProdutoId}, " +
                $"com a data de vigencia: {key.DataVigencia.ToString("dd/MM/yyyy")} e TipoFormaContrataca: {key.TipoFormaContratacaoId} .").Validate();
            
            return ambienteProvisao.Valor.Select(dto => new DadosProduto
            {
                ItemProdutoId = dto.CarregamentoDoItemProduto.ItemProdutoId,
                TipoFormaContratacaoId = key.TipoFormaContratacaoId,
                DataVigenciaCobertura = key.DataVigencia,
                DataInicioVigencia = itemProduto.DataDaVigencia,
                DataFimVigencia = itemProduto.DataFimVigencia,
                ProdutoId = dto.CarregamentoDoItemProduto.ProdutoId,
                NomeProduto = dto.CarregamentoDoItemProduto.NomeProduto,                
                NumeroProcessoSusep = dto.CarregamentoDoItemProduto.NumeroProcessoSusep,
                RegimeFinanceiroId = dto.CarregamentoDoItemProduto.RegimeFinanceiroId,
                TipoItemProdutoId = dto.CarregamentoDoItemProduto.TipoItemProdutoId,
                ModalidadeCoberturaId = dto.CarregamentoDoItemProduto.ModalidadeDaCoberturaId,
                IndiceBeneficioId = dto.CarregamentoDoItemProduto.IndiceBeneficioId,
                IndiceContribuicaoId = dto.CarregamentoDoItemProduto.IndiceContribuicaoId,                
                PermiteResgateParcial = dto.PermiteResgateParcial,       
                NumeroBeneficioSusep = dto.CarregamentoDoItemProduto.NumeroBeneficioSusep,
                ProvisoesPossiveis = itemProduto.EhServico ? (int)TipoProvisaoEnum.Nenhuma : ProvisoesAConstituir(dto)                
            }).FirstOrDefault();
        }        

        private ParametrosAmbienteDeProvisao ObterParametrosDeProvisao(ChaveProduto key)
        {
            return new ParametrosAmbienteDeProvisao
            {                
                ItemProdutoId = key.ItemProdutoId,                
                TipoFormaContratacaoId = (short)key.TipoFormaContratacaoId,
                TipoRendaId =  key.TipoRendaId,
                DataVigencia = key.DataVigencia                
            };
        }
        
        private int ProvisoesAConstituir(AmbienteDeProvisao ambienteProvisao)
        {
            TipoProvisaoEnum provisoesPossiveis = TipoProvisaoEnum.Nenhuma;

            Assertion.NotNull(ambienteProvisao.ProvisoesAConstituir,
                $"Não foi retornada Provisão a Constituir com os parâmetros informados. Item produto id: {ambienteProvisao.ItemProdutoId}, " +
                $"forma contratação id: {ambienteProvisao.FormaContratacaoId}, data de vigência: {ambienteProvisao.DataVigencia.ToString("yyyyMMdd")}").Validate();

            Assertion.NotNull(ambienteProvisao.ProvisoesAConstituir.RegraReserva,
                    $"A Regra de Reversa, para o itemProduto { ambienteProvisao.ItemProdutoId } não pode ser nulo.").Validate();

            Assertion.NotNull(ambienteProvisao.ProvisoesAConstituir.RegraReserva.Tipos,
                    $"Os Tipos de Regra de Reversa, para o itemProduto { ambienteProvisao.ItemProdutoId } não pode ser nulo.").Validate();

            foreach (var provisao in ambienteProvisao.ProvisoesAConstituir.RegraReserva.Tipos)
            {
                Assertion.NotNull(provisao.TipoReserva, $"O Tipo de Reversa, para o itemProduto { ambienteProvisao.ItemProdutoId } não pode ser nulo.").Validate();

                var tipoProvisaoId = MapearProvisoes(provisao.TipoReserva.Id);

                Assertion.IsTrue(System.Enum.IsDefined(typeof(TipoProvisaoEnum), (int)tipoProvisaoId),
                        $"Erro ao obter lista de provisões a constituir: não existe configuração de TipoProvisaoEnum para o id { provisao.TipoReserva.Id } na regra de provisões em produto.").Validate();

                provisoesPossiveis = provisoesPossiveis | tipoProvisaoId;
            }            

            return (int)provisoesPossiveis;
        }

        private TipoProvisaoEnum MapearProvisoes(int tipoReservaId)
        {
            switch (tipoReservaId)
            {
                case 1:
                    return TipoProvisaoEnum.PMBAC;
                case 2:
                    return TipoProvisaoEnum.PROVR;
                case 3:
                    return TipoProvisaoEnum.PMBC;
                case 4:
                    return TipoProvisaoEnum.PSL;
                case 5:
                    return TipoProvisaoEnum.PBAR;
                case 6:
                    return TipoProvisaoEnum.PCP;
                case 7:
                    return TipoProvisaoEnum.PDA;
                case 8:
                    return TipoProvisaoEnum.PEF;
                case 10:
                    return TipoProvisaoEnum.PIC;
                case 12:
                    return TipoProvisaoEnum.PPNG;
                case 14:
                    return TipoProvisaoEnum.PRNE;
                case 16:
                    return TipoProvisaoEnum.IBNR;
                case 18:
                    return TipoProvisaoEnum.POR;
                case 19:
                    return TipoProvisaoEnum.PortabilidadeExterna;
                default:
                    return TipoProvisaoEnum.Nenhuma;
            }
        }        
    }
}

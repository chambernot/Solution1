using System;
using System.Collections.Generic;
using Mongeral.ESB.Infraestrutura.Contrato;
using Mongeral.ESB.Produto.Contrato.v2.Dados;
using Mongeral.ESB.Produto.Contrato.v2.Operacoes;

namespace Mongeral.Provisao.V2.Adapters.SvcImplementation
{
    public class ProdutoService : IProdutoService
    {
        public ResultadoDaOperacao<Produto> ObterUmProdutoCadastrado(DateTime dataVigencia, int produtoId)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<Produto>> ObterProdutosCadastrados(DateTime dataVigencia, int[] produtoIds)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<ItemProduto>> ObterItensDosProdutos(DateTime dataVigencia, int[] ids)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<ItemProduto>> ObterItensDoCadastroPorIds(DateTime dataVigencia, int[] ids)
        {
            return WcfClient<IProdutoService>.CallUsing(x => x.ObterItensDoCadastroPorIds(dataVigencia,ids));
        }

        public ResultadoDaOperacao<List<ItemProduto>> ObterItensDoCadastroPorCodigosSysPrev(DateTime dataVigencia, DeParaESimSysPrev[] codigos)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<DeParaESimSysPrev> ObterDePara(DeParaESimSysPrev dePara)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<DeParaESimSysPrev>> ObterVariosDePara(DeParaESimSysPrev[] dePara)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<Produto>> ObterProdutosDoVG()
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<AmbienteDeCalculo> ObterAmbienteDeCalculo(ParametrosAmbienteDeCalculo parametro)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<AcaoDeMarketing>> ObterAcoesDeMarketing()
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<ProdutosAtivosParaComercializacao>> ObterProdutosAtivosParaComercializacao()
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<ItensProdutosAtivosParaComercializacao>> ObterItensProdutosAtivosParaComercializacaoDaNegociacao(int modeloRelacioamentoId)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<Produto>> ObterProdutosNasVigencias(IdentificacaoDoProdutoNaLinhaDeTempo[] identidades)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<Servico>> ObterServicoAssistencialCondicionado(ParametroServicoAssistencialCondicionado parametro)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<ServicoSorteio> ObterCustoServicoSorteio(ParametrosCustoServicoSorteio parametro)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<GrupoProduto>> ObterGruposDeProduto(ParametrosObterGruposDeProduto parametro)
        {
            throw new NotImplementedException();
        }

        public ResultadoDaOperacao<List<Produto>> ObterProdutosQueNaoPermitemAjusteDeParcela()
        {
            throw new NotImplementedException();
        }
    }
}
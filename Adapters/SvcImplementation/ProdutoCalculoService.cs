using System.Collections.Generic;
using Mongeral.ESB.Infraestrutura.Contrato;
using Mongeral.ESB.Produto.Contrato.v2.Dados;
using Mongeral.ESB.Produto.Contrato.v2.Operacoes;

namespace Mongeral.Provisao.V2.Adapters.SvcImplementation
{
    public class ProdutoCalculoService : ICalculoService
    {
        public ResultadoDaOperacao<AmbienteDeCalculo> ObterAmbienteDeCalculo(ParametrosAmbienteDeCalculo parametro)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDaOperacao<List<AmbienteDeCalculo>> ObterAmbientesDeCalculo(List<ParametrosAmbienteDeCalculo> parametros)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDaOperacao<CarregamentoDoItemProduto> ObterCarregamentoDoItemProduto(ParametrosCarregamento parametro)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDaOperacao<List<CarregamentoDoItemProduto>> ObterCarregamentoDosItensDeProduto(List<ParametrosCarregamento> parametros)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDaOperacao<List<AmbienteDeProvisao>> ObterAmbienteDeProvisoes(List<ParametrosAmbienteDeProvisao> parametros)
        {
            return WcfClient<ICalculoService>.CallUsing(svc=>svc.ObterAmbienteDeProvisoes(parametros));
        }

        public ResultadoDaOperacao<List<ValoresCalculados>> CalcularValorContribuicaoBeneficio(List<ParametrosCalculo> parametros)
        {
            throw new System.NotImplementedException();
        }

        public ResultadoDaOperacao<List<ValoresCalculadosPMBAC>> ObterDadosProjecaoPMBAC(List<ParametrosCalculo> parametros)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Linq;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class ProdutoBuilder : MockBuilder<IProduto>
    {   
        public static ProdutoBuilder UmProduto() { return new ProdutoBuilder(); }

        public ProdutoBuilder Com(params CoberturaBuilder[] cobertura)
        {
            Mock.SetupGet(x => x.Coberturas).Returns(cobertura.Select(c => c.Build()).ToList());
            return this;
        }

        public ProdutoBuilder Com(params BeneficiarioBuilder[] beneficiarios)
        {
            Mock.SetupGet(x => x.Beneficiarios).Returns(beneficiarios.Select(b => b.Build()).ToList());
            return this;
        }
        public ProdutoBuilder ComCodigo(int codigo)
        {
            Mock.SetupGet(x => x.Codigo).Returns(codigo);
            return this;
        }

        public ProdutoBuilder ComInscricao(long inscricao)
        {
            Mock.SetupGet(x => x.InscricaoCertificado).Returns(inscricao);
            return this;
        }

        public ProdutoBuilder ComMatricula(long matricula)
        {
            Mock.SetupGet(x => x.Matricula).Returns(matricula);
            return this;
        }

        public ProdutoBuilder Padrao()
        {
            ComMatricula(IdentificadoresPadrao.Matricula);
            ComCodigo(IdentificadoresPadrao.ProdutoId);
            ComInscricao(IdentificadoresPadrao.InscricaoId);
            Com(CoberturaBuilder.UmaCobertura().Padrao());
            Com(BeneficiarioBuilder.UmBeneficiario().Padrao());
            return this;
        }
    }
}

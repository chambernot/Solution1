using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class ProdutoExtensions
    {
        public static IAssertion Validar(this IProduto produto)
        {
            var certificado = Assertion.IsFalse(produto.InscricaoCertificado == default(long), "Não foi informado o numero de certificado / inscrição");
            var produtoId = Assertion.IsFalse(produto.Codigo == default(int), $"Não foi informado o produtoId para a inscricao {produto.InscricaoCertificado}");
            var coberturasNull = Assertion.NotNull(produto.Coberturas, $"A lista de coberturas da inscrição {produto.InscricaoCertificado} não foi informada");
            var coberturasVazia = coberturasNull.IsValid()
                ? Assertion.IsTrue(produto.Coberturas.Any(), $"A lista de coberturas da inscrição {produto.InscricaoCertificado} está vazia")
                : Assertion.Neutral();
            var coberturas = coberturasNull.IsValid()
                ? produto.Coberturas.Select(c => c.Validar(produto.InscricaoCertificado)).Aggregate(Assertion.Neutral(), (x, y) => x.and(y))
                : Assertion.Neutral();
            var matricula = Assertion.IsFalse(produto.Matricula.Equals(default(long)), $"Não foi informada a matricula da inscrição {produto.InscricaoCertificado}");

            return certificado.and(produtoId).and(coberturasNull).and(coberturasVazia).and(matricula);
        }
    }
}

using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class PropostaExtensions
    {
        public static void Validar(this IProposta proposta)
        {
            var evento = proposta.ValidarEvento();

            var proponente = proposta.Proponente.Validar(proposta.Numero);

            var dataImplantacao = Assertion.IsFalse(proposta.DataImplantacao.Equals(default(DateTime)), $"Proposta com Data de implantação inválida. Número da Proposta: {proposta.Numero}.");
            var dataAssinatura = Assertion.IsFalse(proposta.DataAssinatura.Equals(default(DateTime)), $"Proposta com Data de assinatura inválida. Número da Proposta: {proposta.Numero}.");
            var numero = Assertion.IsFalse(proposta.Numero.Equals(default(int)), $"Número da proposta inválido. Identificador: {proposta.Identificador}.");
            var produtosNull = Assertion.NotNull(proposta.Produtos, $"A proposta {proposta.Numero} não tem produtos informados");
            var produtosEmpty = produtosNull.IsValid()
                ? Assertion.IsTrue(proposta.Produtos.Any(), $"A proposta {proposta.Numero} tem lista de produtos vazia")
                : Assertion.Neutral();
            var produtos = produtosNull.IsValid()
                ? proposta.Produtos.Select(p => p.Validar()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y))
                : Assertion.Neutral();            

            evento.and(proponente)
                .and(dataImplantacao)
                .and(dataAssinatura)                
                .and(numero)
                .and(produtosNull)
                .and(produtosEmpty)
                .and(produtos)
                .Validate();            
        }        
    }    
}

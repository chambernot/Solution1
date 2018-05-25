using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioBuilder: BaseBuilder<Premio>
    {
        public PremioBuilder()
        {
            Instance = new Premio();
        }

        public static PremioBuilder Um()
        {
            return new PremioBuilder();
        }

        public PremioBuilder ComItemCertificadoApolice(long itemCertificado)
        {
            Instance.ItemCertificadoApoliceId = itemCertificado;
            return this;
        }

        public PremioBuilder ComDataCompetencia(DateTime competencia)
        {
            Instance.Competencia = competencia;
            return this;
        }

        public PremioBuilder ComInicioVigencia(DateTime inicioVigencia)
        {
            Instance.InicioVigencia = inicioVigencia;
            return this;
        }

        public PremioBuilder ComFimVigencia(DateTime fimVigencia)
        {
            Instance.FimVigencia = fimVigencia;
            return this;
        }

        public PremioBuilder ComValorPremio(decimal valor)
        {
            Instance.ValorPremio = valor;
            return this;
        }

        public PremioBuilder ComValorBeneficio(decimal valor)
        {
            Instance.ValorBeneficio = valor;
            return this;
        }
        public PremioBuilder ComValorCarregamento(decimal valor)
        {
            Instance.ValorCarregamento = valor;
            return this;
        }
        public PremioBuilder ComValorCapitalSegurado(decimal valor)
        {
            Instance.ValorCapitalSegurado = valor;
            return this;
        }

        public PremioBuilder ComTipoMovimento(short tipomovimento)
        {
            Instance.TipoMovimentoId = tipomovimento;
            return this;
        }

        public PremioBuilder ComNumeroParcela(int numero)
        {
            Instance.Numero = numero;
            return this;
        }
        
        public PremioBuilder Com(PagamentoPremioBuilder pagamento)
        {
            Instance.ComPagamento(pagamento.Build());
            return this;
        }

        public PremioBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            Instance.ComCobertura(cobertura.Build());
            return this;
        }

        public PremioBuilder Com(params MovimentoProvisaoBuilder[] movimento)
        {
            Instance.AdicionarMovimentoProvisao(movimento.Select(x => x.Build()));
            return this;
        }        
        
        public PremioBuilder Padrao()
        {
            ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId);
            ComDataCompetencia(IdentificadoresPadrao.Competencia);
            ComInicioVigencia(IdentificadoresPadrao.DataInicioVigencia);
            ComFimVigencia(IdentificadoresPadrao.DataFimVigencia);
            ComValorPremio(IdentificadoresPadrao.ValorContribuicao);
            ComValorBeneficio(IdentificadoresPadrao.ValorBeneficio);
            ComValorCarregamento(IdentificadoresPadrao.ValorCarregamento);
            ComValorCapitalSegurado(IdentificadoresPadrao.ValorCapitalSegurado);
            ComNumeroParcela(IdentificadoresPadrao.NumeroParcela);
            ComTipoMovimento((short)TipoMovimentoEnum.Emissao);
            return this;
        }
    }
}

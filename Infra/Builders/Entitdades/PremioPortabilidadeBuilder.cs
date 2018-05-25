using System;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioPortabilidadeBuilder : PremioBuilder<PremioPortabilidade>
    {
        private PremioPortabilidadeBuilder() : base()
        {
            Instance = new PremioPortabilidade()
            {
                TipoMovimentoId = (short)TipoMovimentoEnum.Portabilidade
            };

        }
        public static PremioPortabilidadeBuilder Um()
        {
            return new PremioPortabilidadeBuilder();
        }

        public PremioPortabilidadeBuilder ComParcela(int parcela)
        {
            Instance.Numero = parcela;
            return this;
        }

        public PremioPortabilidadeBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            ComCoberturaBuilder(cobertura);
            return this;
        }

        public PremioPortabilidadeBuilder Com(MovimentoProvisaoBuilder movimento)
        {
            ComMovimentoBuilder(movimento);
            return this;
        }

        public PremioPortabilidadeBuilder Com(PagamentoPremioBuilder pagamento)
        {
            Instance.Pagamento = pagamento.Build();
            return this;
        }

        public PremioPortabilidadeBuilder Padrao()
        {
            ComParcela(IdentificadoresPadrao.NumeroParcela);
            ComPremioPadrao();
            return this;
        }
    }
}

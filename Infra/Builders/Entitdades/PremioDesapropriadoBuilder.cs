using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Enum;
using System;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioDesapropriadoBuilder: PremioBuilder<PremioDesapropriado>
    {
        private PremioDesapropriadoBuilder(): base()
        {
            Instance = new PremioDesapropriado
            {
                TipoMovimentoId = (short)TipoMovimentoEnum.Desapropriacao
            };
        }

        public static PremioDesapropriadoBuilder Um()
        {
            return new PremioDesapropriadoBuilder();
        }

        public PremioDesapropriadoBuilder ComParcela(int parcela)
        {
            Instance.Numero = parcela;
            return this;
        }

        public PremioDesapropriadoBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            ComCoberturaBuilder(cobertura);
            return this;
        }

        public PremioDesapropriadoBuilder Com(MovimentoProvisaoBuilder movimento)
        {
            ComMovimentoBuilder(movimento);
            return this;
        }

        public PremioDesapropriadoBuilder Com(PagamentoPremioBuilder pagamento)
        {
            Instance.Pagamento = pagamento.Build();
            return this;
        }


        public PremioDesapropriadoBuilder Padrao()
        {
            ComParcela(IdentificadoresPadrao.NumeroParcela);
            ComPremioPadrao();
            return this;
        }
    }
}

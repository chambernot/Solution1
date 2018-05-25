using System;
using Mongeral.Provisao.V2.Domain.Entidades.Premios;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioAporteBuilder: PremioBuilder<PremioAporte>
    {
        private PremioAporteBuilder() : base()
        {
            Instance = new PremioAporte()
            {
                TipoMovimentoId = (short)TipoMovimentoEnum.Aporte
            };

        }
        public static PremioAporteBuilder Um()
        {
            return new PremioAporteBuilder();
        }

        public PremioAporteBuilder ComParcela(int parcela)
        {
            Instance.Numero = parcela;
            return this;
        }

        public PremioAporteBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            ComCoberturaBuilder(cobertura);
            return this;
        }

        public PremioAporteBuilder Com(MovimentoProvisaoBuilder movimento)
        {
            ComMovimentoBuilder(movimento);
            return this;
        }

        public PremioAporteBuilder Com(PagamentoPremioBuilder pagamento)
        {
            Instance.Pagamento = pagamento.Build();
            return this;
        }

        public PremioAporteBuilder Padrao()
        {
            ComParcela(IdentificadoresPadrao.NumeroParcela);
            ComPremioPadrao();
            return this;
        }

    }
}

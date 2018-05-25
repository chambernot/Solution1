using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioApropriadoBuilder: PremioBuilder<PremioApropriado>
    {
        private PremioApropriadoBuilder(): base()
        {
            Instance = new PremioApropriado { TipoMovimentoId = (short) TipoMovimentoEnum.Apropriacao };
        }

        public static PremioApropriadoBuilder Um()
        {
            return new PremioApropriadoBuilder();            
        }

        
        public PremioApropriadoBuilder ComParcela(int parcela)
        {
            Instance.Numero = parcela;
            return this;
        }

        public PremioApropriadoBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            ComCoberturaBuilder(cobertura);
            return this;
        }

        public PremioApropriadoBuilder Com(params MovimentoProvisaoBuilder[] movimento)
        {
            ComMovimentoBuilder(movimento);
            return this;
        }

        

        public PremioApropriadoBuilder Padrao()
        {
            ComParcela(IdentificadoresPadrao.NumeroParcela);
            ComPremioPadrao();
            return this;
        }
    }
}
using Mongeral.Integrador.Contratos;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class PrazosBuilder: MockBuilder<IPrazos>
    {
        public static PrazosBuilder Um()
        {
            return new PrazosBuilder();
        }

        public PrazosBuilder ComCoberturaEmAnos(int prazo)
        {
            Mock.SetupGet(x => x.CoberturaEmAnos).Returns(prazo);
            return this;
        }
        public PrazosBuilder ComDecrescimoEmAnos(int prazo)
        {
            Mock.SetupGet(x => x.DecrescimoEmAnos).Returns(prazo);
            return this;
        }
        public PrazosBuilder ComPagamentoEmAnos(int prazo)
        {
            Mock.SetupGet(x => x.PagamentoEmAnos).Returns(prazo);
            return this;
        }

        public PrazosBuilder Padrao()
        {
            ComCoberturaEmAnos(IdentificadoresPadrao.PrazoCobertura);
            ComDecrescimoEmAnos(IdentificadoresPadrao.PrazoDecrescimo);
            ComPagamentoEmAnos(IdentificadoresPadrao.PrazoPagamento);
            return this;
        }
    }
}

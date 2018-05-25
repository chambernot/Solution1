using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaIdBuilder : MockBuilder<IParcelaId>
    {   
        public static ParcelaIdBuilder UmBuilder()
        {
            return new ParcelaIdBuilder();
        }

        public ParcelaIdBuilder ComParcelaId(long parcelaId)
        {
            Mock.SetupGet(x => x.ParcelaId).Returns(parcelaId);
            return this;
        }

        public ParcelaIdBuilder ComNumeroParcela(int numero)
        {
            Mock.SetupGet(x => x.NumeroParcela).Returns(numero);
            return this;
        }

        public ParcelaIdBuilder ComNumeroEndosso(int numeroEndosso)
        {
            Mock.SetupGet(x => x.NumeroEndosso).Returns(numeroEndosso);
            return this;
        }

        public ParcelaIdBuilder ComIdentificadorExternoCobertura(string identificadorExterno)
        {
            Mock.SetupGet(x => x.IdentificadorExternoCobertura).Returns(identificadorExterno);
            return this;
        }

        public ParcelaIdBuilder Padrao()
        {
            ComNumeroParcela(IdentificadoresPadrao.NumeroParcela);
            ComNumeroEndosso(IdentificadoresPadrao.SequencialEndosso);
            ComParcelaId(IdentificadoresPadrao.ParcelaId);
            ComIdentificadorExternoCobertura(IdentificadoresPadrao.ItemCertificadoApoliceId.ToString());
            return this;
        }
    }
}

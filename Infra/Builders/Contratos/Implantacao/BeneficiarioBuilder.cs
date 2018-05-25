using System;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class BeneficiarioBuilder : MockBuilder<IBeneficiario>
    {
        public static BeneficiarioBuilder UmBeneficiario()
        {
            return new BeneficiarioBuilder();
        }

        public BeneficiarioBuilder ComDataNascimento(DateTime dataNascimento)
        {
            Mock.SetupGet(x => x.DataNascimento).Returns(dataNascimento);
            return this;
        }

        public BeneficiarioBuilder ComSexo(string sexo)
        {
            Mock.SetupGet(x => x.Sexo).Returns(sexo);
            return this;
        }

        public BeneficiarioBuilder Padrao()
        {
            ComDataNascimento(IdentificadoresPadrao.DataNascimento);
            ComSexo(IdentificadoresPadrao.Sexo);
            return this;
        }
    }
}

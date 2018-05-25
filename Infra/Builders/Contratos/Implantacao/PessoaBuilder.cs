using System;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class PessoaBuilder: MockBuilder<IPessoa>
    {
        public static PessoaBuilder UmaPessoa() { return new PessoaBuilder(); }

        public PessoaBuilder ComMatricula(long matricula)
        {
            Mock.SetupGet(p => p.Matricula).Returns(matricula);
            return this;
        }

        public PessoaBuilder ComSexo(string sexo)
        {
            Mock.SetupGet(p => p.Sexo).Returns(sexo);
            return this;
        }

        public PessoaBuilder ComDataNascimento(DateTime dataNascimento)
        {
            Mock.SetupGet(p => p.DataNascimento).Returns(dataNascimento);
            return this;
        }

        public PessoaBuilder Padrao()
        {
            ComMatricula(IdentificadoresPadrao.Matricula);
            ComSexo(IdentificadoresPadrao.Sexo);
            ComDataNascimento(IdentificadoresPadrao.DataNascimento);
            return this;
        }
    }
}

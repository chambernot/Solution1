using System;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class ProponenteBuider: MockBuilder<IProponente>
    {   
        public static ProponenteBuider UmProponente() { return new ProponenteBuider(); }
        
        public ProponenteBuider ComMatricula(long matricula)
        {
            Mock.SetupGet(x => x.Matricula).Returns(matricula);
            return this;
        }

        public ProponenteBuider ComSexo(string sexo)
        {
            Mock.SetupGet(x => x.Sexo).Returns(sexo);
            return this;
        }

        public ProponenteBuider ComDataNascimento(DateTime dataNascimento)
        {
            Mock.SetupGet(x => x.DataNascimento).Returns(dataNascimento);
            return this;
        }

        public ProponenteBuider Com(PessoaBuilder pessoa)
        {
            Mock.SetupGet(p => p.Conjuge).Returns(pessoa.Build());
            return this;
        }

        public ProponenteBuider Padrao()
        {
            ComMatricula(IdentificadoresPadrao.Matricula);
            ComSexo(IdentificadoresPadrao.Sexo);
            ComDataNascimento(IdentificadoresPadrao.DataNascimento);
            return this;
        }
    }
}

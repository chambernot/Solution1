using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using GreenPipes;
using Ninject;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Filters
{
    [TestFixture]
    public class ConversaoApropriacaoPremioFilterTest: UnitTesBase
    {
        private ApropriacaoPremioContext _contexto;
        private IParcelaApropriada _apropriacao;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _apropriacao = ParcelaApropriadaBuilder.UmBuilder().Padrao().Build();

            var lista = new List<PremioApropriado>
            {
                PremioApropriadoBuilder.Um().Padrao().Build()
            };

            _contexto = new ApropriacaoPremioContext(_apropriacao)
            {
                Premios = lista.AsEnumerable()
            };                        

            var pipeline = Pipe.New<ApropriacaoPremioContext>(cfg =>
            {
                cfg.AddFilter(() => MockingKernel.Get<ConversaoApropriacaoPremioFilter>());
            });

            pipeline.Send(_contexto);
        }
    }
}

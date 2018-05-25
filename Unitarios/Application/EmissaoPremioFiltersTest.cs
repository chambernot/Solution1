using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Application.Filters;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Extensions;
using Mongeral.Provisao.V2.Service.Premio.Filters;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades;
using Ninject;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Contratos.Premio;
using static Mongeral.Provisao.V2.Domain.Dominios.StatusCobertura;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Application
{
    [TestFixture]
    public class EmissaoPremioFiltersTest: UnitTesBase
    {
        private EmissaoPremioContext _contexto;
        private IParcelaEmitida _premio;        

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            var movimentoProvisao = MovimentoProvisaoBuilder.UmBuilder().Padrao().Build();

            var cobertutaAtiva = CoberturaContratadaBuilder.Uma().ComStatus(StatusCoberturaEnum.Activa).ComRegimeFinanceiro((short)TipoRegimeFinanceiroEnum.ReparticaoSimples).Build();

            _premio = ParcelaEmitidaBuilder.UmBuilder().Padrao().Build();

            _contexto = new EmissaoPremioContext(_premio)
            {
                Premios = _premio.ToEventoPremio()
            };

            MockingKernel.GetMock<IPremios>()
                .Setup(x => x.VerificarUltimoPremio(long.MinValue, short.MinValue, int.MinValue)).Returns(Task.FromResult(false));

            MockingKernel.GetMock<ICoberturas>()
                .Setup(x => x.ObterPorItemCertificado(It.IsAny<long>())).Returns(Task.FromResult(cobertutaAtiva));

            MockingKernel.GetMock<ICalculadorProvisaoPremio>()
                .Setup(c => c.CriarProvisao(It.IsAny<Premio>(), TipoEventoEnum.EmissaoPremio)).Returns(new List<MovimentoProvisao>() { movimentoProvisao }.AsEnumerable());

            var pipeline = GreenPipes.Pipe.New<EmissaoPremioContext>(cfg =>
            {
                cfg.AddFilter(() => MockingKernel.Get<EmissaoPremioFilter>());                
            });
            
            pipeline.Send(_contexto).Wait();
        }

        [Test]
        public void DadoUmPremioEmitidoDeveAdicionarOsPremiosNoEvento()
        {
            Assert.That(_contexto.Evento.Premios.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DadoUmPremioEmitidoDeveAdicionarOMovimentoProvisaoPPNGNoPremio()
        {
            var vigencia = _premio.Parcelas.First().Vigencia;

            var qtdCompetencias = 1;

            Assert.That(_contexto.Evento.Premios.First().MovimentosProvisao.Count(), Is.EqualTo(qtdCompetencias));
        }
    }
}

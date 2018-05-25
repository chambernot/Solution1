using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.Infra.Util;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain.Interfaces;
using static Mongeral.Provisao.V2.Domain.Dominios.StatusCobertura;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DAL.Persistir;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public class ProcessarAlteracaoParametrosServiceTest: ImplantacaoBaseTest
    {
        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            ProcessarImplatacao();
        }

        [Test]        
        public async Task DadoUmEventoDeveAtualizarOsDadosDeCobertura()
        {
            var proposta = new JsonToProposta().ObterProposta(false, false);

            GetInstance<AlteracaoPropostaService>().Execute(proposta).Wait();

            var produtos = proposta.Produtos.First();
            var dto = await GetInstance<ICoberturas>().ObterPorItemCertificado(long.Parse(produtos.Coberturas.First().IdentificadorExterno));           

            Assert.That(produtos.Beneficiarios.First().DataNascimento, Is.EqualTo(dto.Historico.DataNascimentoBeneficiario));
            Assert.That(produtos.Beneficiarios.First().Sexo, Is.EqualTo(dto.Historico.SexoBeneficiario));
            Assert.That((int)proposta.DadosPagamento.Periodicidade, Is.EqualTo(dto.Historico.PeriodicidadeId));
            Assert.That(dto.Status, Is.EqualTo((int)(StatusCoberturaEnum.Activa)));
        }

        [Test]
        public async Task AoCompensarDeveExcluirOEvento()
        {
            var proposta = new JsonToProposta().ObterProposta(false, false);

            GetInstance<AlteracaoPropostaService>().Execute(proposta).Wait();

            await GetInstance<AlteracaoPropostaService>().Compensate(proposta);

            var dto = await GetInstance<Eventos<EventoImplantacao>>().Contem(proposta.Identificador);

            Assert.IsFalse(dto);
        }
    }
}

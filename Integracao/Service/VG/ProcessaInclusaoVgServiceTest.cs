using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Util;
using NUnit.Framework;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service.VG
{
    [TestFixture]
    public class InclusaoVgBaseTest : IntegrationTestBase
    {
        protected IInclusaoCoberturaGrupal _inclusao;

        public void ProcessarImplatacao()
        {
            ProcessarImplatacao(false, true);
        }

        protected void ProcessarImplatacao(bool isValid, bool ehImplantacao)
        {
            _inclusao = (IInclusaoCoberturaGrupal) new JsonToProposta().ObterProposta(isValid, ehImplantacao);
            GetInstance<InclusaoCoberturaVgService>().Execute(_inclusao).Wait();
        }
    }

    public class ProcessaInclusaoVgServiceTest : InclusaoVgBaseTest
    {
        [Test]
        public async Task AoProcessarUmaInclusaoVgDevePersistirACobertura()
        {
            ProcessarImplatacao();
            var produtos = _inclusao.Produtos.First();
            var cobertura = produtos.Coberturas.First();

            var dto = await GetInstance<ICoberturas>().ObterPorItemCertificado(long.Parse(cobertura.IdentificadorExterno));

            Assert.That(dto, Is.Not.Null, "Cobertura não implantada");
            Assert.That(produtos.Codigo, Is.EqualTo(dto.ProdutoId));
            Assert.That(produtos.Matricula, Is.EqualTo(dto.Matricula));
            Assert.That(_inclusao.Proponente.DataNascimento, Is.EqualTo(dto.DataNascimento));
            Assert.That(_inclusao.Proponente.Sexo, Is.EqualTo(dto.Sexo));
            Assert.That(cobertura.CodigoItemProduto, Is.EqualTo(dto.ItemProdutoId));
            Assert.That(cobertura.InicioVigencia, Is.EqualTo(dto.DataInicioVigencia));
            Assert.That(cobertura.FimVigencia, Is.EqualTo(dto.DataFimVigencia));
            Assert.That((int)cobertura.Contratacao.TipoFormaContratacao, Is.EqualTo(dto.TipoFormaContratacaoId));
            Assert.That((int)cobertura.Contratacao.TipoDeRenda, Is.EqualTo(dto.TipoRendaId));

            Assert.That((int)_inclusao.DadosPagamento.Periodicidade, Is.EqualTo(dto.Historico.PeriodicidadeId));

            if (cobertura.Prazos != null)
            {
                Assert.That(cobertura.Prazos.DecrescimoEmAnos, Is.EqualTo(dto.PrazoDecrescimoEmAnos));
                Assert.That(cobertura.Prazos.CoberturaEmAnos, Is.EqualTo(dto.PrazoCoberturaEmAnos));
                Assert.That(cobertura.Prazos.PagamentoEmAnos, Is.EqualTo(dto.PrazoPagamentoEmAnos));
            }

            if (produtos.Beneficiarios.Any())
            {
                Assert.That(produtos.Beneficiarios.First().DataNascimento, Is.EqualTo(dto.Historico.DataNascimentoBeneficiario));
                Assert.That(produtos.Beneficiarios.First().Sexo, Is.EqualTo(dto.Historico.SexoBeneficiario));
            }

            Assert.That(dto.Status, Is.EqualTo((int)(StatusCobertura.StatusCoberturaEnum.Activa)));
        }

        [Test]
        public async Task AoCompensarUmEventoDeInclusaoVgACoberturaDeveSerExcluida()
        {
            ProcessarImplatacao();

            await GetInstance<ImplantacaoPropostaService>().Compensate(_inclusao);

            Assert.That(await GetInstance<IEventosBase<EventoInclusaoVg>>().ExisteEvento(_inclusao.Identificador), Is.EqualTo(false));
        }
    }
}

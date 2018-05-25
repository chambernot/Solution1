using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Util;
using NUnit.Framework;
using static Mongeral.Provisao.V2.Domain.Dominios.StatusCobertura;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    [TestFixture]
    public abstract class ImplantacaoBaseTest : IntegrationTestBase
    {
        protected IProposta _proposta;

        public void ProcessarImplatacao()
        {
            ProcessarImplatacao(false, true);
        }

        protected void ProcessarImplatacao(bool isValid, bool ehImplantacao)
        {
            _proposta = new JsonToProposta().ObterProposta(isValid, ehImplantacao);
            GetInstance<ImplantacaoPropostaService>().Execute(_proposta).Wait();
        }
    }

    public class ProcessaImplantacaoServiceTest : ImplantacaoBaseTest
    {
        [Test]
        public async Task Ao_Processar_um_Evento_de_Implantacao_deve_Salvar_A_Cobertura()
        {
            ProcessarImplatacao();

            var produtos = _proposta.Produtos.First();
            var cobertura = produtos.Coberturas.First();

            var dto = await GetInstance<ICoberturas>().ObterPorItemCertificado(long.Parse(cobertura.IdentificadorExterno));
            
            Assert.That(dto, Is.Not.Null,"Cobertura não implantada");
            Assert.That(produtos.Codigo, Is.EqualTo(dto.ProdutoId));
            Assert.That(produtos.Matricula, Is.EqualTo(dto.Matricula));
            Assert.That(_proposta.Proponente.DataNascimento, Is.EqualTo(dto.DataNascimento));
            Assert.That(_proposta.Proponente.Sexo, Is.EqualTo(dto.Sexo));
            Assert.That(cobertura.CodigoItemProduto, Is.EqualTo(dto.ItemProdutoId));
            Assert.That(cobertura.InicioVigencia, Is.EqualTo(dto.DataInicioVigencia));
            Assert.That(cobertura.FimVigencia, Is.EqualTo(dto.DataFimVigencia));
            Assert.That((int)cobertura.Contratacao.TipoFormaContratacao, Is.EqualTo(dto.TipoFormaContratacaoId));
            Assert.That((int)cobertura.Contratacao.TipoDeRenda, Is.EqualTo(dto.TipoRendaId));
            
            Assert.That((int)_proposta.DadosPagamento.Periodicidade, Is.EqualTo(dto.Historico.PeriodicidadeId));

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

            Assert.That(dto.Status, Is.EqualTo((int)(StatusCoberturaEnum.Activa)));
        }

        [Test]
        public async Task Ao_Compensar_um_Evento_de_Implantacao_a_Cobertura_deve_ser_excluida()
        {
            ProcessarImplatacao();

            await GetInstance<ImplantacaoPropostaService>().Compensate(_proposta);

            Assert.That(await GetInstance<IEventosBase<EventoImplantacao>>().ExisteEvento(_proposta.Identificador), Is.EqualTo(false));//Verifica se foi compensado
        }
    }
}
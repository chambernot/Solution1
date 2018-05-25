using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG;
using Moq;

namespace Mongeral.Provisao.V2.Testes.Integracao.Util
{
    public static class PropostaIntegrador
    {
        //public static IProposta ConverterToIProposta(Proposta proposta, bool isValid, bool ehImplantacao)
        //{
        //    return (IProposta)ConverterToContrato(proposta, isValid, ehImplantacao);
        //}

        //public static IInclusaoCoberturaGrupal ConverterToInclusaoVg(Proposta proposta, bool isValid, bool ehImplantacao)
        //{
        //    return ConverterToContrato(proposta, isValid, ehImplantacao);
        //}

        public static IInclusaoCoberturaGrupal ConverterToContrato(Proposta proposta, bool isValid, bool ehImplantacao)
        {
            var iProposta = new Mock<IInclusaoCoberturaGrupal>();

            if (proposta != null)
            {
                //iProposta.SetupGet(x => x.Identificador).Returns(proposta.Identificador);
                iProposta.SetupGet(x => x.Identificador).Returns(Guid.NewGuid());
                iProposta.SetupGet(x => x.IdentificadorCorrelacao).Returns(proposta.IdentificadorCorrelacao);
                iProposta.SetupGet(x => x.IdentificadorNegocio).Returns(proposta.IdentificadorNegocio);
                iProposta.SetupGet(x => x.DataAssinatura).Returns(proposta.DataAssinatura);
                iProposta.SetupGet(x => x.DataExecucaoEvento).Returns(proposta.DataExecucaoEvento);
                iProposta.SetupGet(x => x.DataImplantacao).Returns(proposta.DataImplantacao);
                iProposta.SetupGet(x => x.Numero).Returns(proposta.Numero);

                #region Dados Pagamento

                var _iPagamento = new Mock<IFormaPagamento>();

                _iPagamento.SetupGet(p => p.Periodicidade).Returns(proposta.DadosPagamento.Periodicidade);
                iProposta.SetupGet(x => x.DadosPagamento).Returns(_iPagamento.Object);

                #endregion

                #region Produtos e Beneficiarios

                var listaProduto = new List<IProduto>();

                var _iProduto = new Mock<IProduto>();

                var produto = proposta.Produtos.First();

                _iProduto.SetupGet(p => p.InscricaoCertificado).Returns(produto.InscricaoCertificado);
                _iProduto.SetupGet(p => p.Codigo).Returns(produto.Codigo);
                _iProduto.SetupGet(p => p.Descricao).Returns(produto.Descricao);
                _iProduto.SetupGet(p => p.Matricula).Returns(produto.Matricula);

                var listaCobertura = new List<ICobertura>();
                var _iCobertura = new Mock<ICobertura>();

                var cobertura = produto.Coberturas.First();

                if (isValid)
                {
                    _iCobertura.SetupGet(c => c.ClasseRisco).Returns(1479);
                    _iCobertura.SetupGet(c => c.CodigoItemProduto).Returns(202806);
                    _iCobertura.SetupGet(c => c.InicioVigencia).Returns(new DateTime(2017, 06, 01));
                    _iCobertura.SetupGet(c => c.ValorContribuicao).Returns((decimal)895.58);
                    _iCobertura.SetupGet(c => c.IdentificadorExterno).Returns("104906598175810281");
                }
                else
                {
                    _iCobertura.SetupGet(c => c.ClasseRisco).Returns(cobertura.ClasseRisco);
                    _iCobertura.SetupGet(c => c.CodigoItemProduto).Returns(cobertura.CodigoItemProduto);
                    _iCobertura.SetupGet(c => c.InicioVigencia).Returns(cobertura.InicioVigencia);
                    _iCobertura.SetupGet(c => c.ValorContribuicao).Returns(cobertura.ValorContribuicao);
                    _iCobertura.SetupGet(c => c.IdentificadorExterno).Returns(cobertura.IdentificadorExterno);
                }

                _iCobertura.SetupGet(c => c.CodigoCobertura).Returns(cobertura.CodigoCobertura);
                
                _iCobertura.SetupGet(c => c.FimVigencia).Returns(cobertura.FimVigencia);
                _iCobertura.SetupGet(c => c.ValorBeneficio).Returns(cobertura.ValorBeneficio);
                _iCobertura.SetupGet(c => c.ValorCapital).Returns(cobertura.ValorCapital);

                if (cobertura.Prazos != null)
                {
                    var _iPrazos = new Mock<IPrazos>();
                    _iPrazos.SetupGet(p => p.PagamentoEmAnos).Returns(cobertura.Prazos.PagamentoEmAnos);
                    _iPrazos.SetupGet(p => p.DecrescimoEmAnos).Returns(cobertura.Prazos.DecrescimoEmAnos);
                    _iPrazos.SetupGet(p => p.CoberturaEmAnos).Returns(cobertura.Prazos.CoberturaEmAnos);

                    _iCobertura.SetupGet(c => c.Prazos).Returns(_iPrazos.Object);
                }

                var _iContratacao = new Mock<IContratacao>();

                if (isValid)
                {
                    _iContratacao.SetupGet(c => c.TipoFormaContratacao).Returns(TipoFormaContratacaoEnum.CapitalSegurado);
                    _iContratacao.SetupGet(c => c.TipoDeRenda).Returns(0);
                }
                else
                {                    
                    _iContratacao.SetupGet(c => c.TipoFormaContratacao).Returns(cobertura.Contratacao.TipoFormaContratacao);
                    _iContratacao.SetupGet(c => c.TipoDeRenda).Returns(cobertura.Contratacao.TipoDeRenda);
                }

                
                _iContratacao.SetupGet(c => c.PrazoDeRendaEmAnos).Returns(cobertura.Contratacao.PrazoDeRendaEmAnos);

                _iCobertura.SetupGet(c => c.Contratacao).Returns(_iContratacao.Object);

                listaCobertura.Add(_iCobertura.Object);

                _iProduto.SetupGet(p => p.Coberturas).Returns(listaCobertura);

                var listaBeneficiario = new List<IBeneficiario>();

                if (ehImplantacao)
                {
                    if (produto.Beneficiarios.Any())
                    {
                        var _iBeneficiario = new Mock<IBeneficiario>();

                        var beneficiario = produto.Beneficiarios.First();

                        _iBeneficiario.SetupGet(c => c.CPF).Returns(beneficiario.CPF);
                        _iBeneficiario.SetupGet(c => c.DataNascimento).Returns(beneficiario.DataNascimento);
                        _iBeneficiario.SetupGet(c => c.Nome).Returns(beneficiario.Nome);
                        _iBeneficiario.SetupGet(c => c.Participacao).Returns(beneficiario.Participacao);
                        _iBeneficiario.SetupGet(c => c.TitularCPF).Returns(beneficiario.TitularCPF);
                        _iBeneficiario.SetupGet(c => c.Matricula).Returns(beneficiario.Matricula);
                        _iBeneficiario.SetupGet(c => c.TipoParentesco).Returns(beneficiario.TipoParentesco);

                        listaBeneficiario.Add(_iBeneficiario.Object);
                    }

                    _iProduto.SetupGet(p => p.Beneficiarios).Returns(listaBeneficiario);
                }
                else
                {
                    var _iBeneficiario = new Mock<IBeneficiario>();                    

                    _iBeneficiario.SetupGet(c => c.CPF).Returns(11111111111);
                    _iBeneficiario.SetupGet(c => c.DataNascimento).Returns(new DateTime(2010, 01,01));
                    _iBeneficiario.SetupGet(c => c.Nome).Returns("Jose Beneficiario");
                    _iBeneficiario.SetupGet(c => c.Sexo).Returns("Masculino");
                    _iBeneficiario.SetupGet(c => c.Participacao).Returns(0M);
                    _iBeneficiario.SetupGet(c => c.TitularCPF).Returns(false);
                    _iBeneficiario.SetupGet(c => c.Matricula).Returns(000000000);
                    _iBeneficiario.SetupGet(c => c.TipoParentesco).Returns(new TipoParentesco());

                    listaBeneficiario.Add(_iBeneficiario.Object);

                    _iProduto.SetupGet(p => p.Beneficiarios).Returns(listaBeneficiario);
            }

                listaProduto.Add(_iProduto.Object);

                iProposta.SetupGet(x => x.Produtos).Returns(listaProduto);

                #endregion

                #region Proponente

                var _iProponente = new Mock<IProponente>();
                _iProponente.SetupGet(p => p.CPF).Returns(proposta.Proponente.CPF);

                if (isValid)
                { 
                    _iProponente.SetupGet(p => p.DataNascimento).Returns(new DateTime(1959, 12, 04));
                    _iProponente.SetupGet(p => p.Sexo).Returns("M");
                }
                else
                {
                    _iProponente.SetupGet(p => p.DataNascimento).Returns(proposta.Proponente.DataNascimento);
                    _iProponente.SetupGet(p => p.Sexo).Returns(proposta.Proponente.Sexo);
                }

                    _iProponente.SetupGet(p => p.Matricula).Returns(proposta.Proponente.Matricula);
                

                if (proposta.Proponente.Conjuge != null)
                {
                    var _iConjuge = new Mock<IPessoa>();
                    _iConjuge.SetupGet(c => c.Matricula).Returns(proposta.Proponente.Conjuge.Matricula);
                    _iConjuge.SetupGet(c => c.Sexo).Returns(proposta.Proponente.Conjuge.Sexo);
                    _iConjuge.SetupGet(c => c.DataNascimento).Returns(proposta.Proponente.Conjuge.DataNascimento);
                    _iProponente.SetupGet(p => p.Conjuge).Returns(_iConjuge.Object);
                }   
                iProposta.SetupGet(p => p.Proponente).Returns(_iProponente.Object);

                #endregion  
            }

            return iProposta.Object;
        }    
    }   

    public class AcumuloPrevidencia 
    {
       
        public DateTime DataDeConcessao { get; set; }

        public List<Fundo> Fundos { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoTributacaoIR TipoTributacaoIR { get; set; }

        public decimal ValorAporteInicial { get; set; }

        public decimal ValorPortabilidade { get; set; }
    }

    public class Agencia
    {
        public string DVAgencia { get; set; }

        public string NumeroAgencia { get; set; }
    }

    public class Beneficiario
    {
        public decimal Participacao { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoParentesco TipoParentesco { get; set; }

        public long CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Nome { get; set; }

        public Profissao Profissao { get; set; }

        public string Sexo { get; set; }

        public bool TitularCPF { get; set; }

        public long Matricula { get; set; }
    }

    public class Cartao
    {
        public long NumeroPreAutorizacao { get; set; }
    }

    public class Cheque
    {
        public string ContaCorrente { get; set; }

        public string NumeroAgencia { get; set; }

        public string NumeroBanco { get; set; }

        public string NumeroCheque { get; set; }
    }

    public class Cobertura
    {
        public AcumuloPrevidencia AcumuloPrevidencia { get; set; }

        public int? ClasseRisco { get; set; }

        public int CodigoCobertura { get; set; }

        public int CodigoItemProduto { get; set; }

        public Contratacao Contratacao { get; set; }

        public string IdentificadorExterno { get; set; }

        public Prazos Prazos { get; set; }

        public decimal ValorBeneficio { get; set; }

        public decimal ValorCapital { get; set; }

        public decimal ValorContribuicao { get; set; }

        public long Certificado { get; set; }

        public DateTime? FimVigencia { get; set; }

        public DateTime InicioVigencia { get; set; }
    }

    public class ContaCorrente
    {
        public string DVVerificador { get; set; }

        public string NumeroContaCorrente { get; set; }
    }

    public class Contratacao
    {
        public int? PrazoDeRendaEmAnos { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoDeRendaEnum TipoDeRenda { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoFormaContratacaoEnum TipoFormaContratacao { get; set; }
    }

    public class Convenio:IConvenio
    {
        public string NumeroConvenio { get; set; }
    }

    public class Correntista
    {
        public long Documento { get; set; }

        public string NomeCorrentista { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoDocumento TipoDocumento { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoPessoa TipoPessoa { get; set; }
    }

    public class Debito
    {
        public IAgencia Agencia { get; set; }

        public int CodigoOrgao { get; set; }

        public IContaCorrente ContaCorrente { get; set; }

        public ICorrentista Correntista { get; set; }

        public string NumeroBanco { get; set; }
    }

    public class Documento 
    {
        public string Codigo { get; set; }

        public DateTime DataExpedicao { get; set; }

        public string OrgaoExpedidor { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoDocumento TipoDeDocumento { get; set; }
    }

    public class DPS 
    {
        public decimal Altura { get; set; }

        public decimal Peso { get; set; }

        public int QuantidadeCigarrosDia { get; set; }

        public List<Questao> Questoes { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoDPSEnum TipoDPS { get; set; }
    }

    public class Endereco 
    {
        public string Bairro { get; set; }

        public int CEP { get; set; }

        public string Cidade { get; set; }

        public string Complemento { get; set; }

        public string Estado { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoEndereco TipoEndereco { get; set; }
    }

    public class Entrada 
    {
        public string Criterio { get; set; }

        public DateTime Data { get; set; }
    }

    public class Estipulante 
    {
        public long Codigo { get; set; }

        public string Nome { get; set; }
    }

    public class Folha 
    {
        public long CPFFuncionario { get; set; }

        public int CodigoOrgao { get; set; }

        public string Complemento { get; set; }

        public string MatriculaFuncional { get; set; }

        public string NomeFuncionario { get; set; }

        public List<Subdivisao> Subdivisao { get; set; }
    }

    public class Empresa 
    {

        public Endereco Endereco { get; set; }

        public string Nome { get; set; }
    }

    public class Fundo 
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public decimal PercentualAporte { get; set; }

        public decimal PercentualContribuicao { get; set; }

        public decimal PercentualPortabilidade { get; set; }
    }

    public class Pagamento
    {

        public DateTime ApartirDe { get; set; }

        public Cartao DadosCartao { get; set; }

        public Convenio DadosConvenio { get; set; }

        public Debito DadosDebito { get; set; }

        public Folha DadosFolha { get; set; }

        public int DiaDeVencimento { get; set; }

        public TipoFormaPagamento FormaPagamento { get; set; }

        public Periodicidade Periodicidade { get; set; }
    }

    public class Pessoa 
    {

        public long CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Nome { get; set; }

        public Profissao Profissao { get; set; }

        public string Sexo { get; set; }

        public bool TitularCPF { get; set; }

        public long Matricula { get; set; }
    }

    public class PPE 
    {

        public bool DeclarasePPE { get; set; }

        public string Descricao { get; set; }
    }

    public class Prazos 
    {
        public int? CoberturaEmAnos { get; set; }

        public int? DecrescimoEmAnos { get; set; }

        public int? PagamentoEmAnos { get; set; }
    }

    public class PrimeiroPagamento 
    {

        public long CPFPagador { get; set; }

        public Cheque Cheque { get; set; }

        public string NomePagador { get; set; }

        public decimal Valor { get; set; }
    }

    public class Produto 
    {

        public long InscricaoCertificado { get; set; }

        public List<Cobertura> Coberturas { get; set; }

        public int Codigo { get; set; }

        public string Descricao { get; set; }

        public long Matricula { get; set; }

        public List<Beneficiario> Beneficiarios { get; set; }

    }

    public class InclusaoVG: Proposta
    {
    }

    public class Proposta
    {

        public Pagamento DadosPagamento { get; set; }

        public DateTime DataAssinatura { get; set; }

        public DateTime DataProtocolo { get; set; }

        public List<IDPS> DeclaracoesDPS { get; set; }

        public Entrada Entrada { get; set; }

        public Estipulante Estipulante { get; set; }

        public string ModeloProposta { get; set; }

        public int Numero { get; set; }

        public PrimeiroPagamento PrimeiroPagamento { get; set; }

        public List<Produto> Produtos { get; set; }

        public Proponente Proponente { get; set; }

        public Mongeral.Integrador.Contratos.Enum.RamoNegocioEnum RamoNegocio { get; set; }

        public Estipulante Subestipulante { get; set; }

        public UsoCorretor UsoCorretor { get; set; }

        public UsoMongeral UsoMongeral { get; set; }

        public DateTime DataExecucaoEvento { get; set; }

        public Guid Identificador { get; set; }

        [XmlElement(IsNullable = true)]
        public string IdentificadorCorrelacao { get; set; }

        [XmlElement(IsNullable = true)]
        public string IdentificadorNegocio { get; set; }

        public DateTime DataImplantacao { get; set; }

    }

    public class Questao 
    {

        public string DetalhesResposta { get; set; }

        public int NumeroQuestao { get; set; }

        public bool Resposta { get; set; }

        public string TextoQuestao { get; set; }
    }

    public class Proponente 
    {

        public Pessoa Conjuge { get; set; }

        public List<Documento> Documentos { get; set; }

        public Endereco Endereco { get; set; }

        public Mongeral.Integrador.Contratos.Enum.EstadoCivilEnum EstadoCivil { get; set; }

        public long Matricula { get; set; }

        public string Nacionalidade { get; set; }

        public int NumeroFilhos { get; set; }

        public bool ObrigacoesFiscais { get; set; }

        public PPE PPE { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoParentesco ParentescoCorretorFuncionanrios { get; set; }

        public decimal RendaMensal { get; set; }

        public RepresentanteLegal RepresentanteLegal { get; set; }

        public bool ResideNoBrasil { get; set; }

        public Telefone Telefone { get; set; }

        public bool TitularCpf { get; set; }

        public long CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Nome { get; set; }

        public Profissao Profissao { get; set; }

        public string Sexo { get; set; }
    }

    public class Profissao
    {
        public bool Ativo { get; set; }

        public Mongeral.Integrador.Contratos.Enum.CategoriaTrabalhadorEnum Categoria { get; set; }   

        public string CodigoCBO { get; set; }

        public string Descricao { get; set; }

        public Empresa Empresa { get; set; }
    }

    public class RepresentanteLegal 
    {

        public Mongeral.Integrador.Contratos.Enum.TipoParentesco TipoParentesco { get; set; }

        public long CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Nome { get; set; }

        public Profissao Profissao { get; set; }

        public string Sexo { get; set; }

        public bool TitularCPF { get; set; }

        public long Matricula { get; set; }
        
    }

    public class Subdivisao 
    {
        public string Codigo { get; set; }

        public string Nivel { get; set; }
    }

    public class Telefone 
    {
        public short DDD { get; set; }

        public short DDI { get; set; }

        public int Numero { get; set; }

        public Mongeral.Integrador.Contratos.Enum.TipoTelefone TipoTelefone { get; set; }
    }

    public class UsoCorretor 
    {
        public string CodigoSusep { get; set; }

        public string Nome { get; set; }
    }

    public class UsoMongeral 
    {

        public string AcaoMarketing { get; set; }

        public int Agente { get; set; }

        public int AgenteFidelizacao { get; set; }
        public int Alternativa { get; set; }

        public string ConvenioAdesao { get; set; }

        public long Corretor1 { get; set; }

        public long Corretor2 { get; set; }

        public int DiretorRegional { get; set; }

        public int GerenteComercial { get; set; }

        public int GerenteSucursal { get; set; }

        public string Sucursal { get; set; }

        public int TipoComissao { get; set; }
    }    
}

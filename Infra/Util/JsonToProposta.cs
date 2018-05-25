using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Testes.Integracao.Util;
using Newtonsoft.Json;

namespace Mongeral.Provisao.V2.Testes.Infra.Util
{
    public class JsonToProposta
    {
        private string json = @"{    ""numero"": 105240075,    ""modeloProposta"": ""N1"" ,    ""ramoNegocio"": ""Individual"",    ""dataProtocolo"": ""2017-04-03T00:00:00"",    ""dataAssinatura"": ""2017-04-03T00:00:00"",    ""dataImplantacao"": ""2017-04-05T00:00:00"",    ""proponente"": {      ""matricula"": 5146521896,      ""estadoCivil"": 0,      ""resideNoBrasil"": true,      ""obrigacoesFiscais"": false,      ""nacionalidade"": ""BRASILEIRA"",      ""rendaMensal"": 5000.0,      ""numeroFilhos"": 0,      ""endereco"": {        ""tipoEndereco"": ""RelacionamentoDoCliente"",        ""logradouro"": ""MANOEL A CORREA"",        ""numero"": ""2521"",        ""complemento"": """",        ""bairro"": ""OFICINAS"",        ""cidade"": ""TUBARAO"",        ""estado"": ""SC"",        ""cep"": 88816730      },      ""parentescoCorretorFuncionanrios"": ""NaoSeAplica"",      ""nome"": ""ANDREZA BOING"",      ""dataNascimento"": ""1976-09-28T00:00:00"",      ""sexo"": ""F"",      ""cpf"": 1603591982,      ""titularCPF"": true,      ""profissao"": {        ""codigoCBO"": ""2515-10"",        ""descricao"": ""Psicólogo clínico"",        ""categoria"": 0,        ""ativo"": false      }    },    ""produtos"": [      {        ""codigo"": 1589,        ""descricao"": ""PECÚLIO COLETIVO POR MORTE"",        ""matricula"": 5146521896,        ""inscricaoCertificado"": 1052400751589,        ""tipoRelacaoSegurado"": ""Titular"",        ""coberturas"": [          {            ""identificadorExterno"": ""105240075158910411"",            ""codigoCobertura"": 41,            ""certificado"": 10524007515891,            ""codigoItemProduto"": 200419,            ""valorContribuicao"": 80.0,            ""valorBeneficio"": 300292.41,            ""valorCapital"": 300292.41,            ""inicioVigencia"": ""2017-04-06T00:00:00"",            ""fimVigencia"": ""2099-12-31T00:00:00"",            ""contratacao"": {              ""tipoFormaContratacao"": ""CapitalSegurado"",              ""tipoDeRenda"": ""NaoSeAplica""            },            ""prazos"": {}          }        ],        ""beneficiarios"": [          {            ""tipoParentesco"": ""Nenhum"",            ""participacao"": 100.0,            ""nome"": ""QUANTA"",            ""dataNascimento"": ""1901-01-01T00:00:00"",            ""matricula"": 0,            ""cpf"": 0,            ""titularCPF"": false          }        ]      },      {        ""codigo"": 1590,        ""descricao"": ""PECÚLIO COLETIVO POR INVALIDEZ"",        ""matricula"": 5146521896,        ""inscricaoCertificado"": 1052400751590,        ""tipoRelacaoSegurado"": ""Titular"",        ""coberturas"": [          {            ""identificadorExterno"": ""105240075159010401"",            ""codigoCobertura"": 40,            ""certificado"": 10524007515901,            ""codigoItemProduto"": 200422,            ""valorContribuicao"": 40.0,            ""valorBeneficio"": 398105.02,            ""valorCapital"": 398105.02,            ""inicioVigencia"": ""2017-04-06T00:00:00"",            ""fimVigencia"": ""2099-12-31T00:00:00"",            ""contratacao"": {              ""tipoFormaContratacao"": ""CapitalSegurado"",              ""tipoDeRenda"": ""NaoSeAplica""            },            ""prazos"": {}          }        ],        ""beneficiarios"": []      }    ],    ""dadosPagamento"": {      ""periodicidade"": ""Mensal"",      ""formaPagamento"": ""Instituido"",      ""diaDeVencimento"": 30,      ""apartirDe"": ""2017-04-01T00:00:00""    },    ""usoMongeral"": {      ""codigoParceria"": ""99888777000148"",      ""acaoMarketing"": ""AM2197"",      ""alternativa"": 0,      ""sucursal"": ""F81"",      ""diretorRegional"": 0,      ""gerenteSucursal"": 0,      ""gerenteComercial"": 0,      ""agente"": 0,      ""corretor1"": 0,      ""corretor2"": 0,      ""agenteFidelizacao"": 0,      ""tipoComissao"": 1    },    ""identificador"": ""20b9d3b1-3f20-48b8-a409-0002a4b4eed2"",    ""identificadorNegocio"": ""20b9d3b1-3f20-48b8-a409-0002a4b4eed2"",    ""dataExecucaoEvento"": ""2017-04-05T00:00:00""  }";

        public IProposta ObterProposta(bool IsValid)
        {
            return ObterProposta(IsValid, true);
        }

        public IProposta ObterProposta(bool isValid, bool ehImplantacao)
        {
            var proposta = JsonConvert.DeserializeObject<Proposta>(json);

            var result = PropostaIntegrador.ConverterToContrato(proposta, isValid, ehImplantacao);

            return result;
        }

        public IInclusaoCoberturaGrupal ObterInclusaoVg(bool IsValid)
        {
            return ObterInclusaoVG(IsValid, true);
        }

        public IInclusaoCoberturaGrupal ObterInclusaoVG(bool isValid, bool ehImplantacao)
        {
            var proposta = (InclusaoVG) JsonConvert.DeserializeObject<Proposta>(json);

            var result = PropostaIntegrador.ConverterToContrato(proposta, isValid, ehImplantacao);

            return result;
        }
    }
}

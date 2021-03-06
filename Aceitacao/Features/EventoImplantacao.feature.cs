﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Mongeral.Provisao.V2.Testes.Aceitacao.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Evento de Implantação")]
    [NUnit.Framework.CategoryAttribute("EventoImplantacao")]
    public partial class EventoDeImplantacaoFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "EventoImplantacao.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "Evento de Implantação", null, ProgrammingLanguage.CSharp, new string[] {
                        "EventoImplantacao"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Implantacao de uma proposta")]        
        public virtual void ImplantacaoDeUmaProposta()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Implantacao de uma proposta", new string[] {
                        "ignore"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Identificador",
                        "IdExterno",
                        "ItemProdutoId",
                        "DataInicioVigencia",
                        "ClasseRiscoId",
                        "TipoFormaContratacaoId",
                        "TipoRendaId",
                        "IdentificadorNegocio",
                        "InscricaoId",
                        "Matricula",
                        "Periodicidade",
                        "DataNascimento",
                        "Sexo"});
            table1.AddRow(new string[] {
                        "9d2b0228-4d0d-4c23-8b49-01a698857709",
                        "10116682815341371",
                        "153417",
                        "01/01/2016",
                        "10",
                        "5",
                        "2",
                        "245a444f-fc39-4ee1",
                        "9994447091",
                        "631011",
                        "30",
                        "01/01/1980",
                        "Masculino"});
#line 6
 testRunner.Given("que há uma proposta com os seguintes dados", ((string)(null)), table1, "Dado ");
#line 9
 testRunner.When("processar o evento de implantacao", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Identificador",
                        "IdentificadorNegocio"});
            table2.AddRow(new string[] {
                        "9d2b0228-4d0d-4c23-8b49-01a698857709",
                        "245a444f-fc39-4ee1"});
#line 10
 testRunner.Then("deve ser criado um evento implantado com os seguintes dados", ((string)(null)), table2, "Então ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "IdExterno",
                        "ItemProdutoId",
                        "DataInicioVigencia",
                        "ClasseRiscoId",
                        "TipoFormaContratacaoId",
                        "TipoRendaId",
                        "InscricaoId",
                        "Matricula"});
            table3.AddRow(new string[] {
                        "10116682815341371",
                        "153417",
                        "01/01/2016",
                        "10",
                        "5",
                        "2",
                        "9994447091",
                        "631011"});
#line 13
 testRunner.And("uma cobertura contratada deve conter", ((string)(null)), table3, "E ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Periodicidade",
                        "DataNascimento",
                        "Sexo"});
            table4.AddRow(new string[] {
                        "Mensal",
                        "01/01/1980",
                        "Masculino"});
#line 16
 testRunner.And("um historico de cobertura com", ((string)(null)), table4, "E ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Implantacao duas proposta com o mesmo identificador")]        
        public virtual void ImplantacaoDuasPropostaComOMesmoIdentificador()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Implantacao duas proposta com o mesmo identificador", new string[] {
                        "ignore"});
#line 22
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Identificador",
                        "IdExterno",
                        "ItemProdutoId",
                        "DataInicioVigencia",
                        "ClasseRiscoId",
                        "TipoFormaContratacaoId",
                        "TipoRendaId",
                        "IdentificadorNegocio",
                        "InscricaoId",
                        "Matricula",
                        "Periodicidade",
                        "DataNascimento",
                        "Sexo"});
            table5.AddRow(new string[] {
                        "9d2b0228-4d0d-4c23-8b49-01a698857709",
                        "10116682815341371",
                        "153417",
                        "01/01/2016",
                        "0",
                        "1",
                        "1",
                        "245a444f-fc39-4ee1",
                        "101166828534",
                        "100",
                        "30",
                        "01/01/1980",
                        "Masculino"});
#line 23
 testRunner.Given("que há uma implantacao com", ((string)(null)), table5, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Identificador",
                        "IdExterno",
                        "ItemProdutoId",
                        "DataInicioVigencia",
                        "ClasseRiscoId",
                        "TipoFormaContratacaoId",
                        "TipoRendaId",
                        "IdentificadorNegocio",
                        "InscricaoId",
                        "Matricula",
                        "Periodicidade",
                        "DataNascimento",
                        "Sexo"});
            table6.AddRow(new string[] {
                        "9d2b0228-4d0d-4c23-8b49-01a698857709",
                        "10116682815341371",
                        "153417",
                        "01/01/2016",
                        "0",
                        "1",
                        "1",
                        "245a444f-fc39-4ee1",
                        "101166828534",
                        "100",
                        "30",
                        "01/01/1980",
                        "Masculino"});
#line 26
 testRunner.And("uma outra implantacao com o mesmo IdExterno", ((string)(null)), table6, "E ");
#line 29
 testRunner.When("Processar os evento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 30
 testRunner.Then("deve criar apenas um evento com uma cobertura", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Implantacao de uma proposta com cobertura existente")]        
        public virtual void ImplantacaoDeUmaPropostaComCoberturaExistente()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Implantacao de uma proposta com cobertura existente", new string[] {
                        "ignore"});
#line 34
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Identificador",
                        "IdExterno",
                        "ItemProdutoId",
                        "DataInicioVigencia",
                        "ClasseRiscoId",
                        "TipoFormaContratacaoId",
                        "TipoRendaId",
                        "IdentificadorNegocio",
                        "InscricaoId",
                        "Matricula",
                        "Periodicidade",
                        "DataNascimento",
                        "Sexo"});
            table7.AddRow(new string[] {
                        "9d2b0228-4d0d-4c23-8b49-01a698857709",
                        "10116682815341371",
                        "153417",
                        "01/01/2016",
                        "0",
                        "1",
                        "1",
                        "245a444f-fc39-4ee1",
                        "101166828534",
                        "100",
                        "30",
                        "01/01/1980",
                        "Masculino"});
#line 35
 testRunner.Given("que há uma cobertura com os seguintes dados", ((string)(null)), table7, "Dado ");
#line 38
 testRunner.When("Processar o evento", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 39
 testRunner.Then("ocorre um erro: \"O identificador externo 10116682815341371 já foi implantado ante" +
                    "riormente.\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

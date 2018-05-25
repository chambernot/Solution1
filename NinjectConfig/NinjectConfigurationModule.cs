using Mongeral.Infrastructure.DataAccess;
using Mongeral.Infrastructure.UnitOfWork.Session;
using Mongeral.Provisao.V2.DAL;
using Mongeral.Provisao.V2.DAL.Persistir;
using Ninject.Modules;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Adapters.Containers;
using Mongeral.Provisao.V2.Adapters;
using ProdutoSvc = Mongeral.ESB.Produto.Contrato.v2.Operacoes;
using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Adapters.SvcImplementation;
using Mongeral.Provisao.V2.DTO;
using ICalculoService = Mongeral.Calculo.Contratos.Assinatura.ICalculoService;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Application;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Domain.Services;

namespace Mongeral.Provisao.V2.NinjectConfig
{
    public class NinjectConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<TransactionManager>().To<DataAccessSessionManager>();
            Bind<DataAccessSessionFactory>().To<SqlServerDataAccessSessionFactory>();
            Bind<ITransactionalMethod>().To<TransactionalMethod>();

            Bind<IEventosBase<EventoImplantacao>>().To<EventosImplantacao>();
            Bind<IEventosBase<EventoAlteracao>>().To<EventosAlteracaoParametros>();
            Bind<IEventosBase<EventoPremio>>().To<EventosPremio>();
            Bind<IEventosBase<EventoInclusaoVg>>().To<EventosInclusaoVg>();
            Bind<IEventosBase<EventoCobertura>>().To<EventoCobertura<EventoCobertura>>();  

            Bind<ICoberturas>().To<Coberturas>();
            Bind<IPremios>().To<Premios>();
            Bind<IProvisoes>().To<MovimentosProvisao>();
            Bind<IPremioService>().To<PremioService>();
            Bind<IProvisoesService>().To<ProvisoesService>();                 

            Bind<ICalculoService>().To<CalculoService>();
            Bind<IndexedCachedContainer<ChaveProduto, DadosProduto>>().To<ProdutoContainer>().InSingletonScope();
            Bind<ICalculoFacade>().To<CalculoFacede>();
            Bind<IProdutoAdapter>().To<ProdutoAdapter>();
            Bind<ProdutoSvc.IProdutoService>().To<ProdutoService>();
            Bind<ProdutoSvc.ICalculoService>().To<ProdutoCalculoService>();
            Bind<ICalculadorProvisaoPremio>().To<CalculadorProvisaoPremio>();

            Bind<IExecucaoEvento>().To<ExecutarEventoImplantacao>().WhenInjectedInto<ImplantacaoPropostaService>();
            Bind<IExecucaoEvento>().To<ExecutarEventoAlteracao>().WhenInjectedInto<AlteracaoPropostaService>();
            Bind<IExecucaoEvento>().To<ExecutarEventoInclusaoVg>().WhenInjectedInto<InclusaoCoberturaVgService>();    
        }
    }    
}
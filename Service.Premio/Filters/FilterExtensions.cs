using System;
using GreenPipes;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Application.Filters;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Service.Premio.Filters
{
    public static class FilterExtensions
    {
        public static void AddFilter<TContext>(this IPipeConfigurator<TContext> cfg, Func<IFilter<TContext>> filterFactory)
            where TContext : ProvisaoContext
        {
            cfg.AddPipeSpecification(new FilterSpecification<TContext>(filterFactory));
        }

        public static void AddFilter<TContexto, TEventoOperacional>(this IPipeConfigurator<TContexto> cfg, Func<IEventosBase<TEventoOperacional>> eventosFactory)
            where TContexto : ProvisaoContext
            where TEventoOperacional : EventoOperacional
        {
            cfg.AddPipeSpecification(new CompensarEventoFilterSpecification<TContexto, TEventoOperacional>(eventosFactory));
        }

        public static void AddEventAction<TContext, TEvento>(this IPipeConfigurator<TContext> cfg, params Action<TEvento>[] actions)
            where TEvento : IEvento
            where TContext : ProvisaoContext
        {
            cfg.AddPipeSpecification(new ActionSpecification<TContext, TEvento>(actions));
        }
    }
}
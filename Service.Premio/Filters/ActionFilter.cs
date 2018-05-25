using System;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Context;

namespace Mongeral.Provisao.V2.Service.Premio.Filters
{
    public class ActionFilter<TContext,TEvento> : IFilter<TContext>
        where TContext : ProvisaoContext
        where TEvento : IEvento
    {
        private readonly Action<TEvento>[] _actions;

        public ActionFilter(Action<TEvento>[] actions)
        {
            _actions = actions;
        }

        public async Task Send(TContext context, IPipe<TContext> next)
        {
            foreach (var validador in _actions)
            {
                validador((TEvento)(context.Request));
            }
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}
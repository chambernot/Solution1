using System;
using System.Collections.Generic;
using System.Linq;
using GreenPipes;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Context;

namespace Mongeral.Provisao.V2.Service.Premio.Filters
{
    public class FilterSpecification<TContext> : IPipeSpecification<TContext>
        where TContext : ProvisaoContext
    {
        private IFilter<TContext> _filter;

        public FilterSpecification(Func<IFilter<TContext>> filterFactory)
        {
            _filter = filterFactory.Invoke();
        }

        public void Apply(IPipeBuilder<TContext> builder)
        {
            builder.AddFilter(_filter);
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }

    public class ActionSpecification<TContext, TEvento> : IPipeSpecification<TContext>
        where TContext : ProvisaoContext
        where TEvento : IEvento
    {
        private readonly Action<TEvento>[] _actions;

        public ActionSpecification(Action<TEvento>[] actions)
        {
            _actions = actions;
        }

        public void Apply(IPipeBuilder<TContext> builder)
        {
            builder.AddFilter(new ActionFilter<TContext, TEvento>(_actions));
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
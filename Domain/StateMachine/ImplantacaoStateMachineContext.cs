using System.Runtime.InteropServices;
using System.Threading;
using Automatonymous;
using Automatonymous.Contexts;

namespace Mongeral.Provisao.V2.Domain.StateMachine
{
    //public class ProvisaoBehaviorContext<TInstance,TData,TEventoOperacional> : 
    //    EventBehaviorContext<TInstance,TData>
    //{
    //    public ProvisaoBehaviorContext(EventContext<TInstance, TData> context) : base(context)
    //    {
    //    }
    //}


    //public class ProvisaoStateMachineEventContext<TInstance, TData, TEvento> :
    //    StateMachineEventContext<TInstance, TData>, 
    //    EventContext<TInstance, TData>
    //    where TInstance : class
    //    where TEvento : EventoOperacional
    //{
    //    private readonly EventoOperacional _eventoOperacional;
    //    private TData _data;
    //    private Event<TData> _event;

    //    public ProvisaoStateMachineEventContext(StateMachine<TInstance> machine, TInstance instance, Event<TData> @event, TData data, TEvento eventoOperacional, CancellationToken cancellationToken)
    //        : base (machine,instance,@event,data,cancellationToken)
    //    {
    //        _eventoOperacional = eventoOperacional;
    //        _data = data;
    //        _event = @event;
    //    }
    //    TData EventContext<TInstance, TData>.Data => _data;
    //    Event<TData> EventContext<TInstance,TData>.Event => _event;
    //}    
}
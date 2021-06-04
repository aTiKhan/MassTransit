namespace MassTransit.Topology.Conventions.CorrelationId
{
    using System;
    using Context;
    using Filters;
    using GreenPipes;


    public class SetCorrelationIdMessageSendTopology<T> :
        IMessageSendTopology<T>
        where T : class
    {
        readonly IFilter<SendContext<T>> _filter;

        public SetCorrelationIdMessageSendTopology(IMessageCorrelationId<T> messageCorrelationId)
        {
            if (messageCorrelationId == null)
                throw new ArgumentNullException(nameof(messageCorrelationId));

            _filter = new SetCorrelationIdFilter<T>(messageCorrelationId);
        }

        public void Apply(ITopologyPipeBuilder<SendContext<T>> builder)
        {
            builder.AddFilter(_filter);
        }
    }
}

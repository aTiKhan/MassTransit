namespace MassTransit.Azure.ServiceBus.Core.Topology.Builders
{
    using Entities;


    /// <summary>
    /// A unique builder context should be created for each specification, so that the items added
    /// by it can be combined together into a group - so that if a subsequent specification yanks
    /// something that conflicts, the system can yank the group or warn that it's impacted.
    /// </summary>
    public interface ISubscriptionEndpointBrokerTopologyBuilder :
        IBrokerTopologyBuilder
    {
        /// <summary>
        /// A handle to the subscription topic
        /// </summary>
        TopicHandle Topic { get; }
    }
}

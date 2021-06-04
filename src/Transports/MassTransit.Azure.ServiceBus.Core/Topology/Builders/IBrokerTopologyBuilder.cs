namespace MassTransit.Azure.ServiceBus.Core.Topology.Builders
{
    using Entities;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Management;


    public interface IBrokerTopologyBuilder
    {
        /// <summary>
        /// Creates a topic
        /// </summary>
        /// <param name="topicDescription">The immutable topic description</param>
        /// <returns>An entity handle used to reference the exchange in subsequent calls</returns>
        TopicHandle CreateTopic(TopicDescription topicDescription);

        /// <summary>
        /// Creates a subscription
        /// </summary>
        /// <param name="topic">The source exchange</param>
        /// <param name="subscriptionDescription"></param>
        /// <param name="rule"></param>
        /// <param name="filter"></param>
        /// <returns>An entity handle used to reference the binding in subsequent calls</returns>
        SubscriptionHandle CreateSubscription(TopicHandle topic, SubscriptionDescription subscriptionDescription, RuleDescription rule, Filter filter);

        /// <summary>
        /// Creates a subscription which forwards to a different topic
        /// </summary>
        /// <param name="source">The source topic</param>
        /// <param name="destination">The destination topic</param>
        /// <param name="subscriptionDescription"></param>
        /// <returns>An entity handle used to reference the binding in subsequent calls</returns>
        TopicSubscriptionHandle CreateTopicSubscription(TopicHandle source, TopicHandle destination, SubscriptionDescription subscriptionDescription);

        /// <summary>
        /// Creates a queue
        /// </summary>
        /// <param name="queueDescription"></param>
        /// <returns></returns>
        QueueHandle CreateQueue(QueueDescription queueDescription);

        /// <summary>
        /// Creates a subscription which forwards to a queue
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="subscriptionDescription"></param>
        /// <param name="rule"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        QueueSubscriptionHandle CreateQueueSubscription(TopicHandle exchange, QueueHandle queue, SubscriptionDescription subscriptionDescription,
            RuleDescription rule,
            Filter filter);
    }
}

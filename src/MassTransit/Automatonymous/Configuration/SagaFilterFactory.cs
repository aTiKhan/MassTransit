namespace Automatonymous
{
    using GreenPipes;
    using MassTransit;
    using MassTransit.Saga;


    public delegate IFilter<ConsumeContext<TData>> SagaFilterFactory<TInstance, TData>(ISagaRepository<TInstance> repository,
        ISagaPolicy<TInstance, TData> policy, IPipe<SagaConsumeContext<TInstance, TData>> sagaPipe)
        where TInstance : class, ISaga
        where TData : class;
}

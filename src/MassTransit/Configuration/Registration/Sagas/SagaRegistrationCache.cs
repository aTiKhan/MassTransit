namespace MassTransit.Registration
{
    using System;
    using Saga;


    public static class SagaRegistrationCache
    {
        public static void Register(Type sagaType, IContainerRegistrar registrar)
        {
            Cached.Instance.GetOrAdd(sagaType).Register(registrar);
        }

        public static void AddSaga(IRegistrationConfigurator configurator, ISagaRepositoryRegistrationProvider provider, Type sagaType,
            Type sagaDefinitionType = null)
        {
            Cached.Instance.GetOrAdd(sagaType).AddSaga(configurator, provider, sagaDefinitionType);
        }

        /// <summary>
        /// Sets a saga type so that it will not be registered. This is used to allow state machines to register without a conflicting
        /// standard saga from also being registered.
        /// </summary>
        /// <param name="sagaType"></param>
        public static void DoNotRegister(Type sagaType)
        {
            Cached.Instance.GetOrAdd(sagaType).DoNotRegister();
        }

        static CachedRegistration Factory(Type type)
        {
            return (CachedRegistration)Activator.CreateInstance(typeof(CachedRegistration<>).MakeGenericType(type));
        }


        static class Cached
        {
            internal static readonly RegistrationCache<CachedRegistration> Instance = new RegistrationCache<CachedRegistration>(Factory);
        }


        interface CachedRegistration
        {
            void Register(IContainerRegistrar registrar);
            void AddSaga(IRegistrationConfigurator registry, ISagaRepositoryRegistrationProvider provider, Type sagaDefinitionType);

            void DoNotRegister();
        }


        class CachedRegistration<T> :
            CachedRegistration
            where T : class, ISaga
        {
            bool _doNotRegister;

            public void Register(IContainerRegistrar registrar)
            {
                if (_doNotRegister)
                    return;

                registrar.RegisterSaga<T>();
            }

            public void AddSaga(IRegistrationConfigurator registry, ISagaRepositoryRegistrationProvider provider, Type sagaDefinitionType)
            {
                ISagaRegistrationConfigurator<T> configurator = registry.AddSaga<T>(sagaDefinitionType);

                provider?.Configure(configurator);
            }

            public void DoNotRegister()
            {
                _doNotRegister = true;
            }
        }
    }
}

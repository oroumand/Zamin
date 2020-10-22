using Zamin.CqrsRouter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.CqrsRouter.HandlerRegistrations
{
    public class CqRouterServiceConfiguration
    {
        public Type MediatorImplementationType { get; private set; }
        public ServiceLifetime Lifetime { get; private set; }

        public CqRouterServiceConfiguration()
        {
            MediatorImplementationType = typeof(CqRouter);
            Lifetime = ServiceLifetime.Transient;
        }

        public CqRouterServiceConfiguration Using<TMediator>() where TMediator : ICqRouter
        {
            MediatorImplementationType = typeof(TMediator);
            return this;
        }

        public CqRouterServiceConfiguration AsSingleton()
        {
            Lifetime = ServiceLifetime.Singleton;
            return this;
        }

        public CqRouterServiceConfiguration AsScoped()
        {
            Lifetime = ServiceLifetime.Scoped;
            return this;
        }

        public CqRouterServiceConfiguration AsTransient()
        {
            Lifetime = ServiceLifetime.Transient;
            return this;
        }
    }
}

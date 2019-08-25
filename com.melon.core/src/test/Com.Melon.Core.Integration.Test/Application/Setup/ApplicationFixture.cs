using System.Linq;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Com.Melon.Core.Integration.Test.Application.Setup
{
    using System;
    using System.Collections.Generic;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Com.Melon.Core.Application;
    using Com.Melon.Core.Integration.Test.Application.Dummy;
    using MediatR;

    public class ApplicationFixture: IDisposable
    {
        private WindsorContainer _container;

        public ApplicationFixture()
        {
            _container = BuildWindsorContainer();
        }

        public IMediator BuildMediator()
        {
            var mediator = _container.Resolve<IMediator>();

            return mediator;
        }

        public DummyCommandHandler BuildDummyCommandHandler()
        {
           return _container.Resolve<ICommandHandler<DummyCommand>>() as DummyCommandHandler;
        }

        public ICommandBus BuildCommandBus()
        {
            return _container.Resolve<ICommandBus>();
        }

        public DummyCommandWithResultHandler BuildDummyCommandWithResultHandler()
        {
            return _container.Resolve<ICommandHandler<DummyCommandWithResult, DummyResult>>() as DummyCommandWithResultHandler;
        }

        private static WindsorContainer BuildWindsorContainer()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(Classes.FromAssemblyContaining<DummyCommandHandler>().BasedOn(typeof(IRequestHandler<,>)).WithServiceAllInterfaces().LifestyleSingleton());

            container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());
            container.Register(Component.For<ICommandBus>().ImplementedBy<CommandBus>());
            container.Register(Component.For<ServiceFactory>().UsingFactoryMethod<ServiceFactory>(k => (type =>
            {
                var enumerableType = type
                    .GetInterfaces()
                    .Concat(new[] { type })
                    .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                return enumerableType != null ? k.ResolveAll(enumerableType.GetGenericArguments()[0]) : k.Resolve(type);
            })));
            return container;
        }

        public void Dispose()
        {
        }
    }
}
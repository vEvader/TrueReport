using System;
using BusinessService;
using BusinessServiceInterface;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Provider;
using Provider.DataSourceProvider;
using Provider.PrintProvider;
using Provider.TemplateProvider;
using ProviderInterface;
using Repository;
using RepositoryInterface;

namespace Container
{
    public class TrContainer
    {
        private static readonly Lazy<TrContainer> m_lazy = new Lazy<TrContainer>(() => new TrContainer());

        private readonly WindsorContainer m_windsor;

        public static TrContainer Instance
        {
            get
            {
                return m_lazy.Value;
            }
        }

        public TService Resolve<TService>()
        {
            return m_windsor.Resolve<TService>();
        }

        private TrContainer()
        {
            m_windsor = new WindsorContainer();

            m_windsor.Register(Component.For<ITrService>().ImplementedBy<TrService>());

            m_windsor.Register(Component.For<ITemplateProvider>().ImplementedBy<TemplateProvider>());
            m_windsor.Register(Component.For<IDataSourceProvider>().ImplementedBy<DataSourceProvider>());
            m_windsor.Register(Component.For<IPrintProvider>().ImplementedBy<AsposePrintProvider>());
            
            m_windsor.Register(Component.For<ITrRepository>().ImplementedBy<TrRepository>());
        }
    }
}

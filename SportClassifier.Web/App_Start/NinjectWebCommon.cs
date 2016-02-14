[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SportClassifier.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SportClassifier.Web.App_Start.NinjectWebCommon), "Stop")]

namespace SportClassifier.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using SportClassifier.Data;
    using SportClassifier.Web.Infrastructure.Services.Contracts;
    using SportClassifier.Web.Infrastructure.Services;
    using Agromo.Web.Infrastructure.Services;
    using SportClassifier.Web.Infrastructure.ModelBinders;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        //COPY THIS TOO

        private static IKernel kernel;
        public static IKernel Kernel
        {
            get
            {
                return kernel;
            }
        }

        //COPY THIS TOO


        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
              kernel.Bind<IUowData>().To<UowData>();
            kernel.Bind<IKeyTypeKeyValueService>().To<KeyTypeKeyValueService>();
            kernel.Bind<IHomeService>().To<HomeService>();
            kernel.Bind<ICrowlingService>().To<CrowlingService>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind(typeof(EntityModelBinder<>)).ToSelf();
        }
    }
}

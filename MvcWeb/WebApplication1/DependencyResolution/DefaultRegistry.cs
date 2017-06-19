
namespace MvcWeb.DependencyResolution {
    using App_Start;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Web;
    using System;
    using Models.Promises;

    public class DefaultRegistry : Registry {

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });

            For<IAuthenticated>().Use(() => getAuthenticatedSessionUser());
        }

        private static IAuthenticated getAuthenticatedSessionUser()
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(HttpContext.Current.Session);

            return session[Reference.KEY_USER_SESSION] as IAuthenticated ?? null;
        }

    }
}
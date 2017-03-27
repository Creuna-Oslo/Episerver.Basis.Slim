using System.Web.Http.Dependencies;
using StructureMap;

namespace Creuna.Basis.Revisited.Web.App_Start.Structuremap
{
    // NOTE: slightly modified version of 'StructureMapDependencyResolver' from 'StructureMap.MVC4' project
    // https://github.com/webadvanced/Structuremap.MVC4/blob/master/content/DependencyResolution/StructureMapDependencyResolver.cs.pp
    public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container) : base(container) { }

        public IDependencyScope BeginScope()
        {
            var childContainer = Container.GetNestedContainer();
            return new StructureMapDependencyScope(childContainer);
        }
    }
}
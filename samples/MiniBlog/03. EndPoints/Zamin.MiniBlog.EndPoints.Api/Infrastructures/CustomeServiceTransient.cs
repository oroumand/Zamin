using Zamin.MiniBlog.Core.Domain.Writers;
using Zamin.Utilities.Services.DependentyInjectors;

namespace Zamin.MiniBlog.EndPoints.Api.Infrastructures
{
    public class CustomeServiceTransient : ICustomeServiceTransient,ITransientDependency
    {
        public void Exec()
        {
            
        }
    }

    public class CustomeServiceScope : ICustomeServiceScope, IScopeDependency
    {
        public void Exec()
        {

        }
    }
    public class CustomeServiceSingletone : ICustomeServiceSingletone, ISingletoneDependency
    {
        public void Exec()
        {

        }
    }

}

using Zamin.Utilities.Services.DependentyInjection;

namespace Zamin.MiniBlog.EndPoints.Mvc.Infrastrucutres
{
    public interface ICustomServiceSingletone
    {
        void Exec();
    }
    public interface ICustomServiceScope
    {
        void Exec();
    }
    public interface ICustomServiceTransient
    {
        void Exec();
    }

    public class CustomServiceSingletone : ICustomServiceSingletone, ISingletoneLifetime
    {
        public void Exec()
        {
        }
    }

    public class CustomServiceScope : ICustomServiceScope, IScopeLifetime
    {
        public void Exec()
        {
        }
    }

    public class TransientServiceScope : ICustomServiceTransient, ITransientLifetime
    {
        public void Exec()
        {
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using Zamin.Core.Domain.Data;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    /// <summary>
    /// توابع کمکی جهت ثبت نیازمندی‌های لایه داده
    /// </summary>
    public static class AddDataAccessExtentsions
    {

        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddRepositories(assembliesForSearch).AddUnitOfWorks(assembliesForSearch);

        public static IServiceCollection AddRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<>), typeof(IQueryRepository));

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
    }
}

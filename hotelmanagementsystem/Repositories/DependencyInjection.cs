using HotelManagementSystemAPI.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Abstractions;
using Repositories.Repositories;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<JwtGenerator>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            return services;
        }
    }
}
using DataAccess;
using DataAccess.DAO;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using WpfApp;

namespace LuuThanhDatWPF
{
    public class DIService
    {
        private static DIService instance = null;
        private static readonly object lockObject = new object();
        private ServiceProvider serviceProvider;
        public ServiceProvider ServiceProvider => serviceProvider;

        public static DIService Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new DIService();
                    }
                }
                return instance;
            }
        }

        public DIService()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureService(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection services)
        {
            services.AddSingleton<AppDbContext>();
            
            services.AddSingleton<CustomerDAO>();
            services.AddSingleton<RoomDAO>();
            services.AddSingleton<RoomTypeDAO>();
            services.AddSingleton<BookingDetailDAO>();
            services.AddSingleton<BookingReservationDAO>();
            
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();
            services.AddSingleton<IRoomTypeRepository, RoomTypeRepository>();
            services.AddSingleton<IBookingDetailRepository, BookingDetailRepository>();
            services.AddSingleton<IBookingReservationRepository, BookingReservationRepository>();
        }
    }
}

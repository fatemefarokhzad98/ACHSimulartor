using ACHSimulartor.Application.Interfaces;
using ACHSimulartor.Application.Services;
using ACHSimulartor.Data.Repositories;
using ACHSimulartor.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACHSimulartor.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<ITransferRequestService, TransferRequestService>();
            #endregion

            #region Repository
            services.AddScoped<ITransferRequestRepository, TransferRequestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Application.Interfaces;
using CManager.Core.Application.Services;
using CManager.Core.Infrastructure.Repos;

namespace CManager.Core.Application.Factories
{
    // Skapar och returnerar ett färdigt CustomerService så att resten av gui appen bara kan använda det direkt

    public static class CustomerServiceFactory
    {
        public static ICustomerService Create()
        {
            return new CustomerService(new CustomerRepo());
        }
    }
}


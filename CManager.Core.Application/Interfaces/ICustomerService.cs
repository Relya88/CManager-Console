using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Domain.Models;

namespace CManager.Core.Application.Interfaces;

// visar vilken funktionalitet applikationen kan göra me dkunder som att skapa, hämta och ta bort kunder 
public interface ICustomerService
{
    bool CreateCustomer(CustomerModel customer);
    List<CustomerModel> GetCustomers();
    bool DeleteCustomer(Guid id);
}

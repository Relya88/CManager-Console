using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Domain.Models;

namespace CManager.Core.Infrastructure.Repos;

// Min repositoryinterface som används för att separera lagring från affärslogik
public interface ICustomerRepo
{
    List<CustomerModel> GetAll();
    bool Save(List<CustomerModel> customers);
}

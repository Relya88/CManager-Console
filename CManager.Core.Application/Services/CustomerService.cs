using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Application.Interfaces;
using CManager.Core.Domain.Models;
using CManager.Core.Infrastructure.Repos;

namespace CManager.Core.Application.Services;

//logiken för att hantera kunder där klassen har ansvar för regler gällande kund
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _repo;

    //konstruktor för hur repot kopplas in (dependency injection)
    public CustomerService(ICustomerRepo repo)
    {
        _repo = repo;
    }

    //skapar kund och Hämtar alla kunder från repot
    public bool CreateCustomer(CustomerModel customer)
    {
        var customers = _repo.GetAll();

        //kontrollerar om det redan finns kund med samma email och om det gör det så avbrtys det 
        if (customers.Any(c => c.Email == customer.Email))
            return false;

        customers.Add(customer);

        //listan sparas via repot
        return _repo.Save(customers);
    }

    //hämtar alla kunder
    public List<CustomerModel> GetCustomers()
    {
        return _repo.GetAll();
    }

    //tar bort kund baserat på hens id. Enligt inlämningsuppgiften ska man kunna ta bort en kund via e-postadress men eftersom att ett
    // id är unikt och aldrig ändras så valde jag borttagning via id istället.
    //
    // Tänker att om en kund ändrar sin mail så blir det problem.
    // Alltså användaren väljer fortfarande kunden via email i konsolen,men när själva borttagningen görs så används Id.
    // Kändes mer logiskt att göra så och Emil visade under lektioen hur man kunde göra det
 
    public bool DeleteCustomer(Guid id)
    {
        var customers = _repo.GetAll();

        var customer = customers.FirstOrDefault(c => c.Id == id); //hittar kund med rätt id

        if (customer == null) //avbryter borttagning om ingen hittas
            return false;

        customers.Remove(customer); //remove som tar bort kunden och sedan sparas listan med ändringen
        return _repo.Save(customers);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CManager.Core.Application.Interfaces;
using CManager.Core.Domain.Models;

namespace CManager.Core.Presentation.ConsoleApp.Controllers;

//meny och användarflöde i konsolen

public class MenuController
{
    private readonly ICustomerService _customerService;

    // CustomerService injiceras via Dependency Injection
    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    //lagt till loop så att menyn visas om och om igen tills man själv avlutar programmet 
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager Core");
            Console.WriteLine("1. Create customer");
            Console.WriteLine("2. View customers");
            Console.WriteLine("3. View customer details");
            Console.WriteLine("4. Delete customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewCustomers();
                    break;

                case "3":
                    ViewCustomerDetails();
                    break;

                case "4":
                    DeleteCustomer();
                    break;

                case "0":
                    // Avslutar menyn och programmet
                    return;

                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Skapar kund med inskriven input iställer för använda helper. Tänker att det är enklare att validera med input direkt i controllern 
    //även valt enklare validering med if som kontrollera tom input. Man måste ange giltig namn och email
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create customer");

        Console.Write("First name: ");
        var firstName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(firstName))
        {
            ShowInputError("First name");
            return;
        }

        Console.Write("Last name: ");
        var lastName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lastName))
        {
            ShowInputError("Last name");
            return;
        }

        Console.Write("Email: ");
        var email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email))
        {
            ShowInputError("Email");
            return;
        }

        Console.Write("Phone number: ");
        var phoneNumber = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            ShowInputError("Phone number");
            return;
        }

        Console.Write("Street address: ");
        var street = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(street))
        {
            ShowInputError("Street address");
            return;
        }

        Console.Write("Postal code: ");
        var postalCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            ShowInputError("Postal code");
            return;
        }

        Console.Write("City: ");
        var city = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(city))
        {
            ShowInputError("City");
            return;
        }

        // Skapade ett nytt objekt (customerModel) med hjälp av chatgpt för insamling av info och att varje kund alltid ska få ett unikt id (Guid.NewGuid)
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                Street = street,
                PostalCode = postalCode,
                City = city
            }
        };

        //anropar servicelagret
        var result = _customerService.CreateCustomer(customer);

        Console.WriteLine(result
            ? "Customer created successfully"
            : "Customer already exists");

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }

    //det här ska visa alla mina kunder i systemet
    private void ViewCustomers()
    {
        Console.Clear();
        Console.WriteLine("All customers");

        var customers = _customerService.GetCustomers();

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} - {customer.Email}");
            }
        }

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }


    // får upp all info om en specifik kund baserat på deras mail
    private void ViewCustomerDetails()
    {
        Console.Clear();
        Console.WriteLine("View customer details");

        var customers = _customerService.GetCustomers();

        if (!customers.Any())
        {
            Console.WriteLine("No customers found.");
            Console.ReadKey();
            return;
        }

        // Visar alla kunders mail
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.Email);
        }

        Console.WriteLine();
        Console.Write("Enter email of customer: ");
        var email = Console.ReadLine();

        var customerToView = customers.FirstOrDefault(c => c.Email == email);

        if (customerToView == null)
        {
            Console.WriteLine("Customer not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Customer details:");
        Console.WriteLine($"Name: {customerToView.FirstName} {customerToView.LastName}");
        Console.WriteLine($"Id: {customerToView.Id}");
        Console.WriteLine($"Email: {customerToView.Email}");
        Console.WriteLine($"Phone number: {customerToView.PhoneNumber}");
        Console.WriteLine($"Address: {customerToView.Address.Street}, {customerToView.Address.PostalCode}, {customerToView.Address.City}");

        Console.WriteLine();
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }

    // Fick tipset att ta bort den från console.Writheline delen för att slippa upprepa, så la till en gemensam felutskrift för tom input istället
    private void ShowInputError(string fieldName)
    {
        Console.WriteLine($"{fieldName} cannot be empty.");
        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }


    // Tar bort en kund baserat på mail men själva borttagningen sker via kundes unika id i servicelagret
    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete customer");

        var customers = _customerService.GetCustomers();

        if (!customers.Any())
        {
            Console.WriteLine("No customers to delete.");
            Console.ReadKey();
            return;
        }

        // alla kunders mail
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.Email);
        }

        Console.WriteLine();
        Console.Write("Enter email of customer to delete: ");
        var email = Console.ReadLine();

        // letar efter kundesn mail
        var customerToDelete = customers
            .FirstOrDefault(c => c.Email == email);

        if (customerToDelete == null)
        {
            Console.WriteLine("Customer not found.");
            Console.ReadKey();
            return;
        }

        // förfrågan och bekräftelse på borttagning av kunden. (skrev om kodsnutten med hjälp av chatgpt för min fungerade ej i konsolen)
        Console.WriteLine();
        Console.WriteLine($"Are you sure you want to delete customer '{customerToDelete.Email}'?");
        Console.Write("Type Y to confirm or N to cancel: ");
        var confirmation = Console.ReadLine();

        //måste bekräftas med yes annars avbryts det 
        if (!string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Deletion cancelled.");
            Console.ReadKey();
            return;
        }

        //själva kundborttagningen
        var result = _customerService.DeleteCustomer(customerToDelete.Id);

        Console.WriteLine(result
            ? "Customer removed."
            : "Something went wrong.");

        Console.WriteLine("Press any key to continue..");
        Console.ReadKey();
    }
}



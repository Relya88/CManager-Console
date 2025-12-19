using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CManager.Core.Domain.Models;

namespace CManager.Core.Infrastructure.Repos;

// Min filhantering, json med endast lagring
public class CustomerRepo : ICustomerRepo
{
    private readonly string _filePath = "Data/customers.json";

    public List<CustomerModel> GetAll()
    {
        // Om filen inte finns så finns det inga kunder än
        if (!File.Exists(_filePath))
            return [];

        try
        {
            var json = File.ReadAllText(_filePath);

            // konverterar json till C#-objekt
            return JsonSerializer.Deserialize<List<CustomerModel>>(json) ?? [];
        }
        catch (Exception ex)
        {
            //Tänker att det kanske hade räckt med catch { return []; } men vade att skriva try/catch + Console.WriteLine som Emil visade
          
            Console.WriteLine($"Error reading customers: {ex.Message}");
            throw;
        }
    }

    public bool Save(List<CustomerModel> customers)
    {
        try
        {
            // skapar mappen om den inte finns
            Directory.CreateDirectory("Data");

            var json = JsonSerializer.Serialize(
                customers,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving customers: {ex.Message}");
            return false;
        }
    }
}

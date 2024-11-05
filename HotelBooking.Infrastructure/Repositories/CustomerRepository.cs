using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;

namespace HotelBooking.Infrastructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly HotelBookingContext db;

        public CustomerRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Add(Customer entity)
        {
            var existingCustomer = db.Customer.Find(entity.Id);
            if (existingCustomer == null)
            {
                db.Customer.Add(entity);
            }
            else
            {
                db.Entry(existingCustomer).CurrentValues.SetValues(entity);
            }
            db.SaveChanges();
        }


        public void Edit(Customer entity)
        {
            // Assuming the Edit functionality is not needed immediately, or will be implemented later
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            return db.Customer.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public void Remove(int id)
        {
            var customer = db.Customer.Find(id); // Using Find for better performance on primary key lookup
            if (customer != null)
            {
                db.Customer.Remove(customer);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"No customer found with ID {id}");
            }
        }
    }
}
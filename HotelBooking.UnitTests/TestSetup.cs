using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Reqnroll;
using Reqnroll.BoDi;

namespace HotelBooking.UnitTests
{
    [Binding]
    public class TestSetup
    {
        private readonly IObjectContainer _container;
        private HotelBookingContext _context;

        public TestSetup(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void Initialize()
        {
            // Set up a fresh in-memory database for each scenario
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase(databaseName: $"HotelBookingDb_{Guid.NewGuid()}")
                .Options;

            _context = new HotelBookingContext(options);
            _context.Database.EnsureDeleted();  // Ensure the database is clean before starting
            _context.Database.EnsureCreated();  // Create the database

            // Create repositories
            var bookingRepository = new BookingRepository(_context);
            var roomRepository = new RoomRepository(_context);
            var customerRepository = new CustomerRepository(_context);

            // Create the booking manager
            var bookingManager = new BookingManager(bookingRepository, roomRepository);

            // Register the dependencies in the Reqnroll container
            _container.RegisterInstanceAs<IRepository<Booking>>(bookingRepository);
            _container.RegisterInstanceAs<IRepository<Room>>(roomRepository);
            _container.RegisterInstanceAs<IRepository<Customer>>(customerRepository);
            _container.RegisterInstanceAs<IBookingManager>(bookingManager);

            // Optionally, you could also seed some default data here if necessary
            InitializeDefaultData(roomRepository, customerRepository);
        }

        private void InitializeDefaultData(IRepository<Room> roomRepository, IRepository<Customer> customerRepository)
        {
            if (!_context.Room.Any())
            {
                roomRepository.Add(new Room { Id = 101, Description = "Single" });
                roomRepository.Add(new Room { Id = 102, Description = "Double" });
            }

            if (!_context.Customer.Any())
            {
                customerRepository.Add(new Customer { Id = 1, Name = "John Doe", Email = "johndoe@email.com" });
                customerRepository.Add(new Customer { Id = 2, Name = "Jane Smith", Email = "janesmith@email.com" });
            }

            _context.SaveChanges();
        }

        [AfterScenario]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted(); // Clean up the database after each scenario
            _context.Dispose(); // Dispose of the context properly
        }
    }
}

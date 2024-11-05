using System;
using System.Linq;
using Xunit;
using HotelBooking.Core;
using HotelBooking.Infrastructure.Repositories;
using Reqnroll;

[Binding]
public class BookingSteps
{
    private readonly BookingManager _bookingManager;
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Customer> _customerRepository;
    private bool _bookingResult;

    public BookingSteps(BookingManager bookingManager, IRepository<Room> roomRepository, IRepository<Customer> customerRepository)
    {
        _bookingManager = bookingManager;
        _roomRepository = roomRepository;
        _customerRepository = customerRepository;
    }

    [Given(@"the following rooms are available")]
    public void GivenTheFollowingRoomsAreAvailable(Table table)
    {
        foreach (var row in table.Rows)
        {
            var room = new Room
            {
                Id = int.Parse(row["RoomID"]),
                Description = row["Description"]
            };
            _roomRepository.Add(room);
        }
    }
    
    [Given(@"room (.*) is already booked from ""(.*)"" to ""(.*)""")]
    public void GivenRoomIsAlreadyBooked(int roomId, string startDate, string endDate)
    {
        var customer = _customerRepository.GetAll().First(); // Assuming there's at least one customer
        var booking = new Booking
        {
            StartDate = DateTime.Parse(startDate),
            EndDate = DateTime.Parse(endDate),
            RoomId = roomId,
            CustomerId = customer.Id
        };
        _bookingManager.CreateBooking(booking);
    }

    [Given(@"the following customers are registered")]
    public void GivenTheFollowingCustomersAreRegistered(Table table)
    {
        foreach (var row in table.Rows)
        {
            var customer = new Customer
            {
                Id = int.Parse(row["CustomerID"]),
                Name = row["Name"],
                Email = row["Email"]
            };
            _customerRepository.Add(customer);
        }
    }

    [Given(@"customer (.*) requests a booking for room (.*)")]
    public void GivenCustomerRequestsABookingForRoom(int customerId, int roomId)
    {
        ScenarioContext.Current.Set(new Booking
        {
            CustomerId = customerId,
            RoomId = roomId
        }, "currentBooking");
    }

    [When(@"the customer tries to book from ""(.*)"" to ""(.*)""")]
    public void WhenTheCustomerTriesToBookFromTo(string startDate, string endDate)
    {
        var booking = ScenarioContext.Current.Get<Booking>("currentBooking");
        booking.StartDate = DateTime.Parse(startDate);
        booking.EndDate = DateTime.Parse(endDate);
        _bookingResult = _bookingManager.CreateBooking(booking);
    }

    [Then(@"the booking should be successful")]
    public void ThenTheBookingShouldBeSuccessful()
    {
        Assert.True(_bookingResult, "Expected the booking to be successful.");
    }

    [Then(@"the booking should be unsuccessful")]
    public void ThenTheBookingShouldBeUnsuccessful()
    {
        Assert.False(_bookingResult, "Expected the booking to be unsuccessful.");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager _bookingManager;
        private IRepository<Booking> _bookingRepository;
        private Mock<IRepository<Room>> _mockRoomRepository;
        private Mock<IRepository<Booking>> _mockBookingRepository;

        public BookingManagerTests()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            _bookingRepository = new FakeBookingRepository(start, end);
            _mockRoomRepository = new Mock<IRepository<Room>>();
            _mockBookingRepository = new Mock<IRepository<Booking>>();
            _bookingManager = new BookingManager(_bookingRepository, _mockRoomRepository.Object);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(-1);

            // Act
            Action act = () => _bookingManager.FindAvailableRoom(date, date, 1);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            // Setup mock data: ensure there are no bookings and at least one room is available.
            var rooms = new List<Room> { new Room { Id = 1 }, new Room { Id = 2 } };
            var bookings = new List<Booking>(); // No bookings for any rooms.
    
            _mockBookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            _mockRoomRepository.Setup(repo => repo.GetAll()).Returns(rooms);
            _bookingManager = new BookingManager(_mockBookingRepository.Object, _mockRoomRepository.Object);

            // Act
            int roomId = _bookingManager.FindAvailableRoom(date, date, 1);

            // Assert
            Assert.NotEqual(-1, roomId); // Ensure that a room is found.
        }


        [Fact]
        public void FindAvailableRoom_RoomAvailable_ReturnsAvailableRoom()
        {
            // This test was added to satisfy the following test design principle: "Tests should have strong assertions".

            // Arrange
            DateTime date = DateTime.Today.AddDays(1);

            // Act
            int roomId = _bookingManager.FindAvailableRoom(date, date, 1);

            var bookingForReturnedRoomId = _bookingRepository.GetAll().Where(
                b => b.RoomId == roomId
                && b.StartDate <= date
                && b.EndDate >= date
                && b.IsActive);

            // Assert
            Assert.Empty(bookingForReturnedRoomId);
        }

        
        //additional new tests:
        
        [Theory]
        [InlineData("2025-10-01", "2025-10-05", true)]  // Valid date range
        [InlineData("2025-10-05", "2025-10-01", false)] // Invalid date range (start date is after end date)
        public void CreateBooking_ValidatesBookingDateRange(string start, string end, bool expectedResult)
        {
            // Arrange: Set up the necessary mock objects and data
            var bookings = new List<Booking>();  // No existing bookings for the test scenario
            var rooms = new List<Room> { new Room { Id = 1 } }; // Assume one available room

            // Mock repository to return the empty list of bookings and available room
            _mockBookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            _mockRoomRepository.Setup(repo => repo.GetAll()).Returns(rooms);

            // Create a new booking object with the provided start and end dates
            var booking = new Booking
            {
                RoomId = 1,
                StartDate = DateTime.Parse(start),
                EndDate = DateTime.Parse(end),
                IsActive = true
            };

            // Mock repository to verify that the booking is added if it's valid
            _mockBookingRepository.Setup(repo => repo.Add(It.IsAny<Booking>())).Verifiable();
            // Initialize BookingManager with the mocked repositories
            _bookingManager = new BookingManager(_mockBookingRepository.Object, _mockRoomRepository.Object);

            // Act & Assert: Perform different assertions based on expected result
            if (expectedResult)
            {
                // If expectedResult is true, the booking should be successfully created and return true
                var result = _bookingManager.CreateBooking(booking);
                Assert.True(result);
            }
            else
            {
                // If expectedResult is false, an ArgumentException should be thrown due to invalid date range
                Assert.Throws<ArgumentException>(() => _bookingManager.CreateBooking(booking));
            }
        }

        [Fact]
        public void FindAvailableRoom_WhenAllRoomsAreBooked_ReturnsMinusOne()
        {
            // Arrange: Set up a date range (October 1, 2025, to October 5, 2025).
            var startDate = DateTime.Parse("2025-10-01");
            var endDate = DateTime.Parse("2025-10-05");

            // Arrange: Create two bookings that occupy all available rooms for the given date range.
            var bookings = new List<Booking>
            {
                new Booking { RoomId = 1, StartDate = startDate, EndDate = endDate, IsActive = true },
                new Booking { RoomId = 2, StartDate = startDate, EndDate = endDate, IsActive = true }
            };

            // Arrange: Create two rooms (Room 1 and Room 2).
            var rooms = new List<Room> { new Room { Id = 1 }, new Room { Id = 2 } };

            // Set up mock repositories to return the created bookings and rooms.
            _mockBookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            _mockRoomRepository.Setup(repo => repo.GetAll()).Returns(rooms);

            // Initialize the BookingManager with the mocked repositories.
            _bookingManager = new BookingManager(_mockBookingRepository.Object, _mockRoomRepository.Object);

            // Act: Attempt to find an available room for the specified date range.
            var roomId = _bookingManager.FindAvailableRoom(startDate, endDate, 1);

            // Assert: Verify that no rooms are available and -1 is returned.
            Assert.Equal(-1, roomId);
        }


        [Fact]
        public void FindAvailableRoom_WhenRoomIsAvailable_ReturnsRoomId()
        {
            // Arrange: Set up a date range (October 7, 2025, to October 10, 2025).
            var startDate = DateTime.Parse("2025-10-07");
            var endDate = DateTime.Parse("2025-10-10");

            // Arrange: Create a booking for Room 2 during the specified date range.
            var bookings = new List<Booking>
            {
                new Booking { RoomId = 2, StartDate = startDate, EndDate = endDate, IsActive = true }
            };

            // Arrange: Create two rooms (Room 1 and Room 2).
            var rooms = new List<Room> { new Room { Id = 1 }, new Room { Id = 2 } };

            // Set up mock repositories to return the created bookings and rooms.
            _mockBookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            _mockRoomRepository.Setup(repo => repo.GetAll()).Returns(rooms);

            // Initialize the BookingManager with the mocked repositories.
            _bookingManager = new BookingManager(_mockBookingRepository.Object, _mockRoomRepository.Object);

            // Act: Attempt to find an available room for the specified date range.
            var roomId = _bookingManager.FindAvailableRoom(startDate, endDate, 1);

            // Assert: Verify that Room 1 is returned as it is available.
            Assert.Equal(1, roomId);
        }



        [Fact]
        public void GetFullyOccupiedDates_ReturnsExpectedOccupiedDates()
        {
            // Arrange: Define a minimal date range and bookings.
            var startDate = DateTime.Parse("2024-10-01");
            var endDate = DateTime.Parse("2024-10-02");

            var bookings = new List<Booking>
            {
                // Booking for Room 1 from 2024-10-01 to 2024-10-02
                new Booking { RoomId = 1, StartDate = DateTime.Parse("2024-10-01"), EndDate = DateTime.Parse("2024-10-02"), IsActive = true }
            };

            // Set up the repository to return these bookings.
            _mockBookingRepository.Setup(repo => repo.GetAll()).Returns(bookings);
            _bookingManager = new BookingManager(_mockBookingRepository.Object, _mockRoomRepository.Object);

            // Act: Get the fully occupied dates based on the simplified data.
            var fullyOccupiedDates = _bookingManager.GetFullyOccupiedDates(startDate, endDate);

            // Assert: Define the expected dates for this simplified scenario.
            var expectedDates = new List<DateTime>
            {
                DateTime.Parse("2024-10-01"), // Room 1's booking covers this date.
                DateTime.Parse("2024-10-02")  // Room 1's booking end date.
            };

            Assert.Equal(expectedDates, fullyOccupiedDates);
        }


    }
}

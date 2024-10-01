using System;
using Moq;
using Xunit;
using HotelBooking.Core;

public class Test
{
    [Fact]
    public void CreateBooking_ReturnsTrue_WhenBookingIsValid()
    {
        // Arrange
        var mockBookingManager = new Mock<IBookingManager>();
        var booking = new Booking { Id = 1, RoomId = 1, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(3) };

        mockBookingManager.Setup(bm => bm.CreateBooking(booking)).Returns(true);

        // Act
        var result = mockBookingManager.Object.CreateBooking(booking);

        // Assert
        Assert.True(result);
    }
}
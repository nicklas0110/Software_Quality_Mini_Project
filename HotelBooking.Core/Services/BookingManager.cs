using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Core
{
    public class BookingManager : IBookingManager
    {
        private IRepository<Booking> bookingRepository;
        private IRepository<Room> roomRepository;

        // Constructor injection
        public BookingManager(IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
        {
            this.bookingRepository = bookingRepository;
            this.roomRepository = roomRepository;
        }

        public bool CreateBooking(Booking booking)
        {
            // Explicitly check if the requested room is available for the dates
            int availableRoomId = FindAvailableRoom(booking.StartDate, booking.EndDate, booking.RoomId);

            if (availableRoomId == booking.RoomId)
            {
                booking.IsActive = true;
                bookingRepository.Add(booking);
                return true;
            }
            else
            {
                return false;
            }
        }


        public int FindAvailableRoom(DateTime startDate, DateTime endDate, int requestedRoomId)
        {
            var allRooms = roomRepository.GetAll();
            var bookings = bookingRepository.GetAll();

            foreach (var room in allRooms)
            {
                var bookingsForRoom = bookings.Where(b => b.RoomId == room.Id && b.IsActive);
                bool isRoomAvailable = bookingsForRoom.All(b => endDate < b.StartDate || startDate > b.EndDate);

                if (isRoomAvailable && room.Id == requestedRoomId)
                {
                    return room.Id; // Return the ID only if the room is available and it's the requested one
                }
            }
            return -1; // Return -1 if no suitable room is found
        }


        public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("The start date cannot be later than the end date.");

            List<DateTime> fullyOccupiedDates = new List<DateTime>();
            int noOfRooms = roomRepository.GetAll().Count();
            var bookings = bookingRepository.GetAll();

            if (bookings.Any())
            {
                for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
                {
                    var noOfBookings = from b in bookings
                                       where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                       select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                }
            }
            return fullyOccupiedDates;
        }

    }
}

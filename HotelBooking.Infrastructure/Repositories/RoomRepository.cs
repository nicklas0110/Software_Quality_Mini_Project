using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;

namespace HotelBooking.Infrastructure.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly HotelBookingContext db;

        public RoomRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Add(Room entity)
        {
            var existingRoom = db.Room.Find(entity.Id);
            if (existingRoom == null)
            {
                db.Room.Add(entity);
                db.SaveChanges();
            }
            else
            {
                // Optionally, update the existing entity or log that addition was skipped.
                // Update logic here if needed, or just skip if exact duplicates are expected.
                db.Entry(existingRoom).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }


        public void Edit(Room entity)
        {
            throw new NotImplementedException();
        }

        public Room Get(int id)
        {
            // The FirstOrDefault method below returns null
            // if there is no room with the specified Id.
            return db.Room.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Room.ToList();
        }

        public void Remove(int id)
        {
            // The Single method below throws an InvalidOperationException
            // if there is not exactly one room with the specified Id.
            var room = db.Room.Single(r => r.Id == id);
            db.Room.Remove(room);
            db.SaveChanges();
        }
    }
}

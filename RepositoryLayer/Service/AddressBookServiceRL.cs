using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class AddressBookServiceRL : IAddressBookServiceRL
    {
        private readonly AddressContext _dbContext;

        public AddressBookServiceRL(AddressContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddressEntity AddContact(AddressEntity entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public AddressEntity UpdateContact(int id, AddressEntity entity)
        {
            var existingEntry = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (existingEntry != null)
            {
                existingEntry.FirstName = entity.FirstName;
                existingEntry.LastName = entity.LastName;
                existingEntry.Email = entity.Email;
                existingEntry.PhoneNumber = entity.PhoneNumber;

                _dbContext.SaveChanges();
                return existingEntry;
            }
            return null;
        }

        // Delete Contact
        public bool DeleteContact(int id)
        {
            var entry = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (entry != null)
            {
                _dbContext.Users.Remove(entry);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<AddressEntity> GetAllContacts()
        {
            return _dbContext.Users.ToList();
        }

        public AddressEntity GetContactById(int id)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Id == id);
        }
    }
}

using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookServiceRL
    {
        AddressEntity AddContact(AddressEntity entity);
        AddressEntity UpdateContact(int id, AddressEntity entity);
        bool DeleteContact(int id);
        List<AddressEntity> GetAllContacts();
        AddressEntity GetContactById(int id);
    }
}

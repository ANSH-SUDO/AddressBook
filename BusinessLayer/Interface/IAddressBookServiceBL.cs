using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAddressBookServiceBL
    {
        AddressBookEntryDTO AddContact(AddressBookEntryDTO entryDto);
        AddressBookEntryDTO UpdateContact(int id, AddressBookEntryDTO entryDto);
        bool DeleteContact(int id);
        List<AddressBookEntryDTO> GetAllContacts();
        AddressBookEntryDTO GetContactById(int id);
    }
}

using AutoMapper;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class AddressBookServiceBL : IAddressBookServiceBL
    {
        private readonly IAddressBookServiceRL _addressRL;
        private readonly IMapper _mapper;

        public AddressBookServiceBL(IAddressBookServiceRL addressRL, IMapper mapper)
        {
            _addressRL = addressRL;
            _mapper = mapper;
        }

        public AddressBookServiceBL(AddressBookServiceRL addressRL)
        {
            _addressRL = addressRL;
        }

        public AddressBookEntryDTO AddContact(AddressBookEntryDTO entryDto)
        {
            var entry = _mapper.Map<AddressEntity>(entryDto);
            var addedEntry = _addressRL.AddContact(entry);
            return _mapper.Map<AddressBookEntryDTO>(addedEntry);
        }

        public AddressBookEntryDTO UpdateContact(int id, AddressBookEntryDTO entryDto)
        {
            var entry = _mapper.Map<AddressEntity>(entryDto);
            var updatedEntry = _addressRL.UpdateContact(id, entry);
            return updatedEntry != null ? _mapper.Map<AddressBookEntryDTO>(updatedEntry) : null;
        }

        public bool DeleteContact(int id)
        {
            return _addressRL.DeleteContact(id);
        }

        public List<AddressBookEntryDTO> GetAllContacts()
        {
            var contacts = _addressRL.GetAllContacts();
            return _mapper.Map<List<AddressBookEntryDTO>>(contacts);
        }

        public AddressBookEntryDTO GetContactById(int id)
        {
            var contact = _addressRL.GetContactById(id);
            return contact != null ? _mapper.Map<AddressBookEntryDTO>(contact) : null;
        }
    }
}

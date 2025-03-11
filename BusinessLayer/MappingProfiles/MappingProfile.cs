using AutoMapper;
using ModelLayer.DTO;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressBookEntry, AddressBookEntryDTO>().ReverseMap();
        }
    }
}

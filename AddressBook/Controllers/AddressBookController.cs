using AutoMapper;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using System;

namespace AddressBook.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressBookController : ControllerBase
{
    private readonly IAddressBookServiceBL _addressBL;
    private readonly IMapper _mapper;
    public AddressBookController(IAddressBookServiceBL addressBL, IMapper mapper)
    {
        _addressBL = addressBL;
        _mapper = mapper;
    }

    /// <summary>
    /// UC2- Method to fetch all contacts
    /// </summary>
    /// <returns>Contacts</returns>
    [HttpGet]
    public ActionResult<string> GetAllContacts()
    {
        var result = _addressBL.GetAllContacts();
        return Ok(new
        {
            Success = true,
            Message = "Contact fetch successful",
            Data = result
        });
    }

    /// <summary>
    /// UC2- Method to fetch contacts by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Contacts</returns>
    [HttpGet]
    [Route("addressBook/{id}")]
    public ActionResult<string> GetContactById(int id)
    {
        var result = _addressBL.GetContactById(id);
        if (result == null)
            return NotFound(new { Message = "Contact not found" });

        return Ok(new
        {
            Success = true,
            Message = $"Contact fetch successful with id {id}",
            Data = result
        });
    }

    /// <summary>
    /// UC2- Method to add new contact
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns>Add Contact</returns>
    [HttpPost]
    [Route("addContact")]
    public ActionResult<string> AddContact([FromBody] AddressBookEntryDTO addressEntryDTO)
    {
        var result = _addressBL.AddContact(addressEntryDTO);
        return CreatedAtAction(nameof(AddContact), new { id = result.Name }, result);       
    }

    /// <summary>
    /// UC2- Method to update contact by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDTO"></param>
    /// <returns>Updates Contact</returns>
    [HttpPut]
    [Route("updateContact/{id}")]
    public ActionResult<string> UpdateContact(int id, [FromBody] AddressBookEntryDTO addressEntryDTO)
    {
        var result = _addressBL.UpdateContact(id, addressEntryDTO);
        if (result == null)
            return NotFound(new { Message = "Contact not found" });

        return Ok(new
        {
            Success = true,
            Message = $"Contact updated successful with id {id}",
            Data = result
        });
    }

    /// <summary>
    /// UC2- Method to delete the contact
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("deleteContact/{id}")]
    public ActionResult<string> DeleteContact(int id)
    {
        var isDeleted = _addressBL.DeleteContact(id);
        if (!isDeleted)
            return NotFound(new { Message = "Contact not found" });

        return Ok(new { Message = "Contact deleted successfully" });
    }
}

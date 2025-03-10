using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;

namespace AddressBook.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressBookController : ControllerBase
{
    private readonly AddressContext _dbContext;
    public AddressBookController(AddressContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// UC2- Method to fetch all contacts
    /// </summary>
    /// <returns>Contacts</returns>
    [HttpGet]
    public ActionResult<ResponseDTO<string>> GetAllContacts()
    {
        if (_dbContext.Users != null)
        {
            return Ok(new
            {
                Success = true,
                Message = "Contacts fetched successfully",
                Data = ""
            });
        }
        return NotFound(new
        {
            Success = false,
            Message = "No contacts found"
        });
    }

    /// <summary>
    /// UC2- Method to fetch contacts by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Contacts</returns>
    [HttpGet]
    [Route("addressBook/{id}")]
    public ActionResult<ResponseDTO<string>> GetContactById(int id)
    {
        if (_dbContext != null)
        {
            return Ok(new
            {
                Success = true,
                Message = "Contact fetched successfully",
                Data = ""
            });
        }

        return NotFound(new
        {
            Success = false,
            Message = $"Contact with ID {id} not found"
        });
    }

    /// <summary>
    /// UC2- Method to add new contact
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns>Add Contact</returns>
    [HttpPost]
    [Route("addContact")]
    public ActionResult<ResponseDTO<string>> AddContact([FromBody] RequestDTO requestDTO)
    {
        if (ModelState.IsValid)
        {
            var newContact = new AddressEntity
            {
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                Email = requestDTO.Email,
                PhoneNumber = requestDTO.PhoneNumber,
                Password = requestDTO.Password
            };

            _dbContext.Users.Add(newContact);
            _dbContext.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Contact added successfully",
                Data = $"{newContact.FirstName}, {newContact.LastName}, {newContact.Email}, {newContact.PhoneNumber}"
            });
        }

        return BadRequest(new
        {
            Success = false,
            Message = "Invalid data"
        });        
    }

    /// <summary>
    /// UC2- Method to update contact by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDTO"></param>
    /// <returns>Updates Contact</returns>
    [HttpPut]
    [Route("updateContact/{id}")]
    public ActionResult<ResponseDTO<string>> UpdateContact(int id, [FromBody] RequestDTO requestDTO)
    {
        if (ModelState.IsValid)
        {
            var newContact = _dbContext.Users.Find(id);

            if (newContact == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Message = $"Contact with ID {newContact.Id} not found"
                });
            }

            newContact.FirstName = requestDTO.FirstName;
            newContact.LastName = requestDTO.LastName;
            newContact.Email = requestDTO.Email;
            newContact.PhoneNumber = requestDTO.PhoneNumber;

            _dbContext.Users.Update(newContact);
            _dbContext.SaveChanges();

            return Ok(new
            {
                Success = true,
                Message = "Contact updated successfully",
                Data = newContact
            });
        }

        return BadRequest(new
        {
            Success = false,
            Message = "Invalid data"
        });
    }

    /// <summary>
    /// UC2- Method to delete the contact
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("deleteContact/{id}")]
    public ActionResult<ResponseDTO<string>> DeleteContact(int id)
    {
        var contact = _dbContext.Users.Find(id);

        if (contact == null)
        {
            return NotFound(new
            {
                Success = false,
                Message = $"Contact with ID {contact.Id} not found"
            });   
        }

        _dbContext.Users.Remove(contact);
        _dbContext.SaveChanges();

        return Ok(new
        {
            Success = true,
            Message = $"Contact with id {contact.Id} deleted successfully"
        });
    }
}

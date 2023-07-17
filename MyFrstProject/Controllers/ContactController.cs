using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFrstProject.Models;
using System;
using System.Threading.Tasks;


namespace MyFrstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactsAPIDbContext contactsAPIDb;

        public ContactController(ContactsAPIDbContext contactsAPIDb)
        {
            this.contactsAPIDb = contactsAPIDb;
        }
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                this.contactsAPIDb.Contacts.Add(contact);
                this.contactsAPIDb.SaveChanges();
                return Ok("Contact created successfully.");
            }
            return BadRequest("Invalid contact data.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactsById(int id)
        {
            var contact = await contactsAPIDb.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int id, UpdateContactRequest updateContactRequest)

        {
            var contact = await contactsAPIDb.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.name = updateContactRequest.name;
                contact.phone = updateContactRequest.phone;
                contact.address = updateContactRequest.address;
                contact.email = updateContactRequest.email;
                await contactsAPIDb.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await contactsAPIDb.Contacts.FindAsync(id);
            if (contact != null)
            {
                contactsAPIDb.Remove(contact);
                contactsAPIDb.SaveChanges();
                return Ok(contact);
            }
            return NotFound();
        }


    }
}

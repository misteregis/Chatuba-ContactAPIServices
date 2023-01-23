using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAPIServices.Models;
using ContactAPIServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ContactAPIServices.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    [Produces("application/json")]
    public class ContactApiController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactApiController(IOptions<ContactService> options) => _service = options.Value;

        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Contact>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            IEnumerable<Contact> contacts = _service.GetContacts();

            if (!contacts.Any())
                return NoContent();

            return Ok(contacts);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status201Created)]
        public IActionResult Create(Contact contact)
        {
            IEnumerable<Contact> contacts = _service.GetContacts();
            int id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;

            contact.Id = id;

            _service.AddContact(contact);

            return CreatedAtAction(nameof(Get), new { id }, contact);
        }

        [HttpGet("{id:int}", Name = "GetContact")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            if (!_service.ContactExists(id))
                return NotFound();

            return Ok(_service.GetContact(id));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] Contact contact)
        {
            if (!_service.ContactExists(id))
                return BadRequest();

            contact.Id = id;

            _service.UpdateContact(contact);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (!_service.ContactExists(id))
                return BadRequest();

            _service.DeleteContact(id);

            return NoContent();
        }
    }
}
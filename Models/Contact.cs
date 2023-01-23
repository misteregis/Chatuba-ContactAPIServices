using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPIServices.Models
{
    public class Contact
    {
        public int Id { get; internal set; }

        [Required, MaxLength(30), DisplayName("name")]
        public string Name { get; set; }

        [EmailAddress, DisplayName("email")]
        public string Email { get; set; }
    }
}
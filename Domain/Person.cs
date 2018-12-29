using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Core;
using Newtonsoft.Json;

namespace Domain
{
    public class Person : BaseEntity
    {
        private string _firstName;
        private string _lastName;


        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = "First Name")]
        public virtual string FirstName
        {
            get => _firstName;
            set => _firstName = value?.Trim();
        }

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = "Last Name")]
        public virtual string LastName
        {
            get => _lastName;
            set => _lastName = value?.Trim();
        }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        [JsonIgnore]
        public virtual string FirstLastName => (FirstName + " " + LastName).Trim();
        [JsonIgnore]
        public virtual string LastFirstName => (LastName + " " + FirstName).Trim();
    }
}
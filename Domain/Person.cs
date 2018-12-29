using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Core;

namespace Domain
{
    public class Person : BaseEntity
    {
        private string _firstName;
        private string _lastName;


        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public virtual string FirstName
        {
            get => _firstName;
            set => _firstName = value?.Trim();
        }

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public virtual string LastName
        {
            get => _lastName;
            set => _lastName = value?.Trim();
        }


        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual string FirstLastName => (FirstName + " " + LastName).Trim();
        public virtual string LastFirstName => (LastName + " " + FirstName).Trim();
    }
}
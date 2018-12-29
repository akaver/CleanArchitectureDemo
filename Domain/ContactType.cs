using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Core;

namespace Domain
{
    public class ContactType : BaseEntity
    {
        private string _value;

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = "Contact type")]
        public virtual string Value
        {
            get => _value;
            set => _value = value?.Trim();
        }


        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
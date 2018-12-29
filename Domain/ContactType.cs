using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Core;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Domain.Core;
using Newtonsoft.Json;

namespace Domain
{
    public class Contact : BaseEntity
    {
        private string _value;

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        [Display(Name = "Contact")]
        public string Value
        {
            get => _value;
            set => _value = value?.Trim();
        }


        public int PersonId { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }

        public int ContactTypeId { get; set; }
        [JsonIgnore]
        public ContactType ContactType { get; set; }
    }
}
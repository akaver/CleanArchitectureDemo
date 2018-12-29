using System.ComponentModel.DataAnnotations;
using Domain.Core;

namespace Domain
{
    public class Contact : BaseEntity
    {
        private string _value;

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string Value
        {
            get => _value;
            set => _value = value?.Trim();
        }


        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}
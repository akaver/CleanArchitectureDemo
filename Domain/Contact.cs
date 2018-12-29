using Domain.Core;

namespace Domain
{
    public class Contact : BaseEntity
    {
        private string _value;

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
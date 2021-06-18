using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

       public  string PhoneNumber { get; set; }

        public string emailAddress { get; set; }
        [ForeignKey("Account")]
       public  Guid? AccountId { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
        public Guid? AddressId { get; set; }

        private List<Consent> _consents;
        public List<Consent> Consents
        {
            get
            {
                if (_consents == null)
                    return new List<Consent>();
                else return _consents;
            }
            set
            {
                _consents = value;
            }
        }
    }
}

using PowerService.Util;
using System;

namespace PowerService.Data.Models
{
    public class OrganizationModel
    {
        private DateTime? _createdOn;
        public Guid? Id { get; set; }

        private DateTime _changedOn;
        [SwaggerIgnoreAttribute]
        public DateTime? CreatedOn
        {
            get
            { return _createdOn; }
            private set
            {
                if (_createdOn == null)
                    _createdOn = DateTime.Now;
                else
                    throw new Exception("Cannot change the date the record was created");
            }
        }
        [SwaggerIgnoreAttribute]
        public DateTime? ChangedOn
        {
            get => _changedOn;
            set => _ = DateTime.Now;
        }
        [SwaggerIgnoreAttribute]
        public Organization Organization { get; set; }
    }
}

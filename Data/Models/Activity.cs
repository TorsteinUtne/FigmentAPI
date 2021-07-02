using PowerService.Data.Models.RequestResponseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Util;

namespace PowerService.Data.Models
{
    public class Activity
    {
        public Activity()
        {
            if(CreatedOn == DateTime.MinValue) //Kun dersom denne ikke er satt
                CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        public Activity(ActivityRequest request, Guid ownerId, PowerServiceContext context)
        {
            if (CreatedOn == DateTime.MinValue) //Kun dersom denne ikke er satt
                CreatedOn = DateTime.Now;
            else
            {
                CreatedOn = request.CreatedOn;
            }
            ModifiedOn = DateTime.Now;
            Id = request.Id;
            Content = request.Content; //MÅ være base 64 - bør ha egen tjeneste som poster et innhold og konverterer dette til Base64

            Direction = request.Direction.ToString();
            FromPartyId = MappingFunctions.GetPartyIdFromHandle(request.FromHandle, context);
            ToPartyId = MappingFunctions.GetPartyIdFromHandle(request.ToHandle, context);
            ModifiedOn = DateTime.Now;
            OwnerId = ownerId;
            ReceivedOn = request.ReceivedOn;
            SentOn = request.SentOn;
            Attachments = request.Attachments;
            //AccountId = MappingFunctions.ExtractRelations<Activity>(typeof(Account), request.RelatedObjects);
            Name = request.Name;
            Description = request.Description;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
        [ForeignKey("Contacts")]
        public Guid FromPartyId { get; set; }
        [ForeignKey("Contacts")]
        public Guid ToPartyId { get; set; }

   
        [ForeignKey("Account")] public Guid? AccountId { get; set; }
        [ForeignKey("Case")] public Guid? CaseId { get; set; }
        [ForeignKey("Booking")] public Guid? BookingId { get; set; }
        [ForeignKey("Billing")] public Guid? BillingId { get; set; }
        [ForeignKey("Product")] public Guid? ProductId { get; set; }
        [ForeignKey("Purchase")] public Guid? PurchaseId { get; set; }
        [ForeignKey("Subscription")] public Guid? SubscriptionId { get; set; }
        [ForeignKey("Transaction")] public Guid? TransactionId { get; set; }
        public string Direction { get; set; }

        public string Content { get; set; }//Base64

        private DateTime _createdOn;

        public DateTime CreatedOn
        {
            get => _createdOn;
            private set => _createdOn = DateTime.Now;
        }
        private DateTime _sentOn;

        public DateTime? SentOn
        {
            get => _sentOn;
            private set => _sentOn = DateTime.Now;
        }
        private DateTime _receivedOn;

        public DateTime? ReceivedOn
        {
            get => _receivedOn;
            private set => _receivedOn = DateTime.Now;
        }
        private DateTime _modifiedOn;

        public DateTime? ModifiedOn
        {
            get => _modifiedOn;
            private set => _modifiedOn = DateTime.Now;
        }
       
        public List<Attachment> Attachments { get; set; }

        public string Status { get; set; }
    }
}

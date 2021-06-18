namespace PowerService.Data.Models
{
    public enum BookingStatus
    {
        Inquiry= 1,
        Reserved=2,
        Confirmed=3,
        Refundable=4,
        NonRefundable=5,
        Cancelled=6
    }
}
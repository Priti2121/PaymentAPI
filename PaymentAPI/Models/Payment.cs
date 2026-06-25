namespace PaymentAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string? CardOwnerName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationsDate { get; set; }
        public string? SecurityCode { get; set; }
    }
}

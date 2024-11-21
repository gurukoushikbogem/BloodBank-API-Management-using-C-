namespace BloodBank.Models
{
    public class BloodBankModel
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public int Age { get; set; }
        public string BloodType { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public decimal QuantityInMl { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
    }
}

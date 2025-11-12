namespace WebsiteCharity.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}

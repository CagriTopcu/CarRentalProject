namespace Entities.Concrete
{
    public class Payment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public string Cvv { get; set; }
    }
}
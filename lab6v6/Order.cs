namespace Lab6
{
    public class Order
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }

        public Order(int id, double amount, string status)
        {
            Id = id;
            Amount = amount;
            Status = status;
        }

        public override string ToString()
        {
            return $"Order #{Id}: {Amount} UAH, Status: {Status}";
        }
    }
}

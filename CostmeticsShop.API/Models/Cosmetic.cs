namespace CostmeticsShop.API.Models
{
    public class Cosmetic
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public bool Vegan { get; set;}
    }
}
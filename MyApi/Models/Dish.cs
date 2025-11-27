public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}

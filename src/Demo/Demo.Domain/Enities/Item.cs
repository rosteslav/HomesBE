namespace Demo.Domain.Enities
{
    public partial class Item(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
    }
}
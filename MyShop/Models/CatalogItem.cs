namespace MyShop.Models
{
    public sealed class CatalogItem
    {
        public CatalogItem(string name, string description, decimal price, string picturUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = picturUrl;
        }

        public CatalogItem() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public CatalogType CatalogType { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
    }
}

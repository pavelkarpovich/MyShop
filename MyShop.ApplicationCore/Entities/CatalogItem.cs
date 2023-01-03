namespace MyShop.ApplicationCore.Entities
{
    public sealed class CatalogItem
    {
        public CatalogItem() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }

        public CatalogItem(int catalogTypeId, int catalogBrandId, string name, string description, decimal price, string picturUrl)
        {
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = picturUrl;
        }
        public CatalogItem(int id, string name, string description, decimal price, string picturUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = picturUrl;
        }

        public void UpdateDetails(CatalogItemDetails details)
        {
            Name = details.Name;
            Price = details.Price;
        }

        public readonly record struct CatalogItemDetails
        {
            public string? Name { get; }
            public decimal Price { get; }
            public CatalogItemDetails(string? name, decimal price)
            {
                Name = name;
                Price = price;
            }
        }

    }
}

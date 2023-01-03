﻿namespace MyShop.ApplicationCore.Entities
{
    public sealed class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public CatalogType(string type)
        {
            Type = type;
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Data
{
    public sealed class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext, ILogger logger, int retry=0)
        {
            var retryForAvailability = retry;
            try
            {
                if (!await catalogContext.CatalogBrands.AnyAsync())
                {
                    await catalogContext.AddRangeAsync(GetPreconfiguredBrands());
                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogTypes.AnyAsync())
                {
                    await catalogContext.AddRangeAsync(GetPreconfiguredTypes());
                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogItems.AnyAsync())
                {
                    await catalogContext.AddRangeAsync(GetPreconfiguredItems());
                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability > 10) throw;
                retryForAvailability++;

                logger.LogError(ex.Message);
                SeedAsync(catalogContext, logger, retryForAvailability);
            }
        }

        private static IEnumerable<CatalogBrand> GetPreconfiguredBrands()
        {
            return new List<CatalogBrand>()
            {
                new("Azure"),
                new(".NET"),
                new("Visual Studio"),
                new("SQL Server"),
                new("Other")
            };
        }

        private static IEnumerable<CatalogType> GetPreconfiguredTypes()
        {
            return new List<CatalogType>()
            {
                new("Mug"),
                new("T-Shirt"),
                new("Sheet"),
                new("SQL Server"),
                new("USB Memory Stick")
            };
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new(2, 2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "/images/products/1.png"),
                new(1, 2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "/images/products/2.png"),
                new(2, 5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "/images/products/3.png"),
                new(2, 2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "/images/products/4.png"),
                new(3, 5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "/images/products/5.png"),
                new(2, 2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "/images/products/6.png"),
                new(2, 5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "/images/products/7.png"),
                new(2, 5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "/images/products/8.png"),
                new(1, 5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "/images/products/9.png"),
                new(3, 2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "/images/products/10.png"),
                new(3, 2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "/images/products/11.png"),
                new(2, 5, "Prism White TShirt", "Prism White TShirt", 12, "/images/products/12.png")
            };
        }
    }
}

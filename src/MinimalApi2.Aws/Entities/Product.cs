using MinimalApi2.Aws.Entities.Base;
using System.Collections.Generic;

namespace MinimalApi2.Aws.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<Image> ImageProducts { get; set; } = new();

        public Product()
        {

        }

        public Product(string name, string description, double price)
        {
            AssignId();
            Name = name;
            Description = description;
            Price = price;
        }

        public Product(Guid id, string name, string description, double price)
        {
            AssignId(id);
            Name = name;
            Description = description;
            Price = price;
        }


        public Product(string name, string description, double price, List<Image> imageProducts)
        {
            AssignId();
            Name = name;
            Description = description;
            Price = price;
            ImageProducts.AddRange(imageProducts);
        }

        public Product(Guid id, string name, string description, double price, List<Image> imageProducts)
        {
            AssignId(id);
            Name = name;
            Description = description;
            Price = price;
            ImageProducts.AddRange(imageProducts);
        }

        public static Product Create(string name, string description, double price)
            => new(name, description, price);

        public static Product Create(Guid id, string name, string description, double price)
            => new(id, name, description, price);

        public static Product Create(string name, string description, double price, List<Image> imageProducts)
            => new(name, description, price, imageProducts);

        public static Product Create(Guid id, string name, string description, double price, List<Image> imageProducts)
            => new(id, name, description
                , price, imageProducts);
        public void AddImage(Image imageProduct)
        {
            ImageProducts.Add(imageProduct);
        }

        public void AddImage(List<Image> imageProducts)
        {
            ImageProducts.AddRange(imageProducts);
        }
    }
}

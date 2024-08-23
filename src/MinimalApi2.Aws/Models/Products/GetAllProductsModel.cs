namespace MinimalApi2.Aws.Models.Product
{
    public class GetAllProductsModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<string> Urls { get; set; } = new();

        public GetAllProductsModel()
        {

        }

        public GetAllProductsModel(string name, string description, double price, List<string> urls)
        {
            Name = name;
            Description = description;
            Price = price;
            Urls.AddRange(urls);
        }

        public GetAllProductsModel ModelMapper(string name, string description, double price, List<string> urls)
        {
            Name = name;
            Description = description;
            Price = price;
            Urls.AddRange(urls);
            return this;
        }
    }
}

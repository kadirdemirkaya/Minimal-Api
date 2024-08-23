namespace MinimalApi2.Aws.Models.Product
{
    public class GetProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<string> Urls { get; set; }

        public GetProductModel()
        {

        }

        public GetProductModel(string name, string description, double price, List<string> urls)
        {
            Name = name;
            Description = description;
            Price = price;
            Urls.AddRange(urls);
        }

        public GetProductModel CreateModel(string name, string description, double price, List<string> urls)
        {
            Name = name;
            Description = description;
            Price = price;
            Urls.AddRange(urls);
            return this;
        }
    }
}

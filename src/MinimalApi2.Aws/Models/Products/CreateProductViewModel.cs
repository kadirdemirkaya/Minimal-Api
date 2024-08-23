using Microsoft.AspNetCore.Mvc;

namespace MinimalApi2.Aws.Models.Product
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }


        //public List<IFormFile> Images { get; set; } = new();

        //public List<IFormFileCollection> Images { get; set; } = new();

        public IFormFile Images { get; set; }
    }
}

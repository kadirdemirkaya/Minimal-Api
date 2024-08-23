using MinimalApi2.Aws.Entities.Base;

namespace MinimalApi2.Aws.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }


        public Image()
        {

        }

        public Image(string name, Guid productId)
        {
            AssignId();
            Name = name;
            ProductId = productId;
        }

        public Image(Guid id, string name, Guid productId)
        {
            AssignId(id);
            Name = name;
            ProductId = productId;
        }

        public static Image Create(string name, Guid productId)
            => new(name, productId);

        public static Image Create(Guid id, string name, Guid productId)
             => new(id, name, productId);

    }
}

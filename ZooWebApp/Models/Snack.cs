

namespace ZooWebApp.Models
{
    public class Snack
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }


        public int? AnimalId { get; set; }


        public Animal? Animal { get; set; }
    }
}

using Microsoft.Build.Framework;


namespace ZooWebApp.Models
{
    public class Animal
    {

        public int Id { get; set; }
        [Required]
        public string Species { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


        public List<Snack> Snacks { get; set; } = new List<Snack>();

        public Animal()
        {

        }

    }
}

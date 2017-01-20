using System.ComponentModel.DataAnnotations;

namespace Shadow_Arena.Controllers
{
    public class CreateCharacterViewModel
    {
        [Required]
        [DataType()]
        public string Name { get; set; }
    }
}
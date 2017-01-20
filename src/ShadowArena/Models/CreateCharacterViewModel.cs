using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shadow_Arena.Controllers
{
    public class CreateCharacterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Character Name")]
        [StringLength(maximumLength: 32, ErrorMessage = "Please make sure your chosen {0} is between {2} and {1} letters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Class")]
        public int ClassId { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NeoCrudModal.Models
{
    public class OcasionModel
    {
        public int Id { get; set; }
        [DisplayName("Ocasión")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Ocasion { get; set; } = null!;
    }
}

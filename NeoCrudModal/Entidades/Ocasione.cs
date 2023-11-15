using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NeoCrudModal.Entidades;

public partial class Ocasione
{
    public int Id { get; set; }

    [DisplayName("Ocasión")]
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string Ocasion { get; set; } = null!;

    public virtual ICollection<ProductoOcasione> ProductoOcasiones { get; set; } = new List<ProductoOcasione>();
}

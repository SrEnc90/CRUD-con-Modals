using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NeoCrudModal.Entidades;

public partial class Ocasione
{
    public int Id { get; set; }

    [DisplayName("Ocasión")]

    public string Ocasion { get; set; } = null!;

    public virtual ICollection<ProductoOcasione> ProductoOcasiones { get; set; } = new List<ProductoOcasione>();
}

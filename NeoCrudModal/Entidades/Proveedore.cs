using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Proveedore
{
    public int Id { get; set; }

    public string Proveedor { get; set; } = null!;

    public int? IdDireccion { get; set; }

    public virtual Direccione? IdDireccionNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

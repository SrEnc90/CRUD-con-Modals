using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class ProductoOcasione
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public int IdOcasion { get; set; }

    public virtual Ocasione IdOcasionNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Producto
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public virtual ICollection<ProductoOcasione> ProductoOcasiones { get; set; } = new List<ProductoOcasione>();
}

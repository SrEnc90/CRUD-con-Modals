using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Categoria
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdPadre { get; set; }

    public virtual Categoria? IdPadreNavigation { get; set; }

    public virtual ICollection<Categoria> InverseIdPadreNavigation { get; set; } = new List<Categoria>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

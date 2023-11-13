using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Direccione
{
    public int Id { get; set; }

    public int IdAsentamiento { get; set; }

    public string Calle { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public virtual CodigosPostale IdAsentamientoNavigation { get; set; } = null!;

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();
}

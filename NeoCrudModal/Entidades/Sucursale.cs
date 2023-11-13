using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Sucursale
{
    public int Id { get; set; }

    public string Sucursal { get; set; } = null!;

    public int IdDirecciones { get; set; }

    public virtual Direccione IdDireccionesNavigation { get; set; } = null!;
}

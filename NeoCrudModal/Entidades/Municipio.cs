using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class Municipio
{
    public int Id { get; set; }

    public int IdEstado { get; set; }

    public int Cve { get; set; }

    public string Municipio1 { get; set; } = null!;

    public virtual ICollection<CodigosPostale> CodigosPostales { get; set; } = new List<CodigosPostale>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}

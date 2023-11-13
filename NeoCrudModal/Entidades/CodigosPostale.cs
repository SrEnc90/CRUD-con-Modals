using System;
using System.Collections.Generic;

namespace NeoCrudModal.Entidades;

public partial class CodigosPostale
{
    public int Id { get; set; }

    public int IdMunicipio { get; set; }

    public int CodigoPostal { get; set; }

    public string Asentamiento { get; set; } = null!;

    public virtual ICollection<Direccione> Direcciones { get; set; } = new List<Direccione>();

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
}

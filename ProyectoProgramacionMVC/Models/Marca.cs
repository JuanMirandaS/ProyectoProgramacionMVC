using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Modelos = new HashSet<Modelo>();
        }

        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Modelo> Modelos { get; set; }
    }
}

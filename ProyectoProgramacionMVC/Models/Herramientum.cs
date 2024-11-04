using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Herramientum
    {
        public Herramientum()
        {
            Mantencions = new HashSet<Mantencion>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int HerramientaId { get; set; }
        public int ModeloId { get; set; }
        public string NumeroSerie { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }

        public virtual Modelo? Modelo { get; set; } = null!;
        public virtual ICollection<Mantencion> Mantencions { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}

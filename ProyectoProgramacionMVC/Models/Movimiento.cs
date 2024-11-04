using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Movimiento
    {
        public int MovimientoId { get; set; }
        public int HerramientaId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Observacion { get; set; }

        public virtual Herramientum? Herramienta { get; set; } = null!;
        public virtual Usuario? Usuario { get; set; } = null!;
    }
}

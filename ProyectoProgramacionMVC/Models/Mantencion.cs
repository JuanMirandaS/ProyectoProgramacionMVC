using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Mantencion
    {
        public int MantencionId { get; set; }
        public int HerramientaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Descripcion { get; set; }

        public virtual Herramientum? Herramienta { get; set; } = null!;
        public virtual Usuario? Usuario { get; set; } = null!;
    }
}

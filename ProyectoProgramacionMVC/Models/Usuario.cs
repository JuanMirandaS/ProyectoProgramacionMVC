using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Mantencions = new HashSet<Mantencion>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Mantencion> Mantencions { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}

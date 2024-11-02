using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class VwHerramientasEnUsoPorUsuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public int? HerramientasEnUso { get; set; }
    }
}

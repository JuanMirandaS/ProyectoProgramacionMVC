using System;
using System.Collections.Generic;

namespace ProyectoProgramacionMVC.Models
{
    public partial class Modelo
    {
        public Modelo()
        {
            Herramienta = new HashSet<Herramientum>();
        }

        public int ModeloId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual Marca Marca { get; set; } = null!;
        public virtual ICollection<Herramientum> Herramienta { get; set; }
    }
}

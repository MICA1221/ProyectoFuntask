using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunTask.Models
{
    public class Hijo
    {
        [Key]
        public int HijoId { get; set; }
        [Required]
        public string? UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
        [Required]
        public string? NombreHijo { get; set; }
        public string? ImagenPerfil { get; set; }

        public ICollection<Actividad>? Actividades { get; set; }
    }
}


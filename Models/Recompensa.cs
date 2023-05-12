using System.ComponentModel.DataAnnotations;

namespace FunTask.Models
{
    public class Recompensa
    {
        [Key]
        public int RecompensaId { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public string? Imagen { get; set; }

        public string? Descripcion { get; set; }
    }
}


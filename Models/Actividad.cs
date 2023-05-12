using System.ComponentModel.DataAnnotations;

namespace FunTask.Models
{    

        public class Actividad
        {
            [Key]
            public int ActividadId { get; set; }

            public int HijoId { get; set; }

            public Hijo? Hijo { get; set; }

            [Required]
            public string? Titulo { get; set; }

            public string? Imagen { get; set; }

            public string? Descripcion { get; set; }

            public int DiasParaRecompensa { get; set; }

            public int DiasCompletados { get; set; }

            public int RecompensaId { get; set; }

            public Recompensa? Recompensa { get; set; }
        }

}

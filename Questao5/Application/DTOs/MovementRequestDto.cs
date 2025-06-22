using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.DTOs
{
    public class MovementRequestDto
    {
        [Required]
        public string IdContaCorrente { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public string TipoMovimento { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.PacientesDtos
{
    public class PostPacienteDto
    {
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? Cedula { get; set; }
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public DateTime FechaNacimiento { get; set; }

    }
}
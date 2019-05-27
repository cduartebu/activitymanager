namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Team
    {        
        public int TeamId { get; set; }

        [Required(ErrorMessage = "The TeamName field is required.")]
        [StringLength(200)]
        public string TeamName { get; set; }

        public DateTime CreatedDt { get; set; }

        public int ProjectId { get; set; }       
        
    }
}

namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Team
    {        
        public int TeamId { get; set; }

        [Required]
        [StringLength(200)]
        public string TeamName { get; set; }

        public DateTime CreatedDt { get; set; }

        public int ProjectId { get; set; }
        
        public virtual Project Project { get; set; }
        
    }
}

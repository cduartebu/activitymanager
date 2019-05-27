namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Project
    {       
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "The ProjectName field is required.")]
        [StringLength(100)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(800)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDt { get; set; }        
    }
}

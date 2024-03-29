namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Deliverable
    {
        public int DeliverableId { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDt { get; set; }

        public int ProjectId { get; set; }
    }
}

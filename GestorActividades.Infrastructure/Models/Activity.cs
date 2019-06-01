using System;
using System.ComponentModel.DataAnnotations;

namespace GestorActividades.Infrastructure.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDt { get; set; }

        public int TeamId { get; set; }
    }
}

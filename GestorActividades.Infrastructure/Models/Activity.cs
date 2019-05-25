using System;
using System.ComponentModel.DataAnnotations;

namespace GestorActividades.Infrastructure.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDt { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
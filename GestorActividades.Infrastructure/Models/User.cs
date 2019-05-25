namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        [StringLength(300)]
        public string EmailAddress { get; set; }

        public DateTime CreatedDt { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}

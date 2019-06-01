namespace GestorActividades.Infrastructure.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "The UserName field is required.")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The FirstName field is required.")]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName field is required.")]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The EmailAddress field is required.")]
        [StringLength(300)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(250)]
        public string Password { get; set; }

        public DateTime CreatedDt { get; set; }

        public int? TeamId { get; set; }
    }
}

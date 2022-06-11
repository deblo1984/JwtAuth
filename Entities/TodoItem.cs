using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JwtAuth.Models;

namespace JwtAuth.Entities
{
    public class TodoItem
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
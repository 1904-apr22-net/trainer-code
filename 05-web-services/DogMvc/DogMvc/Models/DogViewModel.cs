using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogMvc.Models
{
    public class DogViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        [Display(Name = "Owner")]
        public string OwnerEmail { get; set; }

        [Display(Name = "Owner")]
        public string OwnerName { get; set; }

        public IEnumerable<SelectListItem> Accounts { get; set; }
    }
}

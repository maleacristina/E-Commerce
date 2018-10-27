using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class County
    {
        public int CountyId { get; set; }

        [Required]
        public string NameCounty { get; set; }
        public List<City> Cities { get; set; }
    }
}
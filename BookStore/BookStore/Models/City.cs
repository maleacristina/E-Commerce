using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class City
    {
        public int CityId { get; set; }

        [Required]
        public string NameCity { get; set; }
    }
}
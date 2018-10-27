using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class OrderFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int City { get; set; }

        public IEnumerable<City> Cities { get; set; }

        [Required]
        public int County { get; set; }

        public IEnumerable<County> Counties { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [Required]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
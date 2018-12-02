using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class NewBook
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
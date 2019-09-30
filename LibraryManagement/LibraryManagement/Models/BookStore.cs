using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookStore
    {
        public int BookStoreID { get; set; }
        public string Title { get; set; }
        public string SerialNumber { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        public string BookPath { get; set; }


        [DisplayName("Images")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public BookStore()
        {
            ImagePath = "~/AppFiles/default.png";
        }
    }
}
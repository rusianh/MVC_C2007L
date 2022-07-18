using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAP_C2007L_NguyenDucHuy.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        public string GenreName { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public Genre()
        {
            this.Albums = new HashSet<Album>();
        }
    }
}
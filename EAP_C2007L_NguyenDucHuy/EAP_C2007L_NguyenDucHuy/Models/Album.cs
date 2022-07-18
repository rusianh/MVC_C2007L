using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAP_C2007L_NguyenDucHuy.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required]
        [MaxLength(32),MinLength(3)]
        public  string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0: MM/dd/yyyy HH:MM:SS tt}")]
        public DateTime ReleaseDate{ get; set; }
        [Required]
        public string  Artist { get; set; }
        [Required]
        [Range(1,15)]
        public double Price { get; set; }

        public int GenreId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Genre Genres { get; set; }
    }
}
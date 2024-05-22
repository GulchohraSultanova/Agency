using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  public  class Portfolio
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Uzunluq 50den cox ola bilmez!")]
        public string Title { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Uzunluq 50den cox ola bilmez!")]
        public string SubTitle { get; set; }
      
        public string ? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ? PhotoFile { get; set; }
    }
}

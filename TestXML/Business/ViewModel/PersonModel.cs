using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestXML.Business.ViewModel
{
    public class PersonModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }

        [Phone]
        [StringLength(10, ErrorMessage = "Enter Correct Number.", MinimumLength = 10)]
        [DisplayName("Cellphone")]
        [Required(ErrorMessage = "Cell Number Required")]
        public string Cellphone { get; set; }

        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [NotMapped]
        public string Base64String { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}
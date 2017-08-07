using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TestXML.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
        public byte[] Image { get; set; }
        public String Base64String { get; set; }
        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}
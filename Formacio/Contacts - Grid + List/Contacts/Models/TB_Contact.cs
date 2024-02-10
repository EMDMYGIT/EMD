using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class TB_Contact
    {
        //ContactID, Name, Surname, Surname2, Telephone, e_mail
        
        public int ContactID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }
        [DataType(DataType.Text)]
        public string Surname2 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string e_mail { get; set; }
        [DataType(DataType.Text)]
        public String City { get; set; }
        [DataType(DataType.Text)]
        public string Country { get; set; }


        // Comentarios del modelo
        [DataType(DataType.MultilineText)]
        public virtual TB_Comments ContactComment { get; set; }

    }
    
}
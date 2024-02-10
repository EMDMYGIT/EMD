using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class TB_Comments
    {
        //CommentID, Comment, ParentType, ParentId
        public int CommentID { get; set; }
        [DataType(DataType.MultilineText)]
        public string  Comment { get; set; }
        public string  ParentType { get; set; }
        public int ParentId { get; set; }
    }
}
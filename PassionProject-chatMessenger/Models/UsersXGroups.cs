using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_chatMessenger.Models
{
    public class UsersXGroups
    {
        public int UsersXGroupsId { get; set; }

        public int UserId { get; set; }
        //public AspNetUser User { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
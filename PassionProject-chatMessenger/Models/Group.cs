using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_chatMessenger.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }

        // A Group can have many messages

        public ICollection<Message> Messages { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }

    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

    }
}
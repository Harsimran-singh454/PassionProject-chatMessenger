using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_chatMessenger.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        // Additional group properties


        //a group can have many users
        //public ICollection<User> Users { get; set; }

        // A group can have many messages
        public ICollection<Message> Messages { get; set; }
    }
}
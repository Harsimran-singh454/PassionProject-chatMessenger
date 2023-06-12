using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_chatMessenger.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Content { get; set; }


        public int UserId { get; set; }


        // a message can belong to one group
        // Single group can have many messages
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public DateTime Timestamp { get; set; }
    }

    public class MessageDto
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public string GroupName { get; set; }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_chatMessenger.Models.ViewModels
{
    public class GroupMessages
    {
        // This View model is for details about all the messages for a selected Group
        public GroupDto SelectedGroup { get; set; }
        public IEnumerable<MessageDto> MessagesForGroup { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_chatMessenger.Models;

namespace PassionProject_chatMessenger.Controllers
{
    public class MessageDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MessageData/List
        /// <summary>
        /// This Function is for listing all the messages
        /// </summary>
        /// <returns>All the messages in the datavase</returns>
        [HttpGet]
        [ResponseType(typeof(GroupDto))]
        public IEnumerable<MessageDto> List()
        {
            List<Message> messages = db.Messages.ToList();
            List<MessageDto> MessageDtos = new List<MessageDto>();

            messages.ForEach(m => MessageDtos.Add(new MessageDto()
            {
                MessageId = m.MessageId,
                //user1 =m.user1,
                //user2 =m.user2,
                Content = m.Content,
                GroupId = m.Id,
                GroupName = m.Group.GroupName,
            }));

            return MessageDtos;
        }



        // GET: api/MessageData/ListMessagesForGroup/{group id}
        /// <summary>
        /// This function is used for fetching messages associated to a specific group with matching ID
        /// </summary>
        /// <param name="id">The Group Id</param>
        /// <returns>Returns list of messages associated with the matching group</returns>
        [HttpGet]
        public IHttpActionResult ListMessagesForGroup(int id)
        {
            List<Message> Messages = db.Messages.Where(a => a.Id == id).ToList();
            List<MessageDto> MessageDtos = new List<MessageDto>();

            Messages.ForEach(a => MessageDtos.Add(new MessageDto()
            {
                MessageId = a.MessageId,
                Content = a.Content,
                GroupId = a.Group.Id,
                GroupName = a.Group.GroupName
            }));

            return Ok(MessageDtos);
        }


        // GET: api/MessageData/FindMessage/5
        /// <summary>
        /// This function is used for fetching the message with matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns details about a message</returns>
        [ResponseType(typeof(Message))]
        [HttpGet]
        public IHttpActionResult FindMessage(int id)
        {
            Message Message = db.Messages.Find(id);
            MessageDto MessageDto = new MessageDto()
            {
                MessageId = Message.MessageId,
                //user1 = Message.user1,
                //user2 = Message.user2,
                Content = Message.Content,
                GroupId = Message.Id,
                GroupName = Message.Group.GroupName,
            };
            if (Message == null)
            {
                return NotFound();
            }

            return Ok(MessageDto);
        }


        //PUT: api/MessageData/updateMessage/5
        /// <summary>
        /// This Function is used for updating a message with matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns>Uppdates the Message with matching Id</returns>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult updateMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.MessageId)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MessageData/addMessage
        /// <summary>
        /// This function adds a new message to the database
        /// </summary>
        /// <param name="message">This object holds the data passed from the form</param>
        /// <returns>Creates a new message in the database</returns>
        [ResponseType(typeof(Message))]
        [HttpPost]
        public IHttpActionResult addMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/MessageData/DeleteMessage/5
        /// <summary>
        /// Finds the message matching with the Id passed an deletes it from the database
        /// </summary>
        /// <param name="id">the targeted MessageId</param>
        /// <returns>Removes a message from the database</returns>
        [ResponseType(typeof(Message))]
        [HttpPost]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }
    }
}
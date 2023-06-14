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

        // GET: api/MessageData/listMessages
        [HttpGet]
        [ResponseType(typeof(GroupDto))]
        public IEnumerable<MessageDto> List()
        {
            List<Message> messages = db.Messages.ToList();
            List<MessageDto> MessageDtos = new List<MessageDto>();

            messages.ForEach(m => MessageDtos.Add(new MessageDto()
            {
                MessageId = m.MessageId,
                user1 =m.user1,
                user2 =m.user2,
                Content = m.Content,
                GroupId = m.GroupId,
                GroupName = m.Group.GroupName,
            }));

            return MessageDtos;
        }


        // GET: api/MessageData/FindMessage/5
        [ResponseType(typeof(Message))]
        [HttpGet]
        public IHttpActionResult FindMessage(int id)
        {
            Message Message = db.Messages.Find(id);
            MessageDto MessageDto = new MessageDto()
            {
                MessageId = Message.MessageId,
                user1 = Message.user1,
                user2 = Message.user2,
                Content = Message.Content,
                GroupId = Message.GroupId,
                GroupName = Message.Group.GroupName,
            };
            if (Message == null)
            {
                return NotFound();
            }

            return Ok(MessageDto);
        }


        //PUT: api/MessageData/updateMessage/5
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
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
using System.Diagnostics;
using PassionProject_chatMessenger.Migrations;

namespace PassionProject_chatMessenger.Controllers
{
    public class GroupsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/GroupsData/ListGroups

        /// <summary>
        /// This function is for fetching list of groups and messages associated with it
        /// </summary>
        /// <returns> Returns a list of groups in the database </returns>
        [HttpGet]
        [ResponseType(typeof(GroupDto))]
        public IEnumerable<GroupDto> ListGroups()
        {
            List<Group> Groups = db.Groups.ToList();
            List<GroupDto> GroupDtos = new List<GroupDto>();
            List<Message> MessageList = new List<Message>();

            Groups.ForEach(g => GroupDtos.Add(new GroupDto()
            {
                Id = g.Id,
                GroupName = g.GroupName,
                Messages = MessageList
            }));

            return GroupDtos;
        }





        // GET: api/GroupsData/FindGroup/5

        /// <summary>
        /// This function is for finding a group in the database using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return details of a group with matching ID</returns>
        [ResponseType(typeof(Group))]
        [HttpGet]
        public IHttpActionResult FindGroup(int id)
        {
            Group Group = db.Groups.Find(id);
            List<Message> msgs = db.Messages.Where(x => x.Id == id).ToList();
            GroupDto GroupDto = new GroupDto()
            {
                Id = Group.Id,
                GroupName = Group.GroupName,
                Messages = msgs

            };
            if (Group == null)
            {
                return NotFound();
            }

            return Ok(GroupDto);
        }





        // PUT: api/GroupsData/UpdateGroup/5
        /// <summary>
        /// This function is for updating the group with matching ID and data to be entered
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns>Returns the updated group</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateGroup(int id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/GroupsData/AddGroup
        /// <summary>
        /// This function is for adding a new group to the database
        /// </summary>
        /// <param name="group">This parameters holds the values passed fromo the form</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Group))]
        public IHttpActionResult AddGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        }

        // DELETE: api/GroupsData/DeleteGroup/5
        /// <summary>
        /// This function is used for deleteing the group with matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delets the group from the database</returns>
        [HttpPost]
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(int id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Count(e => e.Id == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;
using PassionProject_chatMessenger.Models;

namespace PassionProject_chatMessenger.Controllers
{
    public class MessageController : Controller

    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        /// <summary>
        /// Constructors
        /// </summary>
        static MessageController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/");
        }

        // GET: Message
        /// <summary>
        /// This function returns a view with a list of messages
        /// </summary>
        /// <returns>Returns a view with list of messages</returns>
        public ActionResult List()
        {
            string url = "List";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<MessageDto> messages = response.Content.ReadAsAsync<IEnumerable<MessageDto>>().Result;
 
            return View(messages);
        }

        // GET: Message/Details/5
        /// <summary>
        /// This function is for getting details of a message on a view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a view with message details</returns>
        public ActionResult Details(int id)
        {
            string url = "MessageData/FindMessage/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MessageDto selectedMessage = response.Content.ReadAsAsync<MessageDto>().Result;

            Debug.WriteLine("Message Details : ");
            Debug.WriteLine(selectedMessage.Content);


            return View(selectedMessage);
        }

        // GET: Message/Create
        /// <summary>
        /// This functions returns a view with create form for messages
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        /// <summary>
        /// This function calls api to add message to the database
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Returns a view with updated list of messages</returns>
        [HttpPost]
        public ActionResult Create(Models.Message message)
        {
            Debug.WriteLine("the json payload is :");

            //objective: add a new message using the API
            //curl -H "Content-Type:application/json" -d @addMessage.json https://localhost:44324/api/MessageData/addMessage 

            string url = "MessageData/addMessage";


            string jsonpayload = jss.Serialize(message);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Group", new { 
                    id = message.Id
                });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        /// <summary>
        /// This method returns a view in case an error occurs
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {

            return View();
        }

        // GET: Message/Edit/5
        /// <summary>
        /// This function is for fetching details of a message and sending them to a view
        /// </summary>
        /// <param name="id">id of target message</param>
        /// <returns>Returns a view with update form and message data</returns>
        public ActionResult Edit(int id)
        {
            string url = "MessageData/FindMessage/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GroupDto groupSeleted = response.Content.ReadAsAsync<GroupDto>().Result;
            return View(groupSeleted);
        }

        // POST: Message/Edit/5
        /// <summary>
        /// This function calls api to update a message with matching id
        /// </summary>
        /// <param name="id">target messageId</param>
        /// <param name="message">form data</param>
        /// <returns>Returns list of messages with updated message</returns>
        [HttpPost]
        public ActionResult Edit(int id, Message message)
        {
            string url = "MessageData/updateMessage/" + id;
            string jsonpayload = jss.Serialize(message);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Message/Delete/5
        /// <summary>
        /// This function finds a message and returns a view to delete that message
        /// </summary>
        /// <param name="id">targer messageId</param>
        /// <returns>Return a view with delete options</returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            string url = "MessageData/FindMessage/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MessageDto selectedMessage = response.Content.ReadAsAsync<MessageDto>().Result;

            Debug.WriteLine("Message Details : ");
            Debug.WriteLine(selectedMessage.Content);


            return View(selectedMessage);
        }

        // POST: Message/Delete/5
        /// <summary>
        /// This function calls api to delete a message from the database
        /// </summary>
        /// <param name="id">target messageId</param>
        /// <param name="GroupId">target GroupId to return back to the group to display messages</param>
        /// <returns>Return a group with delete message</returns>
        [HttpPost]
        public ActionResult DeleteConfirm(int id, int GroupId)
        {
            string url = "MessageData/DeleteMessage/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Group", new
                {
                    id = GroupId
                });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}

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

        static MessageController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/");
        }

        // GET: Message
        public ActionResult List()
        {
            string url = "List";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<MessageDto> messages = response.Content.ReadAsAsync<IEnumerable<MessageDto>>().Result;
 
            return View(messages);
        }

        // GET: Message/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create

        [HttpPost]
        public ActionResult Create(Message message)
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
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "MessageData/FindMessage/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GroupDto groupSeleted = response.Content.ReadAsAsync<GroupDto>().Result;
            return View(groupSeleted);
        }

        // POST: Message/Edit/5
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "MessageData/DeleteMessage/" + id;
            HttpContent content = new StringContent("");
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
    }
}

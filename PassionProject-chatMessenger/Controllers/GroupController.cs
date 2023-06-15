using PassionProject_chatMessenger.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject_chatMessenger.Controllers
{
    public class GroupController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static GroupController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/GroupsData/");
        }
        // GET: Group
        public ActionResult List()
        {

            // Retrieve list of animals from GroupsData Api
            //curl-https://localhost:44325/api/GroupsData/ListGroups

            string url = "ListGroups";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Group> groups = response.Content.ReadAsAsync<IEnumerable<Group>>().Result;

            return View(groups);
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve details of one group using GroupsData Api
            //curl-https://localhost:44325/api/GroupsData/FindGroup/{id}

            string url = "FindGroup/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Group SelectedGroup = response.Content.ReadAsAsync<Group>().Result;

            return View(SelectedGroup);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/CreateGroup/{forData}
        [HttpPost]
        public ActionResult Create(Group group)
        {
            Debug.WriteLine("the json payload is :");

            //objective: add a new group using the API
            //curl -H "Content-Type:application/json" -d @addGroup.json https://localhost:44324/api/GroupData/AddGroup 

            string url = "GroupData/AddGroup";


            string jsonpayload = jss.Serialize(group);
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



        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
  
            string url = "GroupData/FindGroup/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GroupDto groupSeleted = response.Content.ReadAsAsync<GroupDto>().Result;
            return View(groupSeleted);
        }

        // POST: Group/Update/5
        [HttpPost]
        public ActionResult Update(int id, Group group)
        {
            string url = "UpdateGroup/" + id;
            string jsonpayload = jss.Serialize(group);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

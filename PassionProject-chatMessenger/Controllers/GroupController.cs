﻿using PassionProject_chatMessenger.Models;
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
            client.BaseAddress = new Uri("https://localhost:44325/api/");
        }
        // GET: Group
        public ActionResult List()
        {

            // Retrieve list of animals from GroupsData Api
            //curl-https://localhost:44325/api/GroupsData/ListGroups

            string url = "GroupsData/ListGroups";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<GroupDto> groups = response.Content.ReadAsAsync<IEnumerable<GroupDto>>().Result;

            return View(groups);
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve details of one group using GroupsData Api
            //curl-https://localhost:44325/api/GroupsData/FindGroup/{id}

            string url = "GroupsData/FindGroup/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            GroupDto SelectedGroup = response.Content.ReadAsAsync<GroupDto>().Result;

            return View(SelectedGroup);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/CreateGroup/{forData}
        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            Debug.WriteLine("the json payload is :");

            //objective: add a new group using the API
            //curl -H "Content-Type:application/json" -d @addGroup.json https://localhost:44324/api/GroupData/AddGroup 

            string url = "GroupsData/AddGroup";


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

            //objective: Update a group using the API
            //curl -H "Content-Type:application/json" -d @updateGroup.json https://localhost:44324/api/GroupData/FindGroup/{id}


            string url = "GroupsData/FindGroup/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GroupDto groupSeleted = response.Content.ReadAsAsync<GroupDto>().Result;
            return View(groupSeleted);
        }

        // POST: Group/Update/5
        [HttpPost]
        public ActionResult Update(int id, Group group)
        {
            string url = "GroupsData/UpdateGroup/" + id;
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
            string url = "GroupsData/FindGroup/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            GroupDto SelectedGroup = response.Content.ReadAsAsync<GroupDto>().Result;

            return View(SelectedGroup);
        }

        // POST: Group/DeleteConfirm/5
        [HttpPost]
        public ActionResult DeleteConfirm(int id, FormCollection collection)
        {
            string url = "GroupsData/DeleteGroup/" + id;
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

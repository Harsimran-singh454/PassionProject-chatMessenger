using PassionProject_chatMessenger.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PassionProject_chatMessenger.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult List()
        {

            // Retrieve list of animals from MessageData Api
            //curl-https://localhost:44325/api/GroupsData/ListGroups

            HttpClient client = new HttpClient();

            string url = "https://localhost:44325/api/GroupsData/ListGroups";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("Response Code:");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Group> groups = response.Content.ReadAsAsync<IEnumerable<Group>>().Result;
            Debug.WriteLine("Number of groups");
            Debug.WriteLine(groups.Count());
            return View(groups);
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve details of one group using MessageData Api
            //curl-https://localhost:44325/api/GroupsData/FindGroup/{id}

            HttpClient client = new HttpClient();
            string url = "https://localhost:44325/api/GroupsData/FindGroup/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("Response Code:");
            Debug.WriteLine(response.StatusCode);

            Group SelectedGroup = response.Content.ReadAsAsync<Group>().Result;
            Debug.WriteLine("Number of groups");
            Debug.WriteLine(SelectedGroup.GroupName);
            return View(SelectedGroup);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

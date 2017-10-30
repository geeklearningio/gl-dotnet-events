using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeekLearning.Events.Sample.Models;

namespace GeekLearning.Events.Sample.Controllers
{
    public class HomeController : Controller
    {
        public IEventQueuer queue;
        public IEventQueuer queueAzure;

        public HomeController (IEventFactory factory)
        {
            this.queue = factory.GetQueuer("queue1");
            //this.queueAzure = factory.GetQueuer("queue2");
        }

        public IActionResult Index()
        {
            this.queue.QueueEvent(new EventTest("Swag"));
            this.queue.CommitAsync();
            /*this.queueAzure.QueueEvent(new EventTest("Azure"));
            this.queueAzure.CommitAsync();*/
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

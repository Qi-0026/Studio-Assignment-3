﻿using FIT5032_Week08A.Models;
using FIT5032_Week08A.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Week08A.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Please comment out these codes once you have registered your API key.
            //EmailSender es = new EmailSender();
            //es.RegisterAPIKey();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    var attachment = Request.Files["attachment"];

                    //check upload
                    if (attachment.ContentLength > 0)
                    {
                        String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
                        attachment.SaveAs(path);

                        EmailSender es = new EmailSender();
                        es.SendAttachment(toEmail, subject, contents, path, attachment.FileName);
                    }
                    else
                    {
                        EmailSender es = new EmailSender();
                        es.Send(toEmail, subject, contents);
                    }
                    // save file to server
                    

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;

namespace MvcProjectCamp.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading
        private HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        private CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        private WriterManager writerManager = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var result = headingManager.GetAll();
            return View(result);
        }

        public ActionResult HeadingReport()
        {
            var result = headingManager.GetAll();
            return View(result);
        }
        [HttpGet]
        public ActionResult Add()
        {
            List<SelectListItem> valueCategory = (from x in categoryManager.GetAll()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.categoryVlc = valueCategory;

            List<SelectListItem> valueWriter = (from x in writerManager.GetAll()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurName,
                                                    Value = x.WriterId.ToString()
                                                }).ToList();
            ViewBag.writerVlc = valueWriter;

            return View();

        }
        [HttpPost]
        public ActionResult Add(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            headingManager.Add(heading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueWriter = (from x in writerManager.GetAll()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurName,
                                                    Value = x.WriterId.ToString()
                                                }).ToList();
            ViewBag.writerVlc = valueWriter;

            List<SelectListItem> valueCategory = (from x in categoryManager.GetAll()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.categoryVlc = valueCategory;

            var headingValue = headingManager.GetById(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            headingManager.Update(heading);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var headingValue = headingManager.GetById(id);
            if (headingValue.HeadingStatus == true)
            {
                headingValue.HeadingStatus = false;
            }
            else
            {
                headingValue.HeadingStatus = true;
            }
            headingManager.Delete(headingValue);
            return RedirectToAction("Index");
        }
    }
}
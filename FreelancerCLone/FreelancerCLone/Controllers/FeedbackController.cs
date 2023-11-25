﻿using FreelancerCLone.DbModels;
using FreelancerCLone.Services;
using FreelancerCLone.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerCLone.Controllers
{
    public class FeedbackController : Controller
    {

        public IActionResult Index()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            try
            {
                feedbacks = FeedbackUtility.Instance.GetPendingFeedbacks();
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.ErrorLoggingFunction(ex.Message, this.ToString());
            }
            return View(feedbacks);
        }



        [HttpPost]
        public IActionResult Create(Feedback model)
        {
            try
            {
                FeedbackUtility.Instance.CreateFeedback(model, User.Identity.Name);
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.ErrorLoggingFunction(ex.Message, this.ToString());
            }
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Details(int Id)
        {
            try
            {
                Feedback model = FeedbackUtility.Instance.GetFeedback(Id);
                return PartialView("FeedbackDetailsPartialView", model);
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.ErrorLoggingFunction(ex.Message, this.ToString());
            }
            return RedirectToAction("Index");
        }



        public IActionResult UpdateStatus(Feedback model)
        {
            try
            {
                FeedbackUtility.Instance.UpdateFeedbackStatusToAccept(model);
            }
            catch (Exception ex)
            {
                ErrorLogger.Instance.ErrorLoggingFunction(ex.Message, this.ToString());
            }
            return RedirectToAction("Index");
        }


    }
}
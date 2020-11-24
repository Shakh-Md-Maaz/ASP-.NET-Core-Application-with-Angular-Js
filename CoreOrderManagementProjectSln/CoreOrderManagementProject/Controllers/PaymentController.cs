using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreOrderManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoreOrderManagementProject.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ApplicationDbContext _entities;

        public PaymentController(ApplicationDbContext entities)
        {
            _entities = entities;
        }
        public ViewResult Index()
        {

            return View();
        }


        [HttpGet]
        public JsonResult PaymentList()
        {
            var data = _entities.Payments.ToList();

            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());

        }
        [HttpPost]
        public JsonResult AddPayment([FromBody]Payment payment)
        {

            _entities.Payments.Add(payment);
            _entities.SaveChanges();
            var p = _entities.Payments.ToList();
            return Json(p, new Newtonsoft.Json.JsonSerializerSettings());

        }
        [HttpPost]
        public JsonResult UpdatePayment([FromBody]Payment payment)
        {
            var uppayment = _entities.Payments.FirstOrDefault(x => x.Id == payment.Id); /*Ei Line ta modify korechi*/
            uppayment.CardNO = payment.CardNO;
            uppayment.Pin = payment.Pin;
            _entities.Entry(uppayment).State = EntityState.Modified; /*Ei Line ta add korechi*/
            _entities.SaveChanges();
            var p = _entities.Patients.ToList(); /*Ei Line ta add korechi*/
            return Json(p, new Newtonsoft.Json.JsonSerializerSettings());


        }

        public string DeletePayment(int? id)
        {
            var data = _entities.Payments
                   .Where(x => x.Id == id)
                   .Select(x => x)
                   .FirstOrDefault();

            if (data != null)
            {
                _entities.Payments.Remove(data);
            }
            _entities.SaveChanges();

            return "Delete Success full";
        }
    }
}

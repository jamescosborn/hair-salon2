using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Stylist> model = Stylist.GetAll();
      return View(model);
    }

    [HttpGet("/stylists/add")]
    public ActionResult AddStylist()
    {
      return View();
    }

    [HttpPost("/stylists/stylistadded")]
    public ActionResult AddStylistSuccess()
    {
      string stylistName = Request.Form["stylist-name"];
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();

      List<Stylist> model = Stylist.GetAll();
      return View(model);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Stylist stylist = Stylist.FindById(id);
      return View(stylist);
    }
  }
}
    //
    // [HttpGet("/stylists/{id}")]
    // public ActionResult StylistDetails(int id)
    // {
    //   StylistDetailsModel model = new StylistDetailsModel(id);
    //   return View(model);
    // }
    //
    // [HttpPost("/stylists/{stylistId}/clients/add")]
    // public ActionResult AddClientToStylist(int stylistId)
    // {
    //   string clientName = Request.Form["client-name"];
    //
    //   Client newClient = new Client(clientName, stylistId);
    //   newClient.Save();
    //
    //   StylistDetailsModel model = new StylistDetailsModel(stylistId);
    //   return View("StylistDetails", model);
    // }
//   }
// }

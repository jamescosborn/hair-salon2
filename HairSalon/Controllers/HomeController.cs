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

    [HttpPost("/stylists/addclientsuccess")]
    public ActionResult AddClientSuccess()
    {
      string clientName = Request.Form["client-name"];
      Client newClient = new Client(clientName);
      newClient.Save();
      List<Client> model = Client.GetAll();

      return View("StylistDetail", model);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.FindById(id);
      List<Client> stylistsClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("client", stylistsClients);

      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/add")]
    public ActionResult AddClientToStylist(int stylistId)
    {
      string clientName = Request.Form["client-name"];

      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      List<Client> model = Client.GetAll();
      return View("StylistDetail", model);
    }
  }
}
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

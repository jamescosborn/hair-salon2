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

    [HttpPost("/stylists/add/stylist")]
    public ActionResult AddStylistSuccess()
    {
      string stylistName = Request.Form["stylist-name"];
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();

      List<Stylist> model = Stylist.GetAll();
      return View(model);
    }

    [HttpPost("/stylists/clients/add")]
    public ActionResult AddClientSuccess()
    {
      return View("AddClientSuccess");
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}/update")]
    public ActionResult UpdateClient(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.FindById(stylistId);
      Client selectedClient = Client.Find(clientId);
      model.Add("stylist", selectedStylist);
      model.Add("client", selectedClient);
      return View();
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}/update/success")]
    public ActionResult UpdateClientSuccess(int clientId)
    {
      Client selectedClient = Client.Find(clientId);
      selectedClient.Update(Request.Form["new-name"], clientId);
      return View("UpdateClientSuccess");
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}/delete/success")]
    public ActionResult DeleteClientSuccess(int sylistId, int clientId)
    {
      Client.Delete(clientId)
      return View("DeleteClientSuccess");
    }

    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult AddClient(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.FindById(stylistId);
      model.Add("stylist", selectedStylist);
      return View(model);
    }
    [HttpPost("/stylists/{stylistId}/clients/add")]
    public ActionResult AddClientToStylist(int stylistId)
    {
      string clientName = Request.Form["client-name"];
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();

      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.FindById(stylistId);
      List<Client> stylistsClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistsClients);

      return View("StylistDetail", model);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.FindById(id);
      List<Client> stylistsClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistsClients);

      return View(model);
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

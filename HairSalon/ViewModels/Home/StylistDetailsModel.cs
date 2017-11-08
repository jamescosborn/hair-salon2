using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.ViewModels
{
  public class StylistDetailsModel
  {
    public Stylist CurrentStylist {get; set;}
    public List<Client> Clients {get; set;}

    public StylistDetailsModel(int currentStylistId)
    {
      CurrentStylist = Stylist.FindById(currentStylistId);
      Clients = CurrentStylist.GetClients();

    }
  }
}

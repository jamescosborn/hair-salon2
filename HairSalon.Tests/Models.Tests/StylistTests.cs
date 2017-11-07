using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=james_osborn_test;";
    }
    public void Dispose()
    {
      Stylist.ClearAll();
    }

    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void HasSamePropertiesAs_BothHaveSameProperties_True()
    {
      Stylist stylist1 = new Stylist("Zeus");
      Stylist stylist2 = new Stylist("Zeus");

      bool result = stylist1.HasSamePropertiesAs(stylist2);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void HasSamePropertiesAs_BothDontHaveSameProperties_False()
    {
      Stylist stylist1 = new Stylist("Zeus");
      Stylist stylist2 = new Stylist("Hera");

      bool result = stylist1.HasSamePropertiesAs(stylist2);

      Assert.AreEqual(false, result);
    }
    [TestMethod]
    public void Save_SavesStylistToDatabase_DatabaseSaved()
    {
      Stylist localStylist = new Stylist("Zeus");
      localStylist.Save();
      Stylist databaseStylist = Stylist.GetAll()[0];

      bool result = localStylist.HasSamePropertiesAs(databaseStylist);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void Save_SavesMultipleStylistsToDatabase_StylistsSaved()
    {
      Stylist localStylist1 = new Stylist("Ronnie");
      localStylist1.Save();
      Stylist localStylist2 = new Stylist("Beth");
      localStylist2.Save();
      Stylist databaseStylist1 = Stylist.GetAll()[0];
      Stylist databaseStylist2 = Stylist.GetAll()[1];

      bool result =
        localStylist1.HasSamePropertiesAs(databaseStylist1) &&
        localStylist2.HasSamePropertiesAs(databaseStylist2);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void FindById_GetsStylistFromDatabase_StylistFound()
    {
      Stylist localStylist = new Stylist("Ronnie");
      localStylist.Save();
      Stylist databaseStylist = Stylist.FindById(localStylist.Id);

      bool result = localStylist.HasSamePropertiesAs(databaseStylist);

      Assert.AreEqual(true, result);
    }
  }
}

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=james_osborn_test;";
    }
    public void Dispose()
    {
      Client.ClearAll();
    }

    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      List<Client> clients = Client.GetAll();
      int result = clients.Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void HasSamePropertiesAs_BothHaveSameProperties_True()
    {
      Client client1 = new Client("Gus");
      Client client2 = new Client("Gus");

      bool result = client1.HasSamePropertiesAs(client2);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void HasSamePropertiesAs_BothDontHaveSameProperties_False()
    {
      Client client1 = new Client("Gus");
      Client client2 = new Client("Suzy");

      bool result = client1.HasSamePropertiesAs(client2);

      Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Save_SaveClient_ClientSaved()
    {
      Client testClient = new Client("Tod");
      testClient.Save();

      List<Client> clients = Client.GetAll();
      int result = clients.Count;
      Console.WriteLine(result);

      Assert.AreEqual(true,Client.GetAll().Count==1);
    }

    [TestMethod]
    public void Find_GetsSpecificClientFromDatabase_Client()
    {
      Client localClient = new Client("Ralphie");
      localClient.Save();
      Client databaseClient = Client.Find(localClient.Id);

      bool result = localClient.HasSamePropertiesAs(databaseClient);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Update_UpdateClientInDatabase_ClientWithNewInfo()
    {
      Client initialClient = new Client("Tony", 0);
      initialClient.Save();
      Client newClient = new Client("Ronnie", 0, initialClient.Id);
      initialClient.Update(newClient);
      Client updatedClient = Client.Find(initialClient.Id);

      bool result = updatedClient.HasSamePropertiesAs(newClient);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void DeleteClient_RemovesEntryFromDatabase_EntryRemoved()
    {
      Client client1 = new Client("Chuckie");
      client1.Save();
      Client client2 = new Client("Jason");
      client2.Save();
      Client client3 = new Client("Freddie");
      client3.Save();
      Client.Delete(client2.Id);
      List<Client> remainingClients = Client.GetAll();

      bool result = (
        client1.HasSamePropertiesAs(remainingClients[0]) &&
        client3.HasSamePropertiesAs(remainingClients[1])
      );

      Assert.AreEqual(true, result);
    }
  }
}

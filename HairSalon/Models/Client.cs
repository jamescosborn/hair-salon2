using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    public int Id {get; private set;}
    public string Name {get; private set;}
    public int StylistId {get; private set;}

    public Client(string name, int stylistId = 0, int id = 0)
    {
      Name = name;
      StylistId = stylistId;
      Id = id;
    }

    // See all Client's belonging to a Stylist
    public static List<Client> GetAll()
    {
      List<Client> output = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(name, stylistId, id);
        output.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @ClientId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@ClientId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int clientId = 0;
      string clientName = "";
      int stylistId = 0;

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        stylistId = rdr.GetInt32(2);
      }
      Client output = new Client(clientName, stylistId, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    // Deletes a Client if they no longer come to the salon.
    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @ClientId;";

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@ClientId";
      clientId.Value = id;
      cmd.Parameters.Add(clientId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // Adds new client to a specific Stylist. Cannot add a Client if no Stylist Id is entered.
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@Name, @stylist_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this.StylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      this.Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // Updates a Client's Name
    public void Update(string name, int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @clientId;";

        MySqlParameter newName = new MySqlParameter();
        newName.ParameterName = "@newName";
        newName.Value = name;
        cmd.Parameters.Add(newName);

        MySqlParameter clientId = new MySqlParameter();
        clientId.ParameterName = "@clientId";
        clientId.Value = id;
        cmd.Parameters.Add(clientId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

    public bool HasSamePropertiesAs(Client other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name &&
        this.StylistId == other.StylistId);
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.Id == newClient.Id);
        bool nameEquality = (this.Name == newClient.Name);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

  }
}

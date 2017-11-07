using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int Id {get; private set;}
    public string Name {get; private set;}
    // public int Age {get; private set;}
    // public string Music {get; private set;}

    public Stylist(string name, int id = 0)
    {
      Name = name;
      // Age = age;
      // Music = music;
      Id = id;
    }

    // Shows a list of all Stylists
    public static List<Stylist> GetAll()
    {
      List<Stylist> output = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        // int age = rdr.GetInt32(2);
        // string music = rdr.GetString(3);
        Stylist newStylist = new Stylist(name, id);
        output.Add(newStylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public List<Client> GetClients()
    {
      List<Client> output = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @StylistId;";

      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.Id;
      cmd.Parameters.Add(stylistIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylistId = rdr.GetInt32(3);
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
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      //  Client.ClearAll();

    }
    public bool HasSamePropertiesAs(Stylist other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name);
    }

    // Adds a new Stylist to the Salon
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name) VALUE (@Name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      this.Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // Selects a Stylist to view their info
    public static Stylist FindById(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @StylistId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@StylistId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int stylistId = 0;
      string stylistName = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }
      Stylist output = new Stylist(stylistName, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.Id == newStylist.Id);
        bool nameEquality = (this.Name == newStylist.Name);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }
  }
}

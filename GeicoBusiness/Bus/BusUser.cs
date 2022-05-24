using GeicoBusiness.Help;
using GeicoData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GeicoBusiness.Bus
{
  public class BusUser
  {
    private GeicoData.Models.GeicoEntities db = new GeicoEntities();
    public List<User> read(int userId)
    {
      List<User> result = new List<User>();
      try
      {
        //var goodcodes = db.IODetails.Where(d => d.Code == code).Select(d => d.UserCode.ToString()).ToArray();
        result = db.Users.Where(u => u.Id == (userId>=0? userId: u.Id)).ToList();
        //result = db.Users.Where(g => goodcodes.Contains(g.Code)).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return result;
    }
    public User Create(User User)
    {
      try
      {
        User entity = new User
        {
          typeno = User.typeno,
          USERNAME = User.USERNAME,
          pass = User.pass,
          FullName = User.FullName,
          Id= User.Id,
        };
        db.Users.Add(entity);
        db.SaveChanges();
        User.Id = entity.Id;
      }
      catch (DbEntityValidationException e)
      {
        foreach (var eve in e.EntityValidationErrors)
        {
          Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
              eve.Entry.Entity.GetType().Name, eve.Entry.State);
          foreach (var ve in eve.ValidationErrors)
          {
            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                ve.PropertyName, ve.ErrorMessage);
          }
        }
        throw;
      }
      catch (Exception ex)
      {

        throw ex;
      }
      return User;
    }
    public User Update(User User)
    {
      try
      {
        User entity = new User
        {
          typeno = User.typeno,
          USERNAME = User.USERNAME,
          pass = User.pass,
          FullName = User.FullName,
          Id= User.Id,
        };
        db.Users.Attach(entity);
        db.Entry(entity).State = EntityState.Modified;
        db.SaveChanges();
      }
      catch (DbEntityValidationException e)
      {
        foreach (var eve in e.EntityValidationErrors)
        {
          Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
              eve.Entry.Entity.GetType().Name, eve.Entry.State);
          foreach (var ve in eve.ValidationErrors)
          {
            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                ve.PropertyName, ve.ErrorMessage);
          }
        }
        throw;
      }
      catch (Exception ex)
      {

        throw ex;
      }
      return User;
    }
    public User Destroy(User User)
    {
      try
      {
        User entity = new User
        {
          typeno = User.typeno,
          USERNAME = User.USERNAME,
          pass = User.pass,
          FullName = User.FullName,
          Id = User.Id,
        };
        db.Users.Attach(entity);
        db.Users.Remove(entity);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return User;
    }
    
  }
}
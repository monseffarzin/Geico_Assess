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
using System.Web.Mvc;

namespace GeicoBusiness.Bus
{
  public class BusTask
  {
    private GeicoEntities db = new GeicoEntities();
    public List<Task> read(int Id)
    {
      List<Task> result = new List<Task>();
      try
      {
        if (Id > 0)
        {
          result = db.Tasks.Where(d => d.Id == Id).ToList();
        }
        else
        {
          result = db.Tasks.ToList();
        }
        //result = db.Tasks.Where(g => Tasks.Contains(g.Id)).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return result;
    }
    public Task Create(Task Task)
    {
      try
      {
        Task entity = new Task
        {
          Id = Task.Id,
          Name = Task.Name,
          Description = Task.Description,
          Due = Task.Due,
          Start = Task.Start,
          End = Task.End,
          Priority = Task.Priority,
          Status = Task.Status,
        };
        db.Tasks.Add(entity);
        db.SaveChanges();
        Task.Id = entity.Id;
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
      return Task;
    }
    public Task Update(Task Task)
    {
      try
      {
        Task entity = new Task
        {
          Id = Task.Id,
          Name = Task.Name,
          Description = Task.Description,
          Due = Task.Due,
          Start = Task.Start,
          End = Task.End,
          Priority = Task.Priority,
          Status = Task.Status,
        };
        db.Tasks.Attach(entity);
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
      return Task;
    }
    public Task Destroy(Task Task)
    {
      try
      {
        Task entity = new Task
        {
          Id = Task.Id,
          Name = Task.Name,
          Description = Task.Description,
          Due = Task.Due,
          Start = Task.Start,
          End = Task.End,
          Priority = Task.Priority,
          Status = Task.Status,
        };
        db.Tasks.Attach(entity);
        db.Tasks.Remove(entity);
        db.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return Task;
    }
    public int checkPriorityTasks()
    {
      var result = 0;
      try
      {
          var priority = db.Types.Where(t => t.Name.ToLower() == "high" && t.TypeNo == 1 && t.Control == 1 ).FirstOrDefault();
          result = db.Tasks.Where(d => d.Priority == priority.Id).Count();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return result; 
    }

  }
}
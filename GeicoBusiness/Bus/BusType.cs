using GeicoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Type = GeicoData.Models.Type;

namespace GeicoBusiness.Bus
{
  public class BusType
  {
    private GeicoEntities db = new GeicoEntities();

    public List<Type> Read(int typeno)
    {
      List<Type> result = new List<Type>();
      try
      {
        IQueryable<Type> Types = db.Types;
        result = Types.Where(t => t.TypeNo == typeno).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (List<Type>)result;
    }
    public List<DDSelectListItem> getTypeList(int typeno)
    {
      List<DDSelectListItem> result = new List<DDSelectListItem>();
      try
      {
        result = db.Types.Where(t => t.TypeNo == typeno && t.Control != 0).OrderBy(t => t.Code)
          .Select(t => new DDSelectListItem { Id = t.Id, Name = t.Name }).ToList() ;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return result;
    }
    public List<DDSelectListItem> getUserList(int typeno)
    {
      List<DDSelectListItem> result = new List<DDSelectListItem>();
      try
      {
        result = db.Users.OrderBy(u => u.FullName)//.Where(u => u.typeno == (typeno>=0? typeno: u.typeno))
          .Select(u => new DDSelectListItem { Id = u.Id, Name = u.FullName}).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return result;
    }
    //public Type Create(Type type)
    //{
    //  try
    //  {
    //    Type entity = new Type
    //    {
    //    };
    //    db.Types.Add(entity);
    //    db.SaveChanges();
    //    type.Counter = entity.Counter;
    //  }
    //  catch (Exception ex)
    //  {
    //    throw ex;
    //  }
    //  return iOHeader;
    //}
    //public Type Update(Type iOHeader)
    //{
    //  try
    //  {
    //    Type entity = new Type
    //    {
    //      TransType = iOHeader.TransType,
    //      Owner = iOHeader.Owner,
    //      Fix = iOHeader.Fix,
    //      Code2 = iOHeader.Code2,
    //      Date1 = iOHeader.Date1,
    //      PageNo = iOHeader.PageNo,
    //      StockMan = iOHeader.StockMan,
    //      Name = iOHeader.Name,
    //      FromToType = iOHeader.FromToType,
    //      From1 = iOHeader.From1,
    //      To1 = iOHeader.To1,
    //      ExitNo = iOHeader.ExitNo,
    //      PermisionDate = iOHeader.PermisionDate,
    //      PermisionNo = iOHeader.PermisionNo,
    //      BarnamehNo = iOHeader.BarnamehNo,
    //      BarnamehDate = iOHeader.BarnamehDate,
    //      MachinNo = iOHeader.MachinNo,
    //      DriverName = iOHeader.DriverName,
    //      DriverCost = iOHeader.DriverCost,
    //      Note = iOHeader.Note,
    //    };
    //    db.Types.Add(entity);
    //    db.SaveChanges();
    //    Type.code = entity.code;
    //  }
    //  catch (Exception ex)
    //  {
    //    throw ex;
    //  }
    //  return Type;
    //}
    //public Type Destroy(Type iOHeader)
    //{
    //  try
    //  {
    //    var entity = new Type
    //    {
    //      code = iOHeader.code,
    //      TransType = iOHeader.TransType,
    //      Owner = iOHeader.Owner,
    //      Fix = iOHeader.Fix,
    //      Code2 = iOHeader.Code2,
    //      Date1 = iOHeader.Date1,
    //      PageNo = iOHeader.PageNo,
    //      StockMan = iOHeader.StockMan,
    //      Name = iOHeader.Name,
    //      FromToType = iOHeader.FromToType,
    //      From1 = iOHeader.From1,
    //      To1 = iOHeader.To1,
    //      ExitNo = iOHeader.ExitNo,
    //      PermisionDate = iOHeader.PermisionDate,
    //      PermisionNo = iOHeader.PermisionNo,
    //      BarnamehNo = iOHeader.BarnamehNo,
    //      BarnamehDate = iOHeader.BarnamehDate,
    //      MachinNo = iOHeader.MachinNo,
    //      DriverName = iOHeader.DriverName,
    //      DriverCost = iOHeader.DriverCost,
    //      Note = iOHeader.Note,
    //      id_export = iOHeader.id_export,
    //    };
    //    db.Types.Attach(entity);
    //    db.Types.Remove(entity);
    //    db.SaveChanges();
    //  }
    //  catch (Exception ex)
    //  {
    //    throw ex;
    //  }
    //  return Type;
    //}
  }
}
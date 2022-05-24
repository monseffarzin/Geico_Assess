using GeicoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Entitites;

namespace GeicoData.Models
{
	/// <summary>
	/// Data class pertaining to database table LogDB
	/// </summary>
	public class LogDBData : DataBase
	{
		/// <summary>
		/// Add a record to LogDB table
		/// </summary>
		/// <param name="logDB">logDB</param>
		/// <returns>boolean true/false</returns>
		public bool AddLogDB(LogDB logDB)
		{
			try
			{
				var objLogDB = new LogDB
				{
					Date_Time = logDB.Date_Time,
					UserName = logDB.UserName,
					Controller = logDB.Controller,
					Action = logDB.Action,
					Message = logDB.Message,
					Comment = logDB.Comment
				};
				Db.LogDBs.Add(objLogDB);
				Db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

	}
}

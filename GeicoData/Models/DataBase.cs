using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Entitites;
using GeicoData.Models;

namespace GeicoData.Models
{
	public abstract class DataBase
	{
		public DataBase()
		{
			Db = new GeicoEntities();
		}

		protected GeicoEntities Db { get; set; }

	}
}
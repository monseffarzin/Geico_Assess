using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeicoBusiness.Help
{
  public class Help
  {
    public SelectList ToSelectList(DataTable table, string valueField, string textField)
    {
      List<SelectListItem> list = new List<SelectListItem>();

      foreach (DataRow row in table.Rows)
      {
        list.Add(new SelectListItem()
        {
          Text = row[textField].ToString(),
          Value = row[valueField].ToString()
        });
      }

      return new SelectList(list, "Value", "Text");
    }
    public SelectList ToSelectList(Dictionary<string, int> mydic, int selectedIndex)
    {
      List<SelectListItem> list = new List<SelectListItem>();
      int i= 0;
      foreach (KeyValuePair<string, int> row in mydic)
      { 
        list.Add(new SelectListItem()
        {
          Text = row.Key.ToString(),
          Value = row.Value.ToString(),
          Selected = (selectedIndex == i? true: false)
        });
        i++;
      }
      return new SelectList(list, "Value", "Text", selectedIndex);
    }

  }
}
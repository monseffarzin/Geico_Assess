using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geico.Tool
{
  public static class print
  {
    public static List<string> PrinterList()
    {
      List<string> printers = new List<string>();
      foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
      {
        printers.Add(sPrinters);
      }
      return printers;
    }

  }
}
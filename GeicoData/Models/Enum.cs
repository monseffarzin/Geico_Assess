using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeicoData.Models
{
  public class Enum
  {
  }

  public enum StringQueryOperators
  {
    Contains = 1,
    Does_not_contain = 2,
    Is_equal_to = 3,
    Is_not_equal_to = 4,
    Start_with = 5,
    Ends_with = 6
  }

}
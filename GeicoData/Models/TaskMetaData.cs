using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
//using Attributes;
//using System.Collections.Generic;


namespace GeicoData.Models
{

  [MetadataType(typeof(TaskMetaData))]
  public partial class Task
  {
    //public static explicit operator List(IOHeader i)
    //{
    //    throw new NotImplementedException();
    //}
  }
  public class TaskMetaData
  {
    [Display(Name = "Id")]
    [Editable(false)]
    public string Id{ get; set; }
    [Required(ErrorMessage = "{0} is required.")]
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    [Required(ErrorMessage = "{0} is required.")]
    [UIHint("DropDownPriority")]
    [Display(Name = "Priority")]
    public Nullable<int> Priority { get; set; }
    [UIHint("DropDownStatus")]
    [Display(Name = "Status")]
    public Nullable<int> Status { get; set; }
    [UIHint("DateTimeDisabledBeforeToday")]
    [DataType(DataType.Date)]
    [Display(Name = "Due")]
    public Nullable<int> Due { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Start")]
    public Nullable<int> Start { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "End")]
    public Nullable<int> End{ get; set; }

  }
}
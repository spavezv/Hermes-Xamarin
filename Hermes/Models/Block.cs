using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hermes.Models
{
  public class Block
  {
      public int id { set; get; }
      public string date { set; get; }
      public string start { set; get; }
      public string finish { set; get; }
      public int price { set; get; }
      public string createdAt { set; get; }
      public string updatedAt { set; get; }
      public Clients clientId { set; get; }
      public Court courtId { set; get; }
    
  }
}
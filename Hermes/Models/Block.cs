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
    public string start { set; get; }
    public string finish { set; get; }
    public int price { set; get; }
    public int id { set; get; }
  }
}
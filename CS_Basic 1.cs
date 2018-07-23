using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
  class Program
  {
    static void Main(string[] args)
    {
      int x = 12 * 30;
      Console.WriteLine(x);
      string z = Console.ReadLine();
      int y = Int32.Parse(z);
      Console.WriteLine("Converting " + y + " feet to inches...");
      Console.WriteLine("Feet to inches: "+feet_to_inches(y));
    }
    static int feet_to_inches(int feet)
    {
      return feet * 12;
    }
  }
}

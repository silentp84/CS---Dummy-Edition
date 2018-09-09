using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
  class Program
  {
    static void Swap(ref string a, ref string b)
    {
      string temp = a; //all these assignments are by value and not reference
      a = b;
      b = temp;
    }
    static void _Main(string[] args)
    {
      string x = "Penn";
      string y = "Teller";
      Swap(ref x, ref y);
      Console.WriteLine(x);
      Console.WriteLine(y);
    }
  }
}

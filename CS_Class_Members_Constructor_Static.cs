using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
  public class Emos
  {
    public string Name;
    public static int Scars;

    public Emos(string n)
    {
      Name = n;
      Scars++;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Emos ruggle = new Emos("rlly?");
      Console.WriteLine(ruggle.Name + " " + Emos.Scars);
      Emos todd = new Emos("todd");
      Console.WriteLine(todd.Name + " " + Emos.Scars);
      Emos modd = new Emos("mooddd");
      Console.WriteLine(modd.Name + " " + Emos.Scars);
    }
  }
}

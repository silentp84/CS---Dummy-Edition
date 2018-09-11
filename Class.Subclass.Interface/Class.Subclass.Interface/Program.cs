using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Subclass.Interface
{
  public class Banker: Bank
  {
    public string Name;
  }

  class Program
  {
    static void Main(string[] args)
    {
      Banker andrew = new Banker();
      andrew.balance = 100;
      andrew.acctNumber = 20;
      andrew.userID = 2302106;
      andrew.interest = .20;
    }
  }
}

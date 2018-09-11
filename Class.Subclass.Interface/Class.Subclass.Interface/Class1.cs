using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Subclass.Interface
{
  public class Bank
  {
    public int balance { get; set; }
    public int acctNumber { get; set; }
    public int userID { get; set; }
    public int interest { get; set; }

    void print()
    {
      Console.WriteLine(userID + ": ")
      Console.WriteLine()
    }

    public int monthlyInterest()
    {
      return balance * interest + balance;
    }
  }
}

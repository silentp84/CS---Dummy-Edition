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
    public string userID { get; set; }
    public double interest { get; set; }

    public Bank(int bal, int anum, string user, double acrrue)
    {
      balance = bal;
      acctNumber = anum;
      userID = user;
      interest = acrrue;
    }

    public virtual void print()
    {
      Console.WriteLine(userID + ": ");
      Console.WriteLine("Account Number: " + acctNumber);
      Console.WriteLine("Balance: " + balance);
      Console.WriteLine("Monthly Interest: " + interest);
      Console.WriteLine("Balance after a monthly interest: " + monthlyInterest() + "\n");
    }

    public double monthlyInterest()
    {
      return balance * interest + balance;
    }
  }

  class Banker : Bank
  {
    string call;
    public Banker(int bal, int anum, string user, double acrrue, string name) : base(bal, anum, user, acrrue)
    {
      call = name;
    }

    public override void print()
    {
      Console.WriteLine("I overrode the Bank print");
      Console.WriteLine("I'm in Banker subclass \n" + call);
      base.print();
    }
  }

  class Savings: Bank
  {
    private float saving;
    public Savings(int bal, int anum, string user, double acrrue, float sbalance) : base(bal, anum, user, acrrue)
    {
      saving = sbalance;
    }

    public override void print()
    {
      Console.WriteLine("I overrode the Bank print");
      Console.WriteLine("I'm in Savings subclass \n" + saving);
      base.print();
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Bank andrew = new Bank(100,20,"2302106", .2);
      andrew.print();
      Banker paul = new Banker(200, 21, "2302107", .3, "paul");
      paul.print();
      Savings jenni = new Savings(300, 22, "2302108", .4, 55.85f);
      jenni.print();
    }
  }
}

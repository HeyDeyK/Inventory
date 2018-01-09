    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            Person personGold = new Person();
            Bill bill = new Bill();
            Bill bill2 = new Bill();
            NotMemberPayingStrategy nula = new NotMemberPayingStrategy();
            GoldMemberPayingStrategy borec = new GoldMemberPayingStrategy();

            bill.AccountBalance = 1000;
            bill2.AccountBalance = 1000;

            person.AccountBill = bill;
            person.PayingStrategy = nula;

            personGold.AccountBill = bill2;
            personGold.PayingStrategy = borec;

            personGold.BuyItem(200);
            person.BuyItem(200);

            Console.ReadLine();
        }
    }
}

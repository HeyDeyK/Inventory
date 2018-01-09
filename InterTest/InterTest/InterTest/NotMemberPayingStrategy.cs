using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTest
{
    class NotMemberPayingStrategy : IPayingStrategy
    {
        public void Pay(Bill bill, int price)
        {
            Console.WriteLine("BALANCE před: " + bill.AccountBalance);
            bill.AccountBalance = bill.AccountBalance - price;
            Console.WriteLine("BALANCE před: " + bill.AccountBalance);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTest
{
    class GoldMemberPayingStrategy : IPayingStrategy
    {
        public void Pay(Bill bill, int price)
        {
            Console.WriteLine("BALANCE Gold zákazníka před: " + bill.AccountBalance);
            bill.AccountBalance = bill.AccountBalance - (price/2);
            Console.WriteLine("BALANCE Gold zákazníka před: " + bill.AccountBalance);

        }
    }
}

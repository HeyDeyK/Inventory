using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterTest
{
    class Person
    {
        string Id { get; set; }
        string Name { get; set; }

        public Bill AccountBill;

        public IPayingStrategy PayingStrategy;

        public void BuyItem(int price)
        {
            PayingStrategy.Pay(AccountBill, price);
        }
    }
}

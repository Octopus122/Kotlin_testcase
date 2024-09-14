using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotlin_testcase
{
    internal interface ICurrency
    {
        void ChangeExchangeRates();
        void IncreaseCapacity(float money);
        void DecreaseCapacity(float money);
    }
}

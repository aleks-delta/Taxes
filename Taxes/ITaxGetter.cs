using System;

namespace Taxes
{
    interface ITaxGetter
    {
        double GetTax(DateTime date);
    }
}

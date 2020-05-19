using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class WagesConv
    {
        static public string WageToStr(string wage)
        {
            if (wage == ">20000") return "high";
            if (wage == "10000 - 20000") return "normal";
            if (wage == "<10000") return "low";
            if (wage == null) return null;
            if (int.Parse(wage) > 20000) return "high";
            if ((int.Parse(wage) <= 20000) && (int.Parse(wage) >= 10000)) return "normal";
            if ((int.Parse(wage) < 10000) && (int.Parse(wage) > 0)) return "low";
            if (int.Parse(wage) == 0) return "null";
            return "Error";
        }
    }
}

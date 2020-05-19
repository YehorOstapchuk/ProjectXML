using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class ExperienseConv
    {
        static public string ExperienseToStr(string experience)
        {
            if (experience == ">40") return "high";
            if (experience == "20-40") return "normal";
            if (experience == "<20") return "low";
            if (experience == null) return null;
            if (int.Parse(experience) > 40) return "high";
            if ((int.Parse(experience) <= 40) && (int.Parse(experience) >= 20)) return "normal";
            if ((int.Parse(experience) < 20) && (int.Parse(experience) > 0)) return "low";
            if (int.Parse(experience) == 0) return "null";
            return "Error";
        }
    }
}

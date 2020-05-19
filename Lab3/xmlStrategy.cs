using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class xmlStrategy
    {
        private int flag;
        private Teacher teacher;
        public xmlStrategy(int flag, Teacher teacher)
        {
            this.flag = flag;
            this.teacher = teacher;
        }

        public List<Teacher> Search()
        {
            SearchS search;
            switch (flag)
            {
                case 0: { search = new DOM(); break; }
                case 1: { search = new SAX(); break; }
                case 2: { search = new LINQ(); break; }
                default: { search = null; break; }
            }
            return search.Search(teacher);
        }

    }
}

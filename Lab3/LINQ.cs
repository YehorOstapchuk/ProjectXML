using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;

namespace Lab3
{
    class LINQ : SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {

            List<Teacher> result = new List<Teacher>();
            var document = XDocument.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
            var result2 = from obj in document.Descendants("Teacher")
                          where (
                         ((obj.Attribute("Name").Value == teacher.Name) || (teacher.Name == null)) &&
                         ((obj.Attribute("Faculty").Value == teacher.Faculty) || (teacher.Faculty == null)) &&
                         ((obj.Attribute("Department").Value == teacher.Department) || (teacher.Department == null)) &&
                         ((obj.Attribute("Position").Value == teacher.Position) || (teacher.Position == null)) &&
                         ((WagesConv.WageToStr(obj.Attribute("Pension").Value) == WagesConv.WageToStr(teacher.Pension)) || (teacher.Pension == null)) &&
                         ((ExperienseConv.ExperienseToStr(obj.Attribute("Experiense").Value) == ExperienseConv.ExperienseToStr(teacher.Experiense)) || (teacher.Experiense == null)))
                          select new
                          {
                              Name = obj.Attribute("Name").Value,
                              Faculty = obj.Attribute("Faculty").Value,
                              Department = obj.Attribute("Department").Value,
                              Position = obj.Attribute("Position").Value,
                              Pension = obj.Attribute("Pension").Value,
                              Experiense = obj.Attribute("Experiense").Value

                          };
            foreach (var n in result2)
            {
                Teacher temp = new Teacher();
                temp.Name = n.Name;
                temp.Faculty = n.Faculty;
                temp.Department = n.Department;
                temp.Position = n.Position;
                temp.Pension = n.Pension;
                temp.Experiense = n.Experiense;
                result.Add(temp);
            }
            return result;

        }
    }
}

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
    class SAX : SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {

            List<Teacher> result = new List<Teacher>();
            var xmlReader = new XmlTextReader(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {


                    string Name = null;
                    string Faculty = null;
                    string Department = null;
                    string Position = null;
                    string Pension = null;
                    string Experiense = null;

                    bool flag = false;

                    if (xmlReader.AttributeCount != 6) continue;
                    Console.WriteLine(xmlReader.AttributeCount);
                    for (int i = 0; i < xmlReader.AttributeCount; i++)
                    {
                        if (i == 0) xmlReader.MoveToFirstAttribute();
                        if (i != 0) xmlReader.MoveToNextAttribute();
                        string temp = xmlReader.Value;
                        if (xmlReader.Name.Equals("Name") && (teacher.Name != null)) { if (temp != teacher.Name) { flag = true; break; } }
                        if (xmlReader.Name.Equals("Faculty") && (teacher.Faculty != null)) { if (temp != teacher.Faculty) { flag = true; break; } }
                        if (xmlReader.Name.Equals("Department") && (teacher.Department != null)) { if (temp != teacher.Department) { flag = true; break; } }
                        if (xmlReader.Name.Equals("Position") && (teacher.Position != null)) { if (temp != teacher.Position) { flag = true; break; } }
                        if (xmlReader.Name.Equals("Pension") && (teacher.Pension != null)) { if (WagesConv.WageToStr(temp) != WagesConv.WageToStr(teacher.Pension)) { flag = true; break; } }
                        if (xmlReader.Name.Equals("Experiense") && (teacher.Experiense != null)) { if (ExperienseConv.ExperienseToStr(temp) != ExperienseConv.ExperienseToStr(teacher.Experiense)) { flag = true; break; } }

                        if (xmlReader.Name.Equals("Name")) { Name = temp; }
                        if (xmlReader.Name.Equals("Faculty")) { Faculty = temp; }
                        if (xmlReader.Name.Equals("Department")) { Department = temp; }
                        if (xmlReader.Name.Equals("Position")) { Position = temp; }
                        if (xmlReader.Name.Equals("Pension")) { Pension = temp; }
                        if (xmlReader.Name.Equals("Experiense")) { Experiense = temp; }
                    }

                    if (!flag)
                    {
                        Teacher temp = new Teacher();
                        temp.Name = Name;
                        temp.Faculty = Faculty;
                        temp.Department = Department;
                        temp.Position = Position;
                        temp.Pension = Pension;
                        temp.Experiense = Experiense;
                        result.Add(temp);
                    }


                }
            }
            xmlReader.Close();
            return result;
        }
    }
}

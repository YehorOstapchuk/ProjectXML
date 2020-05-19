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
    class DOM : SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {
            List<Teacher> result = new List<Teacher>();
            //string path = "";
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
            XmlNode base_el = document.DocumentElement;
            string s = "//Teacher[";
            int count = 0;
            if (teacher.Name != null)
            {
                 s += "@Name=\"" + teacher.Name + "\"";
                count++;
            }

            if (teacher.Faculty != null)
            {
                if (count == 0)
                {
                    s += "@Faculty=\"" + teacher.Faculty + "\"";
                    count++;
                }
                else
                {
                    s += " and @Faculty=\"" + teacher.Faculty + "\"";
                    count++;
                }

            }

            if (teacher.Department != null)
            {
                if (count == 0)
                {
                    s += "@Department=\"" + teacher.Department + "\"";
                    count++;
                }
                else
                {
                    s += " and @Department=\"" + teacher.Department + "\"";
                }

            }

            if (teacher.Position != null)
            {
                if (count == 0)
                {
                    s += "@Position=\"" + teacher.Position + "\"";
                    count++;
                }
                else
                {
                    s += " and @Position=\"" + teacher.Position + "\"";
                }

            }

            s += "]";

            XmlNodeList result2 = document.SelectNodes(s);

            if (result2.Count!=0)
            {
                foreach (XmlNode node in result2)
                {


                    bool flag = false;
                    for (int i = 0; i < node.Attributes.Count; i++)
                    {
                        if (node.Attributes[i].Name.Equals("Pension") && (teacher.Pension != null)) { if (WagesConv.WageToStr(node.Attributes[i].Value) != WagesConv.WageToStr(teacher.Pension)) { flag = true; break; } }
                        if (node.Attributes[i].Name.Equals("Experiense") && (teacher.Experiense != null)) { if (ExperienseConv.ExperienseToStr(node.Attributes[i].Value) != ExperienseConv.ExperienseToStr(teacher.Experiense)) { flag = true; break; } }
                    }


                    if (!flag)
                    {
                        Teacher temp = new Teacher();
                        for (int i = 0; i < node.Attributes.Count; i++)
                        {
                            if (node.Attributes[i].Name.Equals("Name")) { temp.Name = node.Attributes[i].Value; }
                            if (node.Attributes[i].Name.Equals("Faculty")) { temp.Faculty = node.Attributes[i].Value; }
                            if (node.Attributes[i].Name.Equals("Department")) { temp.Department = node.Attributes[i].Value; }
                            if (node.Attributes[i].Name.Equals("Position")) { temp.Position = node.Attributes[i].Value; }
                            if (node.Attributes[i].Name.Equals("Pension")) { temp.Pension = node.Attributes[i].Value; }
                            if (node.Attributes[i].Name.Equals("Experiense")) { temp.Experiense = node.Attributes[i].Value; }
                        }
                        result.Add(temp);
                    }
                }
            }

            return result;


            /*   foreach (XmlNode node in base_el.ChildNodes)
               {



                   bool flag = false;
                   for (int i = 0; i < node.Attributes.Count; i++)
                   {
                       if (node.Attributes[i].Name.Equals("Name") && (teacher.Name != null)) { if (node.Attributes[i].Value != teacher.Name) { flag = true; break; } }
                       if (node.Attributes[i].Name.Equals("Faculty") && (teacher.Faculty != null)) { if (node.Attributes[i].Value != teacher.Faculty) { flag = true; break; } }
                       if (node.Attributes[i].Name.Equals("Department") && (teacher.Department != null)) { if (node.Attributes[i].Value != teacher.Department) { flag = true; break; } }
                       if (node.Attributes[i].Name.Equals("Position") && (teacher.Position != null)) { if (node.Attributes[i].Value != teacher.Position) { flag = true; break; } }
                       if (node.Attributes[i].Name.Equals("Pension") && (teacher.Pension != null)) { if (WagesConv.WageToStr(node.Attributes[i].Value) != WagesConv.WageToStr(teacher.Pension)) { flag = true; break; } }
                       if (node.Attributes[i].Name.Equals("Experiense") && (teacher.Experiense != null)) { if (ExperienseConv.ExperienseToStr(node.Attributes[i].Value) != ExperienseConv.ExperienseToStr(teacher.Experiense)) { flag = true; break; } }
                   }

                   if (!flag)
                   {
                       Teacher temp = new Teacher();
                       for (int i = 0; i < node.Attributes.Count; i++)
                       {
                           if (node.Attributes[i].Name.Equals("Name")) { temp.Name = node.Attributes[i].Value; }
                           if (node.Attributes[i].Name.Equals("Faculty")) { temp.Faculty = node.Attributes[i].Value; }
                           if (node.Attributes[i].Name.Equals("Department")) { temp.Department = node.Attributes[i].Value; }
                           if (node.Attributes[i].Name.Equals("Position")) { temp.Position = node.Attributes[i].Value; }
                           if (node.Attributes[i].Name.Equals("Pension")) { temp.Pension = node.Attributes[i].Value; }
                           if (node.Attributes[i].Name.Equals("Experiense")) { temp.Experiense = node.Attributes[i].Value; }
                       }
                       result.Add(temp);
                   }
               }
               return result;*/

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;

namespace Lab3
{

    public partial class Form1 : Form
    {
        string path = "";
        public Form1()
        {
            InitializeComponent();
            GetAllTeachrs();
        }

        private void comboBox5_DropDown(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchBut();
        }

        public void SearchBut()
        {
            richTextBox1.Text = "";
            Teacher temp = new Teacher();

            if (checkBox1.Checked&&(comboBox1.SelectedItem!=null)) temp.Name = comboBox1.SelectedItem.ToString();
            if (checkBox2.Checked && (comboBox2.SelectedItem != null)) temp.Faculty = comboBox2.SelectedItem.ToString();
            if (checkBox3.Checked && (comboBox3.SelectedItem != null)) temp.Department = comboBox3.SelectedItem.ToString();
            if (checkBox4.Checked && (comboBox4.SelectedItem != null)) temp.Position = comboBox4.SelectedItem.ToString();
            if (checkBox5.Checked && (comboBox5.SelectedItem != null)) temp.Pension = comboBox5.SelectedItem.ToString();
            if (checkBox6.Checked && (comboBox6.SelectedItem != null)) temp.Experiense = comboBox6.SelectedItem.ToString();

            if (!((temp.Name == null) && (temp.Faculty == null) && (temp.Department == null) && (temp.Position == null) && (temp.Pension == null) && (temp.Experiense == null) && (!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked)))
            {
                int flag= 0;
                if (radioButton1.Checked) flag = 0;
                if (radioButton2.Checked) flag = 1;
                if (radioButton3.Checked) flag = 2;
                xmlStrategy dom = new xmlStrategy(flag, temp);
               // LINQ dom = new LINQ();
                //richTextBox1.Text += WagesConv.WageToStr(temp.Pension);
                var result = dom.Search();

                foreach (Teacher n in result)
                {
                    richTextBox1.Text += "" + n.Name + "\n";
                    richTextBox1.Text += "" + n.Faculty + "\n";
                    richTextBox1.Text += "" + n.Department + "\n";
                    richTextBox1.Text += "" + n.Position + "\n";
                    richTextBox1.Text += "" + n.Pension + "\n";
                    richTextBox1.Text += "" + n.Experiense + "\n";
                    richTextBox1.Text += "------------------------------------\n";
                }
            }
        }


        public void transform ()
        {
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XSL.xsl");
            string fXML = @"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml";
            string fHTML = @"C:\Users\Егор\Desktop\C#\Lab3\HTMLFile1.html";
            xct.Transform(fXML, fHTML);
            MessageBox.Show("Transforming");
        }

        public void GetAllTeachrs()
        {
            /*   XmlDocument document = new XmlDocument();
               document.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
               XmlElement xml_root = document.DocumentElement;
               XmlNodeList children_nodes = xml_root.SelectNodes("Teacher");
               richTextBox1.Text = children_nodes.Count.ToString();
                 for (int i = 0; i < children_nodes.Count; ++i)
                {
                    AddItems(children_nodes.Item(i));
                 } */

            // var doc = new XDocument().Load();


            // AddItems(children_nodes.Item(0));
            // AddItems(children_nodes.Item(1));
            var document = XDocument.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
            var result2 = from obj in document.Descendants("Teacher")  select new
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
                if (!comboBox1.Items.Contains(n.Name)) comboBox1.Items.Add(n.Name);
                if (!comboBox2.Items.Contains(n.Faculty)) comboBox2.Items.Add(n.Faculty);
                if (!comboBox3.Items.Contains(n.Department)) comboBox3.Items.Add(n.Department);
                if (!comboBox4.Items.Contains(n.Position)) comboBox4.Items.Add(n.Position);
            }

            string high_pension = ">20000";
            string normal_pension = "10000 - 20000";
            string low_pension = "<10000";

            string high_experiense = ">40";
            string normal_experiense = "20-40";
            string low_experience = "<20";

            comboBox5.Items.Add(high_pension);
            comboBox5.Items.Add(normal_pension);
            comboBox5.Items.Add(low_pension);

            comboBox6.Items.Add(high_experiense);
            comboBox6.Items.Add(normal_experiense);
            comboBox6.Items.Add(low_experience);

        }

        private void AddItems(XmlNode n)
        {
            if (!comboBox1.Items.Contains(n.SelectSingleNode("//@Name").Value)) comboBox1.Items.Add(n.SelectSingleNode("//@Name").Value);
            if (!comboBox2.Items.Contains(n.SelectSingleNode("//@Faculty").Value)) comboBox2.Items.Add(n.SelectSingleNode("//@Faculty").Value);
            if (!comboBox3.Items.Contains(n.SelectSingleNode("//@Department").Value)) comboBox3.Items.Add(n.SelectSingleNode("//@Department").Value);
            if (!comboBox4.Items.Contains(n.SelectSingleNode("//@Position").Value)) comboBox4.Items.Add(n.SelectSingleNode("//@Position").Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            transform();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


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


    static class ExperienseConv
    {
        static public string ExperienseToStr(string experience)
        {
            if (experience == ">20000") return "high";
            if (experience == "10000 - 20000") return "normal";
            if (experience == "<10000") return "low";
            if (experience == null) return null;
            if (int.Parse(experience) > 20000) return "high";
            if ((int.Parse(experience) <= 20000) && (int.Parse(experience) >= 10000)) return "normal";
            if ((int.Parse(experience) < 10000) && (int.Parse(experience) > 0)) return "low";
            if (int.Parse(experience) == 0) return "null";
            return "Error";
        }
    }


    class Teacher
    {
        public string Name = null;
        public string Faculty = null;
        public string Department = null;
        public string Position = null;
        public string Pension = null;
        public string Experiense = null;
    }

    class xmlStrategy
    {
        private int flag;
        private Teacher teacher;
        public xmlStrategy (int flag, Teacher teacher)
        {
            this.flag = flag;
            this.teacher = teacher;
        }

        public List<Teacher> Search()
        {
            SearchS search;
            switch (flag) {
                case 0: { search = new DOM(); break; }
                case 1: { search = new SAX(); break; }
                case 2: { search = new LINQ(); break; }
                default: { search = null; break; }
            }
            return search.Search(teacher);
        }

    }


    abstract class SearchS
    {
        abstract public List<Teacher> Search(Teacher teacher);
    }


    class DOM: SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {
            List<Teacher> result = new List<Teacher>();
            //string path = "";
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Егор\Desktop\C#\Lab3\XMLFile1.xml");
            XmlNode base_el = document.DocumentElement;
            foreach (XmlNode node in base_el.ChildNodes)
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
            return result;

        }

    }

    class SAX : SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {
            string path = "";
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

    class LINQ : SearchS
    {
        public override List<Teacher> Search(Teacher teacher)
        {
            string path = "";
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

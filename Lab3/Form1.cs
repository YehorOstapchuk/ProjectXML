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

            if (!((temp.Name == null) && (temp.Faculty == null) && (temp.Department == null) && (temp.Position == null) && (temp.Pension == null) && (temp.Experiense == null)))
            {
                if ((!radioButton1.Checked) && (!radioButton2.Checked) && (!radioButton3.Checked)) { MessageBox.Show("Choose the way of processing."); return; }
                int flag = 4;
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
            else MessageBox.Show("Chose filters!");
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
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to transform?", "Confirm that you really want to transform", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                transform();
            }
           
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to exit?", "Confirm that you really want to exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {

            }
            else e.Cancel = true;
        }
    }

}

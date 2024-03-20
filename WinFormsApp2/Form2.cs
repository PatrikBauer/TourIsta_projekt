using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form2 : Form
    {
        Model model;
        string user;
        List<Destination> cityList = new List<Destination>();
        List<Comfort> comfortList = new List<Comfort>();
        public Form2(Model mod, string name)
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("utaztatas.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            model = mod;
            user = name;
            cityList = model.LoadCities();
            comfortList = model.LoadComforts();
            label1.Text = "Úti cél";
            label1.BackColor = Color.FromArgb(192, 192, 255);
            label2.Text = "Komfort";
            label2.BackColor = Color.FromArgb(192, 192, 255);
            label3.Text = "Felnőttek száma";
            label3.BackColor = Color.FromArgb(192, 192, 255);
            label4.Text = "Gyerekek száma";
            label4.BackColor = Color.FromArgb(192, 192, 255);
            label5.Text = "Nyugdíjasok száma";
            label5.BackColor = Color.FromArgb(192, 192, 255);
            checkBox1.Text = "Menettérti";
            checkBox1.BackColor = Color.FromArgb(192, 192, 255);
            checkBox2.Text = "Háziállat";
            checkBox2.BackColor = Color.FromArgb(192, 192, 255);
            checkBox3.Text = "Étkeztetés";
            checkBox3.BackColor = Color.FromArgb(192, 192, 255);
            checkBox4.Text = "Ágy";
            checkBox4.BackColor = Color.FromArgb(192, 192, 255);
            button1.Text = "Extrákat nulláz";
            label6.Text = "Összeg:";
            label6.BackColor = Color.FromArgb(192, 192, 255);
            label7.Text = "Ft";
            label7.BackColor = Color.FromArgb(192, 192, 255);
            button2.Text = "Utazik";
            button3.Text = "Mégsem";
            textBox1.Enabled = false;
            label8.Text = "Adatok";
            label8.BackColor = Color.FromArgb(192, 192, 255);
            label9.Text = "Extrák";
            label9.BackColor = Color.FromArgb(192, 192, 255);
            pictureBox1.BackColor = Color.FromArgb(192, 192, 255);
            pictureBox2.BackColor = Color.FromArgb(192, 192, 255);

            foreach (Destination item in cityList)
            {
                comboBox1.Items.Add(item.City);
            }
            foreach (Comfort item in comfortList)
            {
                comboBox2.Items.Add(item.Level);
            }
            button1.Click += (s, e) =>
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            };
            button2.Click += (s, e) =>
            {
                List<string> final = new List<string>();
                if (comboBox1.SelectedIndex != 0 || comboBox2.SelectedIndex != 0 || (numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && numericUpDown3.Value != 0))
                {

                    final.Add(comboBox1.Text);
                    final.Add(comboBox2.Text);
                    final.Add(numericUpDown1.Value.ToString());
                    final.Add(numericUpDown2.Value.ToString());
                    final.Add(numericUpDown3.Value.ToString());
                    MessageBox.Show(final.ToString());
                }
                else
                {
                    MessageBox.Show("Kérlek töltsd ki az összes szükséges mezőt!");
                }
            };
            button3.Click += (s, e) =>
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            };
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            comboBox2.SelectedIndexChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            numericUpDown1.ValueChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            numericUpDown2.ValueChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            numericUpDown3.ValueChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            checkBox1.CheckedChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            checkBox2.CheckedChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            checkBox3.CheckedChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
            checkBox4.CheckedChanged += (s, e) =>
            {
                double priceAdult = 0;
                double priceChild = 0;
                double priceOld = 0;
                int peopleCount = Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value) + Convert.ToInt32(numericUpDown3.Value);
                int price = cityList.Where(x => x.City == comboBox1.SelectedItem.ToString()).Select(x => x.Price).First();
                if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0 && (numericUpDown1.Value > 0 || numericUpDown2.Value > 0 || numericUpDown3.Value > 0))
                {
                    if (numericUpDown1.Value > 0)
                    {
                        priceAdult = price * Convert.ToInt32(numericUpDown1.Value);
                    }
                    if (numericUpDown2.Value > 0)
                    {
                        priceChild = (price * Convert.ToInt32(numericUpDown2.Value)) * 0.5;
                    }
                    if (numericUpDown3.Value > 0)
                    {
                        priceOld = (price * Convert.ToInt32(numericUpDown3.Value)) * 0.25;
                    }
                    double priceFinal = priceAdult + priceChild + priceOld;
                    if (checkBox1.Checked)
                    {
                        priceFinal = priceFinal * 2;
                    }
                    if (checkBox2.Checked)
                    {
                        priceFinal = priceFinal + 2500;
                    }
                    if (checkBox3.Checked)
                    {
                        priceFinal = priceFinal + (2000 * peopleCount);
                    }
                    if (checkBox4.Checked)
                    {
                        priceFinal = priceFinal + (1500 * peopleCount);
                    }
                    double comfort = comfortList.Where(x => x.Level == comboBox2.SelectedItem.ToString()).Select(x => x.Multiplier).First();
                    priceFinal = priceFinal + comfort;
                    textBox1.Text = Math.Round(priceFinal, 0).ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            };
        }
        private IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            button1 = new Button();
            textBox1 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            button2 = new Button();
            button3 = new Button();
            label8 = new Label();
            label9 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((ISupportInitialize)numericUpDown1).BeginInit();
            ((ISupportInitialize)numericUpDown2).BeginInit();
            ((ISupportInitialize)numericUpDown3).BeginInit();
            ((ISupportInitialize)pictureBox1).BeginInit();
            ((ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 86);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 157);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 1;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(67, 220);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 2;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(67, 290);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 3;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(67, 363);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 4;
            label5.Text = "label5";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(176, 218);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 5;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(176, 288);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 6;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(176, 361);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(120, 23);
            numericUpDown3.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(175, 81);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 8;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(175, 154);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 9;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(464, 85);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(83, 19);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(639, 86);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(83, 19);
            checkBox2.TabIndex = 11;
            checkBox2.Text = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(464, 153);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(83, 19);
            checkBox3.TabIndex = 12;
            checkBox3.Text = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(639, 153);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(83, 19);
            checkBox4.TabIndex = 13;
            checkBox4.Text = "checkBox4";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(520, 212);
            button1.Name = "button1";
            button1.Size = new Size(144, 23);
            button1.TabIndex = 14;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(535, 282);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(482, 285);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 16;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(641, 285);
            label7.Name = "label7";
            label7.Size = new Size(38, 15);
            label7.TabIndex = 17;
            label7.Text = "label7";
            // 
            // button2
            // 
            button2.Location = new Point(430, 389);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 18;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(629, 389);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 19;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(54, 38);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 20;
            label8.Text = "label8";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(453, 38);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 21;
            label9.Text = "label9";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(40, 27);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(279, 385);
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(430, 27);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(309, 226);
            pictureBox2.TabIndex = 23;
            pictureBox2.TabStop = false;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label7);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(button1);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label9);
            Controls.Add(pictureBox2);
            Controls.Add(numericUpDown3);
            Controls.Add(label5);
            Controls.Add(numericUpDown2);
            Controls.Add(label4);
            Controls.Add(numericUpDown1);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(label8);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MaximumSize = new Size(816, 489);
            MinimumSize = new Size(816, 489);
            Name = "Form2";
            Text = "Form2";
            ((ISupportInitialize)numericUpDown1).EndInit();
            ((ISupportInitialize)numericUpDown2).EndInit();
            ((ISupportInitialize)numericUpDown3).EndInit();
            ((ISupportInitialize)pictureBox1).EndInit();
            ((ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private Button button1;
        private TextBox textBox1;
        private Label label6;
        private Label label7;
        private Button button2;
        private Button button3;
        private Label label8;
        private Label label9;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}
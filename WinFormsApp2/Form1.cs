namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Model model;
        public Form1(Model mod)
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("utaztatas.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            model = mod;
            label1.Text = "Felhasználónév";
            label2.Text = "Jelszó";
            button1.Text = "Regisztráció";
            button2.Text = "Bejelentkezés";
            button1.Click += (s, e) =>
            {
                if (model.Registration(textBox1.Text, textBox2.Text) == false)
                {
                    MessageBox.Show("Már van ilyen felhasználó");
                }
            };
            button2.Click += (s, e) =>
            {
                if (model.SignIn(textBox1.Text, textBox2.Text))
                {
                    Form2 travel = new Form2(model, textBox1.Text);
                    travel.Show();
                }
                else
                {
                    MessageBox.Show("Nincs ilyen felhasználó");
                }
            };
        }
        private System.ComponentModel.IContainer components = null;
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(241, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(241, 161);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(493, 75);
            button1.Name = "button1";
            button1.Size = new Size(112, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(493, 161);
            button2.Name = "button2";
            button2.Size = new Size(112, 23);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(93, 78);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 164);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 5;
            label2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
    }
}
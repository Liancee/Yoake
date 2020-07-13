using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoake
{
    public partial class Form1 : Form
    {
        private KeyValuePair<string, Bitmap> CurrKVPair { get; set; }
        private Dictionary<string, Bitmap> Dic { get; set; }
        private Dictionary<Bitmap, string> ReversedDic { get; set; }
        private Random Rnd = new Random();
        private bool Reversed { get; set; }

        public Form1()
        {
            InitializeComponent();
            Reversed = false;
            GetDic();
            Load += Form1_Load;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button1.GotFocus += Button1_GotFocus;
            textBox1.KeyDown += TextBox1_KeyDown;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                CheckAndContinue();

        }

        private void Button1_GotFocus(object sender, EventArgs e)
        {
            Controls[2].Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProgramStart();
        }

        private void ProgramStart() => Controls.Add(GetControls(Dic.ToList()[Rnd.Next(0, Dic.Count)]));

        private GroupBox GetControls(KeyValuePair<string, Bitmap> kVPair, bool reversed = false)
        {
            CurrKVPair = kVPair;

            if (!Reversed)
            {

                var button = new Button();
                button.Dock = DockStyle.Fill;
                button.BackgroundImage = CurrKVPair.Value;
                button.Click += Button_Click;
                button.BackgroundImageLayout = ImageLayout.Stretch;

                var groupBox = new GroupBox();
                groupBox.Dock = DockStyle.Fill;
                groupBox.Controls.Add(button);
                return groupBox;
            }
            else
            {
                
                textBox1.Text = CurrKVPair.Key;

                var i = 0;
                var imageList = new List<Bitmap>() { CurrKVPair.Value };
                do
                {
                    imageList.AddRange(Dic.Values.ToList().OrderBy(x => Rnd.Next()).Take(3).ToList());
                }
                while (imageList.Distinct().Count() < 4);

                var userControl = new UserControl1();
                userControl.Dock = DockStyle.Fill;

                var button1 = (Button)userControl.Controls[0];
                i = Rnd.Next(0, imageList.Count());
                button1.BackgroundImage = imageList[i];
                button1.Click += PButton1_Click;
                button1.BackgroundImageLayout = ImageLayout.Stretch;
                imageList.RemoveAt(i);

                var button2 = (Button)userControl.Controls[1];
                i = Rnd.Next(0, imageList.Count());
                button2.BackgroundImage = imageList[i];
                button2.Click += PButton2_Click;
                button2.BackgroundImageLayout = ImageLayout.Stretch;
                imageList.RemoveAt(i);

                var button3 = (Button)userControl.Controls[2];
                i = Rnd.Next(0, imageList.Count());
                button3.BackgroundImage = imageList[i];
                button3.Click += PButton3_Click;
                button3.BackgroundImageLayout = ImageLayout.Stretch;
                imageList.RemoveAt(i);

                var button4 = (Button)userControl.Controls[3];
                button4.BackgroundImage = imageList.First();
                button4.Click += PButton4_Click;
                button4.BackgroundImageLayout = ImageLayout.Stretch;

                var groupBox = new GroupBox();
                groupBox.Dock = DockStyle.Fill;
                groupBox.Controls.Add(userControl);
                return groupBox;
            }
        }

        private void PButton1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundImage == CurrKVPair.Value)
            {
                Controls.RemoveAt(3);
                ProgramStart();
            }
        }

        private void PButton2_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundImage == CurrKVPair.Value)
            {
                Controls.RemoveAt(3);
                ProgramStart();
            }
        }

        private void PButton3_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundImage == CurrKVPair.Value)
            {
                Controls.RemoveAt(3);
                ProgramStart();
            }
        }

        private void PButton4_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundImage == CurrKVPair.Value)
            {
                Controls.RemoveAt(3);
                ProgramStart();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            CheckAndContinue();
        }
        private void CheckAndContinue()
        {
            if (textBox1.Text == CurrKVPair.Key)
            {
                textBox1.Text = string.Empty;
                Controls.RemoveAt(3);
                ProgramStart();
            }
        }

        private void GetDic()
        {
            Dic = new Dictionary<string, Bitmap>()
                {
                    {"a", Properties.Resources.Hiragana_A },
                    {"i", Properties.Resources.Hiragana_I },
                    {"u", Properties.Resources.Hiragana_U },
                    {"e", Properties.Resources.Hiragana_E },
                    {"o", Properties.Resources.Hiragana_O },

                    {"ka", Properties.Resources.Hiragana_KA },
                    {"ki", Properties.Resources.Hiragana_KI },
                    {"ku", Properties.Resources.Hiragana_KU },
                    {"ke", Properties.Resources.Hiragana_KE },
                    {"ko", Properties.Resources.Hiragana_KO },

                    //{"sa", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"shi", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"su", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"se", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"so", Properties.Resources._220px_Dramatic_Chipmunk },

                    //{"ta", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"chi", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"tsu", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"te", Properties.Resources._220px_Dramatic_Chipmunk },
                    //{"to", Properties.Resources._220px_Dramatic_Chipmunk }
                };
            ReversedDic = Dic.ToList().ToDictionary(x => x.Value, x => x.Key);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Controls[0].Text = "";
            Controls.RemoveAt(3);
            Reversed = !Reversed;
            if (Reversed)
                textBox1.ReadOnly = true;
            else textBox1.ReadOnly = false;
            ProgramStart();
        }
    }
}

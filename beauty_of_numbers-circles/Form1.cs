using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beauty_of_numbers_circles
{
    public partial class Form1 : Form
    {
        static int width, height, span, spanb, s;
        static int x, y, sx, sy, qx, qy, times;
        StreamReader reader;
        DateTime prev, now;
        TimeSpan one, approx;
        int app;

        Form2 form2;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            reader = new StreamReader(myStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label6.Text = richTextBox1.Text.Length.ToString() + '/' + ((Convert.ToInt32(numericUpDown4.Value) - (2 * Convert.ToInt32(numericUpDown3.Value))) / (Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value)) + 1) * ((Convert.ToInt32(numericUpDown5.Value) - (2 * Convert.ToInt32(numericUpDown3.Value))) / (Convert.ToInt32(numericUpDown1.Value) + Convert.ToInt32(numericUpDown2.Value)) + 1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public Graphics g;
        //char[] number;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            s = Convert.ToInt32(numericUpDown1.Value);
            span = Convert.ToInt32(numericUpDown2.Value);
            spanb = Convert.ToInt32(numericUpDown3.Value);
            width = Convert.ToInt32(numericUpDown4.Value);
            height = Convert.ToInt32(numericUpDown5.Value);

            qx = (width - (2 * spanb)) / (s + span) + 1;
            qy = (height - (2 * spanb)) / (s + span) + 1;

            //if (textBox1.Text.Length != 137)
            //    MessageBox.Show("Ilość znaków musi być równa 137, a jest równa " + textBox1.Text.Length, "Za mało/dużo znaków!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //else if (richTextBox1.Text.Length < qx * qy)
                //MessageBox.Show("Ilość znaków musi być większa lub równa x * y", "Za mało znaków!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //else
            //{
                //number = new char[richTextBox1.Text.Length + 1];

                //for (int i = 0; i < richTextBox1.Text.Length; i++)
                    //number[i] = richTextBox1.Text[i];

                //number[richTextBox1.Text.Length] = '#';

                SystemSounds.Beep.Play();

                form2 = new beauty_of_numbers_circles.Form2();

                if (width == SystemInformation.VirtualScreen.Width && height == SystemInformation.VirtualScreen.Height)
                {
                    form2.FormBorderStyle = FormBorderStyle.None;
                    form2.WindowState = FormWindowState.Maximized;
                    form2.TopMost = true;
                }
                form2.Width = Convert.ToInt32(numericUpDown4.Value + 15);
                form2.Height = Convert.ToInt32(numericUpDown5.Value + 38);
                form2.BackColor = Color.Black;
                form2.Show();

                this.Close();

                g = form2.CreateGraphics();

                times = 0;
                x = spanb;
                y = spanb;

                for (int i = 0; i < qy; i++)
                {
                    prev = DateTime.Now;

                    for (int j = 0; j < qx; j++)
                    {
                        draw_rect(x, y, s, richTextBox1.Text[times]);
                        x += span + s;
                        times++;

                        if (times == richTextBox1.Text.Length - 1)
                            break;
                    }
                    now = DateTime.Now;
                    one = now - prev;
                    approx = new TimeSpan(one.Ticks * (qy - (times / qx + 1)));

                    g.FillRectangle(new SolidBrush(Color.Black), spanb, height - spanb / 2 - 10, 300, 100);
                    g.DrawString(approx.Hours.ToString() + ':' + approx.Minutes.ToString() + ':' + approx.Seconds.ToString(), new Font("Courier new", 12), new SolidBrush(Color.White), spanb, height - spanb / 2 - 10);

                    if (times == richTextBox1.Text.Length - 1)
                        break;
                    x = spanb;
                    y += span + s;
                }

                SystemSounds.Beep.Play();

                x = spanb + s / 2;
                y = spanb + s / 2;
                times = 0;

                for (int i = 0; i < qy; i++)
                {
                    prev = DateTime.Now;
                    for (int j = 0; j < qx; j++)
                    {
                        draw_lines(x, y, times, qx);
                        x += span + s;
                        times++;

                        if (times == richTextBox1.Text.Length - 1)
                            break;
                    }
                    now = DateTime.Now;
                    one = now - prev;
                    approx = new TimeSpan(one.Ticks * (qy - (times / qx + 1)));

                    g.FillRectangle(new SolidBrush(Color.Black), spanb, height - spanb / 2 - 10, 300, 100);
                    g.DrawString(approx.Hours.ToString() + ':' + approx.Minutes.ToString() + ':' + approx.Seconds.ToString(), new Font("Courier new", 12), new SolidBrush(Color.White), spanb, height - spanb / 2 - 10);

                    if (times == richTextBox1.Text.Length - 1)
                        break;
                    x = spanb + s / 2;
                    y += span + s;
                }

                SystemSounds.Beep.Play();

                g.FillRectangle(new SolidBrush(Color.Black), spanb, height - spanb / 2 - 10, 300, 100);
                g.DrawString(textBox1.Text, new Font("Courier new", 16), new SolidBrush(Color.White), spanb + 5, spanb / 2 - 15);
                g.DrawString("Adam Pisula 2016", new Font("Courier new", 16), new SolidBrush(Color.White), (width - spanb - 220), (height - spanb / 2 - 15));

                Thread.Sleep(15000);

                g.Dispose();
                form2.FormBorderStyle = FormBorderStyle.Sizable;
                form2.WindowState = FormWindowState.Normal;
                form2.TopMost = false;
            //}
        }

        private Color choose_color(char number)
        {
            Color col;

            switch (number)
            {
                case '0':
                    col = Color.White;
                    break;

                case '1':
                    col = ColorTranslator.FromHtml("#EA0037");
                    break;

                case '2':
                    col = ColorTranslator.FromHtml("#FF7100");
                    break;

                case '3':
                    col = ColorTranslator.FromHtml("#FFDE00");
                    break;

                case '4':
                    col = ColorTranslator.FromHtml("#71E500");
                    break;

                case '5':
                    col = ColorTranslator.FromHtml("#00A57D");
                    break;

                case '6':
                    col = ColorTranslator.FromHtml("#028D9B");
                    break;

                case '7':
                    col = ColorTranslator.FromHtml("#001979");
                    break;

                case '8':
                    col = ColorTranslator.FromHtml("#7209AA");
                    break;

                case '9':
                    col = ColorTranslator.FromHtml("#C50080");
                    break;
                default:
                    col = Color.Black;
                    break;
            }

            return col;
        }

        private void draw_rect(int x, int y, int size, char number) 
        {
            SolidBrush sb = new SolidBrush(choose_color(number));

            g.FillRectangle(sb, x, y, size, size);
        }

        private void draw_lines(int x, int y, int times, int qx)
        {
            Pen pen = new Pen(choose_color(richTextBox1.Text[times]), s / 4);

            if (times == 0)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times + 1])         g.DrawLine(pen, x, y, x + (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y + (span + s));
            }

            else if (times > 0 && times < qx)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
            }

            else if (times == qx - 1)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
            }

            else if (times % qx == 0 && times != qx * qy - qx)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + 1])         g.DrawLine(pen, x, y, x + (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y + (span + s));
            }

            else if ((times + 1) % qx == 0 && times != qx * qy - 1)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
            }

            else if (times == qx * qy - qx)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + 1])         g.DrawLine(pen, x, y, x + (span + s), y);
            }

            else if (times > qx * qy - qx && times < qx * qy - 1)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + 1])         g.DrawLine(pen, x, y, x + (span + s), y);
            }

            else if (times == qx * qy - 1)
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
            }

            else
            {
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx])        g.DrawLine(pen, x, y, x, y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y - (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times - 1])         g.DrawLine(pen, x, y, x - (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + 1])         g.DrawLine(pen, x, y, x + (span + s), y);
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx - 1])    g.DrawLine(pen, x, y, x - (span + s), y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx])        g.DrawLine(pen, x, y, x, y + (span + s));
                if (richTextBox1.Text[times] == richTextBox1.Text[times + qx + 1])    g.DrawLine(pen, x, y, x + (span + s), y + (span + s));
            }
        }
    }
}

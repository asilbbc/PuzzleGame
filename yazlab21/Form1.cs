using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace yazlab21
{
    public partial class Form1 : Form
    {
        int saniye = 0;
        int dakika = 0;
        int puan = 100;



        Image[] imgr = new Image[16];

        static Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            dosyaoku();


        }

        private int[] karıstır(int[] array)
        {
            Random rand = new Random();
            array = array.OrderBy(x => rand.Next()).ToArray();
            return array;
        }
        public void dosyaoku()
        {
            try
            {

                using (StreamReader sr = new StreamReader("C:\\Users\\Asus\\Desktop\\asil_zafer\\yazlab21\\enyuksekskor.txt"))
                {
                    string line;


                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] puanlar = File.ReadAllLines("C:\\Users\\Asus\\Desktop\\asil_zafer\\yazlab21\\enyuksekskor.txt");

                        Array.Sort(puanlar);
                        Array.Reverse(puanlar);


                        textBox2.Text = puanlar[0];





                    }
                }


                // Array.Sort<string>(puanlar,new Comparison<string>((i1,i2)=>i1.CompareTo(i2)));



            }
            catch (Exception e)
            {
            }


        }
        public void dosyayaz()
        {
            using (StreamWriter sw = File.AppendText("C:\\Users\\Asus\\Desktop\\asil_zafer\\yazlab21\\enyuksekskor.txt"))
            {

                sw.WriteLine(puan);

            }
        }


        public static List<int> RandomUret(int count)
        {

            HashSet<int> aday = new HashSet<int>();
            while (aday.Count < count)
            {

                aday.Add(rnd.Next() % 16);
            }

            List<int> result = new List<int>();
            result.AddRange(aday);

            int i = result.Count;
            while (i > 1)
            {
                i--;
                int k = rnd.Next(i + 1) % 16;
                int value = result[k];
                result[k] = result[i];
                result[i] = value;
            }
            return result;


        }

        Image resim1;
        private void buttonResim_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file.RestoreDirectory = true;
                file.CheckFileExists = false;
                file.Multiselect = false;
                string resim = file.FileName;
                resim1 = Image.FromFile(resim);
                resim1 = (new Bitmap(resim1, new Size(400, 400)));
            }


        }
        Image[] imgarray = new Image[16];
        private void buttonKaristir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var index = i * 4 + j;
                    imgarray[index] = new Bitmap(100, 100);
                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(resim1, new Rectangle(0, 0, 100, 100), new Rectangle(j * 100, i * 100, 100, 100), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }

            List<int> list = RandomUret(16);

            for (int i = 0; i < 16; i++)
            {
                imgr[i] = imgarray[list[i]];
            }

            button1.BackgroundImage = imgr[0];
            button2.BackgroundImage = imgr[1];
            button3.BackgroundImage = imgr[2];
            button4.BackgroundImage = imgr[3];
            button5.BackgroundImage = imgr[4];
            button6.BackgroundImage = imgr[5];
            button7.BackgroundImage = imgr[6];
            button8.BackgroundImage = imgr[7];
            button9.BackgroundImage = imgr[8];
            button10.BackgroundImage = imgr[9];
            button11.BackgroundImage = imgr[10];
            button12.BackgroundImage = imgr[11];
            button13.BackgroundImage = imgr[12];
            button14.BackgroundImage = imgr[13];
            button15.BackgroundImage = imgr[14];
            button16.BackgroundImage = imgr[15];


            int sayac = 0;
            for (int i = 0; i < 16; i++)
            {


                if (karsilastir((Bitmap)imgr[i], (Bitmap)imgarray[i]) > 0)
                {
                    sayac++;
                }


            }
            if (sayac < 16)
            {
                timer1.Interval = 1000;

                if (sayac == 0)
                {
                    MessageBox.Show("En yüksek puanı kazandınız 100!");



                    using (StreamWriter sw = new StreamWriter("enyuksekskor.txt"))
                    {
                        int s = 100;

                        sw.WriteLine(s);

                    }

                }

                timer1.Start();


            }
            else
            {
                MessageBox.Show("Puzzlede en az bir parça doğru yerinde değil tekrar karıştırınız!");
            }


        }

        private int karsilastir(Bitmap img1, Bitmap img2)
        {
            int hataliPixel = 0;
            string s1, s2;


            for (int i = 0; i < img1.Width; i++)
            {
                for (int j = 0; j < img1.Height; j++)
                {
                    s1 = img1.GetPixel(i, j).ToString();
                    s2 = img2.GetPixel(i, j).ToString();

                    if (s1 != s2)
                    {
                        hataliPixel++;
                    }
                }
            }


            return hataliPixel;
        }

        private void değiştir(int i1, int i2)
        {
            Image temp;
            temp = imgr[i1];
            imgr[i1] = imgr[i2];
            imgr[i2] = temp;
        }

        int a = 0;
        int g1 = 0;
        int g2 = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            a++;

            if (a == 1)
            {
                g1 = 0;
            }

            if (a == 2)
            {
                g2 = 0;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 1;
            }

            if (a == 2)
            {
                g2 = 1;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 2;
            }

            if (a == 2)
            {
                g2 = 2;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 3;
            }

            if (a == 2)
            {
                g2 = 3;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 4;
            }

            if (a == 2)
            {
                g2 = 4;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 5;
            }

            if (a == 2)
            {
                g2 = 5;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 6;
            }

            if (a == 2)
            {
                g2 = 6;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 7;
            }

            if (a == 2)
            {
                g2 = 7;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 8;
            }

            if (a == 2)
            {
                g2 = 8;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 9;
            }

            if (a == 2)
            {
                g2 = 9;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 10;
            }

            if (a == 2)
            {
                g2 = 10;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 11;
            }

            if (a == 2)
            {
                g2 = 11;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 12;
            }

            if (a == 2)
            {
                g2 = 12;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 13;
            }

            if (a == 2)
            {
                g2 = 13;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 14;
            }

            if (a == 2)
            {
                g2 = 14;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            a++;
            if (a == 1)
            {
                g1 = 15;
            }

            if (a == 2)
            {
                g2 = 15;
                değiştir(g1, g2);
                g1 = 0;
                g2 = 0;
                a = 0;
                button1.BackgroundImage = imgr[0];
                button2.BackgroundImage = imgr[1];
                button3.BackgroundImage = imgr[2];
                button4.BackgroundImage = imgr[3];
                button5.BackgroundImage = imgr[4];
                button6.BackgroundImage = imgr[5];
                button7.BackgroundImage = imgr[6];
                button8.BackgroundImage = imgr[7];
                button9.BackgroundImage = imgr[8];
                button10.BackgroundImage = imgr[9];
                button11.BackgroundImage = imgr[10];
                button12.BackgroundImage = imgr[11];
                button13.BackgroundImage = imgr[12];
                button14.BackgroundImage = imgr[13];
                button15.BackgroundImage = imgr[14];
                button16.BackgroundImage = imgr[15];
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int sayac = 0;
            for (int i = 0; i < 16; i++)
            {
                if (karsilastir((Bitmap)imgr[i], (Bitmap)imgarray[i]) > 0)
                {
                    sayac++;
                }
            }

            if (sayac == 0)
            {
                timer1.Stop();
                button20.Enabled = false;



                puan = puan - dakika * 60 - saniye + 10;

                MessageBox.Show("Tebrikler " + puan + " puan ile puzzle'ı tamamladınız !");
                if (0 <= puan && puan <= 100)
                {
                    dosyayaz();

                }
                else
                {
                    puan = 0;
                    dosyayaz();
                }




            }
            else
            {
                timer1.Stop();
                button20.Enabled = false;

                puan = puan - dakika * 60 - saniye - sayac * 5 + 10;
                MessageBox.Show("Puzzle'ı" + puan + "puan ile tamamlayamadınız !\n" + sayac + " adet parça yanlış yerde:))))))");

                if (0 <= puan && puan <= 100)
                {
                    dosyayaz();

                }
                else
                {
                    puan = 0;
                    dosyayaz();
                }








            }



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (saniye == 60)
            {
                saniye = 0;
                dakika++;
            }
            if (dakika != 0)
            {
                textBox1.Text = Convert.ToString(dakika + " dakika " + saniye + " saniye");
            }
            else
            {
                textBox1.Text = Convert.ToString(saniye + " saniye");
            }
            saniye++;

        }


    }
}
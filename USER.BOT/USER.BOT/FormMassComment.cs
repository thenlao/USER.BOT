﻿





using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace USER.BOT
{
    public partial class FormMassComment : Form
    {
        public string access_token;
        public string user_id;
        bool CapthaEnter = false;
        Random rnd = new Random();
        

        public FormMassComment()
        {
            InitializeComponent();
        }

        private void FormMassComment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int Number;
            int MoveL;


            if (textBox2.Text == "")
            {
                //label11.Visible = true;
                //label11.BackColor = Color.Red;
                //label2.BackColor = Color.Red;
                //textBox2.BackColor = Color.Red;
                //for(int I = 0;I<10;I++)
                //{
                //    Application.DoEvents();
                //    Thread.Sleep(100);
                //}    

                //label11.Visible = false;
                //label11.BackColor = Color.White;
                //label2.BackColor = Color.White;
                //textBox2.BackColor = Color.White;
                //доделать------------------------------------------------------------------------
                MoveL = rnd.Next(-1, 1);
                textBox2.Location = new Point(15 - 1, 347 - 1);
                Application.DoEvents();

            }
            
            
            for (int i = 0; i < textBox3.Lines.Length; i = i + 1)
            {
                if (textBox3.Lines[i] == "")
                {
                    continue;
                }
                //if(textBox3.Lines[i] = )
                //{
                //    
                //}
                //"https://vk.com/id435786543"
                //"https://vk./com/Id435786543"
                //"https:" "vk" "comid435786543"
                //Random Number--------------------------------------------------------------



               
              

                string screen_name = textBox3.Lines[i];
                string[] ID = screen_name.Split(new[] { "https://vk.com/" }, StringSplitOptions.RemoveEmptyEntries);
                int LAST = ID.Length - 1;
                string request = "https://api.vk.com/method/users.get?user_ids=" + ID[0] + "&" + access_token + "&v=5.124";
                WebClient clienT = new WebClient();
                string AnsweR = clienT.DownloadString(request);


                UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(AnsweR);
                string Request = "https://api.vk.com/method/wall.get?" + "owner_id=" + ug.response[0].id.ToString() + "&" +
                              access_token + "&v=5.124";
                string Answer = clienT.DownloadString(Request);


                WallGet wg = JsonConvert.DeserializeObject<WallGet>(Answer);
                int Posts = 0;
               
                textBox3.Lines[i] = ug.response[0].id.ToString();
                progressBar1.Value = 0;
                foreach (WallGet.Item item in wg.response.items)
                {
                    Posts = Posts + 1;
                    if (Posts > numericUpDown1.Value)
                    {
                        break;
                    }
                    string RequeST = "https://api.vk.com/method/wall.createComment?" + "owner_id=" + ug.response[0].id.ToString() + "&post_id=" + item.id + "&message=" + textBox2.Text + "&" + access_token + "&v=5.124";
                    WebClient clieNT = new WebClient();
                    string AnswER = clieNT.DownloadString(RequeST);

                    textBox4.Text += "\r\n" + AnswER;

                    Number = rnd.Next(3000, 5000);
                    int NumberII = Number / 100;
                    for (int I = 0; I < 100; I++)
                    {
                        Thread.Sleep(NumberII);
                    }
                    //Капча нахоидится где-то ещё в цикле найти это место и исправить(постваить ждалку ввода капчи пользователем)
                    if (AnswER.Contains("Captcha needed"))
                    {
                        Err_main er = JsonConvert.DeserializeObject<Err_main>(AnswER);
                        pictureBox1.ImageLocation = er.error.captcha_img;
                        //подождать 
                        
                        while(CapthaEnter == false)
                        {
                            textBox4.Text +=  " 1000" ;
                            for (int I = 0;I<10;I++)
                            {
                                Thread.Sleep(100);
                                Application.DoEvents();
                            }
                        }
                        string RequEST = "https://api.vk.com/method/wall.createComment?" + "owner_id=" + ug.response[0].id.ToString() + "&post_id=" + item.id + "&message=" + textBox2.Text + "&captcha_sid=" + er.error.captcha_sid + "&captha_key=" + textBox1.Text + "&" + access_token + "&v=5.124";


                    }

                    //label1.Text = AnswER;
                    label7.Text = Posts.ToString();
                    Application.DoEvents();
                    label8.Text = numericUpDown1.Value.ToString();
                    progressBar1.Maximum = (int)numericUpDown1.Value;
                    progressBar1.Value = Posts;

                   

                    int Result;
                    int ost = Math.DivRem(Posts, 9, out Result);
                    if (Result == 0)
                    {
                        // label12.Text = AnswER;
                        int NumberFC = rnd.Next(15000, 20000);
                        textBox4.Text += " " + NumberFC.ToString();
                        int NumberFCI = NumberFC / 100;
                       for(int I = 0;I < 100; I++)
                        {
                            Application.DoEvents();
                            Thread.Sleep(NumberFCI);

                        }

                    }
                 
                   
                    Application.DoEvents();
                }

                //ждалка между ссылками

                Number = rnd.Next(3000, 5000);
                int NumberI = Number / 100;
                for(int I = 0;I < 100; I++)
                {
                    Thread.Sleep(NumberI);
                    Application.DoEvents();

                }
                


                //--------------------

                

                //--------------------



            }


            //Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        private void button2_Click_1(object sender, EventArgs e)
        {
            CapthaEnter = true;
            for(int I = 0; I < 10; I++)
            {
                Thread.Sleep(200);
                
            }
            CapthaEnter = false;

            if (CapthaEnter == true)
            {
                label3.Text = "CapthaEnter - true";
            }
            else
            {
                label3.Text = "CapthaEnter - false";
            }



        }
         

private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Visible = true;
            textBox4.Size = new Size(0, 0);
           
            Application.DoEvents();
            double TSW = textBox4.Width;
            for (int  TSH = textBox4.Size.Height; TSH < 282; TSW = TSW + 6.3, TSH = TSH + 3)
            {
                textBox4.Size = new Size((int)TSW, TSH);
                Thread.Sleep(1);
                Application.DoEvents();
            }
            button4.Visible = true;
            button3.Enabled = false;
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox4.Visible = false;
            button4.Visible = false;
            
            
            //int TSW = textBox4.Size.Width;
            //int TSH = textBox4.Size.Height;
            for(int TSW = textBox4.Size.Width, TSH = textBox4.Size.Height; TSH > 0; TSW = TSW - 6,TSH = TSH - 3)
            {
                textBox4.Size = new Size(TSW, TSH);
                Thread.Sleep(1);
                Application.DoEvents();

            }
            textBox4.Visible = false;
            button3.Enabled = true;
           

        }

    }
}

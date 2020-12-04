





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

        public FormMassComment()
        {
            InitializeComponent();
        }

        private void FormMassComment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int Number = rnd.Next(1000, 3000);
            int NumberFC = rnd.Next(10000, 15000);


            if (textBox2.Text == "")
            {
                label11.Visible = true;
                label11.BackColor = Color.Red;
                label2.BackColor = Color.Red;
                textBox2.BackColor = Color.Red;
                Application.DoEvents();
                Thread.Sleep(1000);
                label11.Visible = false;
                label11.BackColor = Color.White;
                label2.BackColor = Color.White;
                textBox2.BackColor = Color.White;
                return;

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
                    Thread.Sleep(1000);

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
                        Thread.Sleep(NumberFC);
                        Application.DoEvents();
                    }


                }



                Thread.Sleep(Number);
                Application.DoEvents();

                //--------------------

                

                //--------------------



            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
            
        }
    }
}

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
            string screen_name = textBox1.Text;
            string[] ID = screen_name.Split(new[] {"https://vk.com/"}, StringSplitOptions.RemoveEmptyEntries);
            string request = "https://api.vk.com/method/users.get?user_ids=" + ID[0] + "&" + access_token + "&v=5.124";
            WebClient clienT = new WebClient();
            string AnsweR = clienT.DownloadString(request);


            UsersGet ug = JsonConvert.DeserializeObject<UsersGet>(AnsweR);
            string Request = "https://api.vk.com/method/wall.get?" + "owner_id=" + ug.response[0].id.ToString() + "&" +
                          access_token + "&v=5.124";
            string Answer = clienT.DownloadString(Request);
            

            WallGet wg = JsonConvert.DeserializeObject<WallGet>(Answer);
            // label1.Text = AnsweR;
            int Posts = 0;
            // labelFam.Text = wg.response.items[0].text;
            textBox1.Text = ug.response[0].id.ToString();
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
                
            }
        }
    }
}

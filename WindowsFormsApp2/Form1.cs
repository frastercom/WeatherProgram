using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "Выполняется запрос, необходимо подождать";
                string s = "";
                WebRequest request = WebRequest.Create("http://gitmyserver.ddns.net:8080/api/w");
                request.Timeout = 5000; // устанавливаем таймаут на 5 секунд
                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        s = reader.ReadToEnd();
                    }
                }
                response.Close();
                Weather message = JsonSerializer.Deserialize<Weather>(s);
                textBox1.Text = message.data.First().country_code;
                textBox2.Text = message.data.First().datetime;
                textBox3.Text = message.data.First().temp;
                textBox4.Text = message.data.First().city_name;
            } 
            catch
            {
                textBox1.Text = "Сервер недоступен или произошла непредвиденная ошибка";
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pogoda
{
    public partial class Form1 : Form
    {   
        const string API_KEY = "9152c2ba1c9b3c8546f73355913d6a47";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                String city = textBox1.Text.ToString();
                textBox1.Clear();
                Api api = new Api();
                Root weather = api.LoadData(city, API_KEY);

                string picId = weather.weather[0].icon;
                int currentTemp = (int)weather.main.temp;
                int seconds = weather.dt + weather.timezone - 7200;
                DateTime fullDate = Date.UnixTimeStampToDateTime(seconds);
                string time = fullDate.ToString("HH:mm:ss");
                string date = fullDate.ToLongDateString();

                richTextBox2.Text = city + "\n" + date + "\n" + time;

                pictureBox1.ImageLocation = "http://openweathermap.org/img/wn/" + picId + "@2x.png";
                richTextBox4.Text = string.Format("{0} °C", currentTemp);

                richTextBox1.Text = "\nNiebo:";
                richTextBox1.Text += "\nCiśnienie:";
                richTextBox1.Text += "\nWilgotność powietrza:";
                richTextBox3.Text = "\n" + weather.weather[0].description;
                richTextBox3.Text += "\n" + weather.main.pressure;
                richTextBox3.Text += "\n" + weather.main.humidity;

            }
            catch(WebException ex)
            {
                richTextBox1.Text = "Error: City not found";
                richTextBox2.Text = "Error: City not found";
                pictureBox1.ImageLocation = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

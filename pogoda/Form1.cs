using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace pogoda
{   
    public partial class Form1 : Form
    {
        favourites favourite = new favourites();
        const string API_KEY = "9152c2ba1c9b3c8546f73355913d6a47";
        public Form1()
        {
            InitializeComponent();
            showWeatherDataGridView1();
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
                richTextBox1.Text = "";
                richTextBox2.Text = "Error: City not found";
                richTextBox3.Text = "";
                richTextBox4.Text = "";
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String city = textBox1.Text.ToString();
                Api api = new Api();
                Root weather = api.LoadData(city, API_KEY);


                favourite.city = textBox1.Text;
                using (DBEntities db = new DBEntities())
                {
                    db.favourites.Add(favourite);
                    db.SaveChanges();
                }
                showWeatherDataGridView1();
            }
            catch (WebException ex)
            {
                richTextBox1.Text = "";
                richTextBox2.Text = "Error: City not found";
                richTextBox3.Text = "";
                richTextBox4.Text = "";
                pictureBox1.ImageLocation = "";
            }

        }

        void showWeatherDataGridView1()
        {
            using (DBEntities db = new DBEntities())
            {
                dataGridView1.DataSource = db.favourites.ToList<favourites>();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                var entry = db.Entry(favourite);
                if(entry.State == System.Data.Entity.EntityState.Detached)
                {
                    db.favourites.Attach(favourite);
                }
                db.favourites.Remove(favourite);
                db.SaveChanges();
                showWeatherDataGridView1();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                favourite.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                using (DBEntities db = new DBEntities())
                {
                    favourite = db.favourites.Where(x => x.id == favourite.id).FirstOrDefault();
                    textBox1.Text = favourite.city;
                }
            }
        }
    }
}

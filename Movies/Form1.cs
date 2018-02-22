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
using System.Web;
using System.Web.Script.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using System.Xml;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;




namespace Movies
{
    public partial class Form1 : Form
    {
        int pageCounter = 1;
        List<Search> pageMovies = new List<Search>();

        public Form1()
        {
            InitializeComponent();
            
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        static List<RootObject> results = new List<RootObject>();

        private void button1_Click(object sender, EventArgs e)
        {


            string url = "http://www.omdbapi.com/?apikey=c86272c6&s=" + txtMovieName.Text;

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);


                Search obj = JsonConvert.DeserializeObject<Search>(json);

                listBox1.Items.Add(obj.Title + " " + obj.Year);


            }
        }


        private void GetMovieList()
        {
            throw new NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("Armin");
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Get the currently selected item in the ListBox.

            pictureBox1.ImageLocation = pageMovies[listBox1.SelectedIndex].Poster;

          //  listBox1.SelectedItem.ToString();

           /* // Find the string in ListBox2.
            int index = listBox1.FindString(curItem);
            // If the item was not found in ListBox 2 display a message box, otherwise select it in ListBox2.
            if (index == -1)
                MessageBox.Show("Item is not available in ListBox2");
            else
                listBox1.SetSelected(index, true);*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
             string url = "http://www.omdbapi.com/?apikey=c86272c6&s=" + txtMovieName.Text;

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);

                var obj = JsonConvert.DeserializeObject<RootObject>(json);

                if (txtMovieName.Text == "")
                {
                    MessageBox.Show("Empty field !!!");
                }

                else if (obj.Response == "True")
                {
                    int result = Convert.ToInt32(obj.totalResults);
                    int granice = (result > 10) ? 10 : result;

                    listBox1.Items.Clear();

                    for (int i=0;i< granice; i++)
                    { 
                    
                    listBox1.Items.Add(obj.Search[i].Title + " " + obj.Search[i].Year);
                        pageMovies.Add(obj.Search[i]);
                   
                    }
 
                    txtResult.Text = " Ukupno rezultata " + obj.totalResults;

                    lblResult.Text = (result / 10).ToString();

                }

                else if (obj.Response == "False")
                {
                    MessageBox.Show("MOVIE NOT Found !!!  ");

                    
                }

                else
                {
                    MessageBox.Show("Something Wrong ");
                }

            }
            }

        private void getPageMovies(int pageNumber)
        {
            pageMovies.Clear();
            string url = "http://www.omdbapi.com/?apikey=c86272c6&s=" + txtMovieName.Text + "&page=" + pageNumber.ToString();

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);

                var obj = JsonConvert.DeserializeObject<RootObject>(json);

               // List<string> movies = new List<string>();

                if (txtMovieName.Text == "")
                {
                    MessageBox.Show("Empty field !!!");
                }

                else if (obj.Response == "True")
                {
                    int result = Convert.ToInt32(obj.totalResults);
                    int granice = (result > 10) ? 10 : result;

                    for (int i = 0; i < granice; i++)
                    {
                        //listBox1.Items.Add(obj.Search[i].Title + " " + obj.Search[i].Year);
                        pageMovies.Add(obj.Search[i]);
                    }

                }

                else if (obj.Response == "False")
                {
                    MessageBox.Show("MOVIE NOT Found !!!  ");

                }

                else
                {
                    MessageBox.Show("Something Wrong ");
                }

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
                   lblTitle.Text = pageMovies[listBox1.SelectedIndex].Title;

            lblYear.Text = pageMovies[listBox1.SelectedIndex].Year;

            // pictureBox1.ImageLocation = obj.Poster;

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (pageCounter < Convert.ToInt32(lblResult.Text))
                pageCounter++;

            lblPage.Text = pageCounter.ToString();

            getPageMovies(pageCounter);

            listBox1.DataSource = pageMovies.Select(m => m.Title + " " + m.Year).ToList();

        }

        private void lblResult_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pageCounter > 1)
                pageCounter--;

            lblPage.Text = pageCounter.ToString();

            getPageMovies(pageCounter);
            listBox1.DataSource = pageMovies.Select(m => m.Title + " " + m.Year).ToList();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Form2 naziv = new Form2();
            naziv.Show();
        }
    }
}

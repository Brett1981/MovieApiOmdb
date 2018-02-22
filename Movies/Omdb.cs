using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http.Headers;


namespace Movies
{
    class Omdb
    {
        const string omdbUrl = "http://www.omdbapi.com/?"; // Base omdb api URL
        public string omdbKey= "c86272c6"; // A key is required for poster images.
        public Search newMovie; // Initialize movie object
        public Search newMovieList; // Initialize movie list object

        public async Task<Search> GetMovie(string query, string apiKey = "c86272c6")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(omdbUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(omdbUrl + "t=" + query);
                if (response.IsSuccessStatusCode)
                {
                    newMovie = await response.Content.ReadAsAsync<Search>();
                    return newMovie;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<Search> GetMovieList(string query, string apiKey = "c86272c6")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(omdbUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(omdbUrl + "s=" + query);
                if (response.IsSuccessStatusCode)
                {
                    newMovieList = await response.Content.ReadAsAsync<Search>();
                    return newMovieList;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

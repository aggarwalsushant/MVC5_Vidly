using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyModels.Models;
using Vidly.ViewModels;

namespace Vidly.UtilityData
{
    public class DataSimulator
    {
        static IEnumerable<Customer> customerList;
        static IEnumerable<Movie> movieList;


        public static Customer GetCustomerData(int id)
        {
            return GetAllCustomers().Where(x => x.Id == id).FirstOrDefault();

        }

        public static void CreateAllCustomers()
        {
            if (customerList == null)
            {
                customerList = new List<Customer>()
                {
                    new Customer() {Id=1, Name="Vijay" },
                    new Customer() {Id=2, Name="Ravi" },
                    new Customer() {Id=3, Name="Lavkush" },
                    new Customer() {Id=4, Name="Harshit" },

                };
            }
        }

        public static IEnumerable<Customer> GetAllCustomers()
        {
            if (customerList == null)
            {
                CreateAllCustomers();
            }
            return customerList;
        }

        public static void CreateAllMovies()
        {
            if (movieList == null)
            {
                movieList = new List<Movie>()
                {
                    new Movie() {Id=1, Name="12 strong" },
                    new Movie() {Id=2, Name="Fast and Furious" },
                    new Movie() {Id=3, Name="The Raid" },
                    new Movie() {Id=4, Name="Ankur Arora Murder Case" },
                };
            }
        }

        public static IEnumerable<Movie> GetAllMovies()
        {
            if (movieList == null)
            {
                CreateAllMovies();
            }
            return movieList;
        }


    }
}
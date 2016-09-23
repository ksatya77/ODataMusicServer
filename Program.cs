using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Cache.Configuration;
using ODataMusicServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataMusicServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration
            {
                BinaryConfiguration = new BinaryConfiguration(typeof(Movie), typeof(StarRating),typeof(Person)),
                JvmOptions = new List<string> { "-Xms512m", "-Xmx1024m" }
            };

            using (var ignite = Ignition.Start(cfg))
            {
                var cache = ignite.GetOrCreateCache<int, Movie>(new CacheConfiguration
                {
                    Name = "myMusicCache",
                    QueryEntities = new[]
                    {
                        new QueryEntity(typeof(int), typeof(Movie))

                    }
                });

                cache.Put(1, new Movie { Id = 1, Rating = StarRating.FiveStar, ReleaseDate = new DateTime(2015, 10, 25), Title = "StarWars - The Force Awakens", Director = new Person { FirstName = "J.J.", LastName = "Abrams" } });
                cache.Put(2, new Movie { Id = 2, Rating = StarRating.FourStar, ReleaseDate = new DateTime(2015, 5, 15), Title = "Mad Max - The Fury Road", Director = new Person { FirstName = "George", LastName = "Miller" } });
                Console.ReadLine();
            }
        }
    }
}

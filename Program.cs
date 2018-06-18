using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Artist FromMtVernon = Artists.Where(artist => artist.Hometown == "Mount Vernon").Single(); 
            Console.WriteLine($"{FromMtVernon.ArtistName}, {FromMtVernon.Age}");
            //Who is the youngest artist in our collection of artists?
            Artist youngest = Artists.OrderBy(artist => artist.Age).First();
            Console.WriteLine($"the youngest Artist:{youngest.ArtistName}, {youngest.Age}");

            //Display all artists with 'William' somewhere in their real name
            List<Artist> ContWilliams = Artists.Where(artist => artist.RealName.Contains("William")).ToList();
            Console.WriteLine("All Williams:");
            foreach(var artist in ContWilliams){
                Console.WriteLine(artist.ArtistName + artist.RealName);
            }

            //Display the 3 oldest artist from Atlanta
            List<Artist> Oldest = Artists.Where(artist =>artist.Hometown =="Atlanta").OrderByDescending(artist => artist.Age).Take(3).ToList();
            Console.WriteLine("3 oldest artist:");
            foreach(var artist in Oldest){
                Console.WriteLine(artist.ArtistName + artist.Age);
            }   
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            List<string> NotInNewYork = Artists.Join(Groups, artist => artist.GroupId, group => group.Id, (artist, group)=>{ artist.Group = group; return artist;})
            .Where(artist => artist.Hometown != "New York City" && artist.Group != null)
            .Select(artist => artist.Group.GroupName)
            .Distinct().ToList();
            Console.WriteLine("Groups that are not From New York");
            foreach( var group in NotInNewYork){
                Console.WriteLine(group);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            Group WuTangClan = Groups.Where(group => group.GroupName == "Wu-Tang Clan")
                                .GroupJoin(Artists, group => group.Id,
                                 artist => artist.GroupId,
                                (group, artists) => { group.Members = artists.ToList(); return group;})
                                .Single();
                                Console.WriteLine("Artist members of the Wu-Tang Clan:");
                                foreach(var artist in WuTangClan.Members){
                                    Console.WriteLine(artist.ArtistName);
                                }
         


        }
    }
}

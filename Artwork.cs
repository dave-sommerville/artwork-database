﻿namespace Artwork_Database
{
    public class Artwork
    {
        public string Title;
        public string Artist;
        public string Year; //  May want to parse into an integer instead 
        public string Medium;

        public Artwork(string title, string artist, string year, string medium)
        {
            Title = title;
            Artist = artist;
            Year = year;
            Medium = medium;
        }

        public string PrintArtwork()
        {
            string newLine = $"{Title} | {Artist} | {Year} | {Medium}";
            return newLine;
        }
    }
}

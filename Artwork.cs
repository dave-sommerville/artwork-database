namespace Artwork_Database
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
        public static Artwork CreateObject(string[] splitInput)
        {
            if (splitInput.Length != 4)
            {
                Console.WriteLine("Array must have exactly four elements"); // Will want to convert to a throw error 
            }
            return new Artwork(splitInput[0], splitInput[1], splitInput[2], splitInput[3]);
        }
        public static string PrintArtwork(Artwork art)
        {
            string newLine = $"{art.Title} | {art.Artist} | {art.Year} | {art.Medium}";
            return newLine;
        }
    }
}

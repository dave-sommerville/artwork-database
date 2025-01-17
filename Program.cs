using System.Reflection;

namespace Artwork_Database
{
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Starry Night | Vincent van Gogh | 1889 | Painting"; // Just for formating 

            bool Is_Running = true;  

            string[] fileInput = new string[3];
            Artwork[] unsortedArtArray = CollectUnsortedArt(fileInput);
            Artwork[] sortedByTitle = InitialSorts(unsortedArtArray, "Title");
            Artwork[] sortedByArtist = InitialSorts(unsortedArtArray, "Artist");
            Artwork[] sortedByYear = InitialSorts(unsortedArtArray, "Year");
            

            while (Is_Running)
            {
                Console.WriteLine("1) Add Artwork 2) Search Artworks 3) Exit Program");
                int choice = PrintMenu(2);
                if (choice == 1)
                {
                    string[] userInputArt = UserEnteredArtwork();
                    Artwork newArtwork = CreateObject(userInputArt);
                    ObjectInsertAllArrays(newArtwork, sortedByTitle, sortedByArtist, sortedByYear);
                } else if (choice == 2)
                {
                    bool is_Searching = true;
                    while (is_Searching)
                    {
                        Console.WriteLine("Select your search criteria:\n1) Title 2) Artist 3) Year 4) Return to Previous Menu");
                        int searchChoice = PrintMenu(4);
                        if (searchChoice >= 1 && searchChoice < 3)
                        {
                            CriteriaOptions criteria = (CriteriaOptions)searchChoice;
                        } else
                        {
                            is_Searching = false;
                        }
                    }
                } else if (choice == 3)
                {
                    Is_Running = false;
                }

            }
        }
            //  User Selection Methods 
        private static int PrintMenu(int options)
        {
            int intDecision;
            while (true)
            {
                string decision = Console.ReadLine();

                if (int.TryParse(decision, out intDecision))
                {
                    if (intDecision >= 0 && intDecision <= options)

                    {
                        return intDecision;
                    }
                    else
                    {
                        Console.WriteLine("Enter a number between 1- 2");
                    }
                }
                else
                {
                    Console.WriteLine("That is an invalid character.");
                }

            }
        }
        public enum CriteriaOptions
        {
            Title = 1,
            Artist = 2,
            Year = 3
        }

        //  User Artwork input
        public static string[] UserEnteredArtwork()
        {
            string[] propertyArray = new string[4];
            Console.WriteLine("Please enter Title:");
            propertyArray[0] = Console.ReadLine();
            Console.WriteLine("Please enter Artist:");
            propertyArray[1] = Console.ReadLine();
            Console.WriteLine("Please enter Year:");
            propertyArray[2] = Console.ReadLine();
            Console.WriteLine("Please enter Medium:");
            propertyArray[3] = Console.ReadLine();
            return propertyArray;
        }


        //  Artwork Object Creation
        public static string[] SplitLine(string line)
        {
            string[] splitText = line.Split("|");
            for (int i = 0; i < splitText.Length; i++)
            {
                splitText[i] = splitText[i].Trim();
            }
            return splitText;
        }
        public static Artwork CreateObject(string[] splitInput)
        {
            if (splitInput.Length != 4)
            {
                Console.WriteLine("Array must have exactly four elements"); // Will want to convert to a throw error 
            }
            Artwork newArtwork = new Artwork(splitInput[0], splitInput[1], splitInput[2], splitInput[3]);
            return newArtwork;
        }

        //  Private Helper methods for deciding sorting order 
        public static string ChooseProperty(Artwork targetArt, string targetProperty)
        {
            switch (targetProperty)
            {
                case "Title":
                    return targetArt.Title;
                case "Artist":
                    return targetArt.Artist;
                case "Year": 
                    return targetArt.Year;
                default:
                    return "Property not found";
            }
        }

        public static bool NextIndex(string newString, string oldString)
        {
            int compResult = String.Compare(newString, oldString, true);
            if (compResult < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //  Main unsorted collection
        public static Artwork[] CollectUnsortedArt(string[] linesInput)
        {
            Artwork[] unsortedCollection = new Artwork[linesInput.Length];
            for (int i = 0; i < linesInput.Length; i++)
            {
                string[] lineProcessing = SplitLine(linesInput[i]);
                unsortedCollection[i] = CreateObject(lineProcessing);
            }
            return unsortedCollection;
        }

        //  SORTING
        //  Initial Sorting
        public static Artwork[] InitialSorts(Artwork[] unsortedInput, string targetProperty)
        {
            Artwork[] sortedArray = new Artwork[unsortedInput.Length];
            for (int i = 0; i < unsortedInput.Length; i++)
            {
                ObjectInsert(unsortedInput[i], sortedArray, targetProperty);
            }
            return sortedArray;
        }
        //  Array 
        public static void ObjectInsertAllArrays(Artwork newObj, Artwork[] byTitle, Artwork[] byArtist, Artwork[] byYear)
        {
            ObjectInsert(newObj, byTitle, "Title");
            ObjectInsert(newObj, byArtist, "Artist" );
            ObjectInsert(newObj, byYear, "Year");

        }

        public static Artwork[] ObjectInsert(Artwork newObj, Artwork[] artArray, string targetProperty) 
        {
            Artwork[] newArray = new Artwork[artArray.Length + 1];
            for (int i = 0, j = 0; i < newArray.Length; i++)
            {
                if (NextIndex(ChooseProperty(newObj, targetProperty), ChooseProperty(artArray[i], targetProperty))) 
                {
                    newArray[i] = newObj;
                } else
                {
                    newArray[i] = artArray[j];
                    j++;
                }
            }
            return newArray;
        }








        //  FILE READER CLASS                                                           x
        //  Accepting the file as an array of lines                                     x
        //  File data has a for loop to insert sort every object into each array        x
        //  A snipper method (with trim)                                                x
        //  Passes to Artwork method to construct objects                               x
        //  Identifier of needed property                                               x
        //  Comparision method to return sorting instruction                            x
        //  Insertion method to adjust the array                                        x
        //  Binary search method (call upon comparision method) 
        //  User prompt to array method                                                 x
        //  User menu to choose sorting criteria                                        x

 


    }
}


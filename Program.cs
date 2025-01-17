using System.Reflection;

namespace Artwork_Database
{
            //Importing File
            //  Add datetime property to artwork  
            //  Change the year to an int check (or limit the entry to four digits?) 
            //  Add new art into master array
            //  Print arrays 
            //  Export methods 
            //  Error correction and explanation 
            //  Ensure encapsulation
            //  Interfaces 
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Starry Night | Vincent van Gogh | 1889 | Painting"; // Just for formating 

            bool Is_Running = true;  

            string[] fileInput = new string[3];
            fileInput[0] = "Starry Night | Vincent van Gogh | 1869 | Painting";
            fileInput[1] = "highest | Gogh | 1989 | Painting";
            fileInput[2] = "zbottom | Buttface van Gogh | 1889 | Painting";

            int masterArtCount = 0;
            Artwork[] masterArtArray = CollectUnsortedArt(fileInput);

            Artwork[] sortedByTitle = ObjSort(masterArtArray, "title", masterArtCount);
            Artwork[] sortedByArtist = ObjSort(masterArtArray, "artist", masterArtCount);
            Artwork[] sortedByYear = ObjSort(masterArtArray, "year", masterArtCount);

            while (Is_Running)
            {
                Console.WriteLine("1) Add Artwork 2) Search Artworks 3) Exit Program");
                int choice = PrintMenu(2);
                if (choice == 1)
                {
                    string[] userInputArt = UserEnteredArtwork();
                    Artwork newArtwork = CreateObject(userInputArt);
                    ObjInsert(sortedByTitle, newArtwork, masterArtCount, "title");
                    ObjInsert(sortedByArtist, newArtwork, masterArtCount, "artist");
                    ObjInsert(sortedByYear, newArtwork, masterArtCount, "year");


                }
                else if (choice == 2)
                {
                    bool is_Searching = true;
                    while (is_Searching)
                    {
                        Console.WriteLine("Select your search criteria:\n1) Title 2) Artist 3) Year 4) Return to Previous Menu");
                        int searchChoice = PrintMenu(4);

                        switch (searchChoice)
                        {
                            case 1:
                                PrintSearchResult(SearchByProperty(sortedByTitle, "title"));
                                break;
                            case 2:
                                PrintSearchResult(SearchByProperty(sortedByArtist, "artist"));
                                break;
                            case 3:
                                PrintSearchResult(SearchByProperty(sortedByYear, "year"));
                                break;
                            case 4:
                                is_Searching = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Entry");
                                break;
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



        static Artwork[] ObjSort(Artwork[] masterArray, string targetProperty, int count)
        {
            Artwork[] sortedArray = new Artwork[masterArray.Length];
            count = 0;
            for (int i=0; i < masterArray.Length; i++)
            {
                ObjInsert(sortedArray, masterArray[i], count, targetProperty);
                count++;
            }
            return sortedArray;
        }

        static string ChooseProperty(Artwork artwork, string targetProperty)
        {
            if (artwork == null)
            {
                throw new ArgumentNullException(nameof(artwork), "Artwork cannot be null or empty");
            }
            targetProperty = targetProperty.Trim().ToLower();

            if (targetProperty == "title")
            {
                return artwork.Title;
            } else if(targetProperty == "artist") {
                return artwork.Artist;
            } else if (targetProperty == "year")
            {
                return artwork.Year;
            } else
            {
                throw new ArgumentException($"Invalid property name: '{targetProperty}'. Valid options are 'Title', 'Artist', or 'Year'.");
            }
        }
        
        static void ObjInsert(Artwork[] sortedArray, Artwork newArtwork, int count, string targetProperty)
        {
            for (int i = count; i > 0;  i--)
            {
                string currentProperty = ChooseProperty(sortedArray[i - 1], targetProperty);
                string newProperty = ChooseProperty(newArtwork, targetProperty);
                if (string.Compare(currentProperty, newProperty, true) > 0)
                {
                    sortedArray[i] = sortedArray[i - 1];
                } else
                {
                    sortedArray[i] = newArtwork;
                    return;
                }
            }
            sortedArray[0] = newArtwork;
        }
        public static Artwork SearchByProperty(Artwork[] sortedArray, string targetProperty)
        {
            Console.WriteLine("Please enter your inquiry:");
            string userInput = Console.ReadLine().Trim().ToLower();

            int left = 0;
            int right = sortedArray.Length - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;

                // Retrieve the property value for the current middle item
                string propertyCheck = ChooseProperty(sortedArray[middle], targetProperty).ToLower();

                // Compare the user input with the current property value
                int comparison = string.Compare(propertyCheck, userInput, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    // Match found
                    return sortedArray[middle];
                }
                else if (comparison < 0)
                {
                    // User input is greater than the current property value (search right half)
                    left = middle + 1;
                }
                else
                {
                    // User input is less than the current property value (search left half)
                    right = middle - 1;
                }
            }

            // No match found
            Console.WriteLine("Selection not found.");
            return null;
        }
        public static void PrintSearchResult(Artwork artwork)
        {
            if (artwork != null)
            {
                Console.WriteLine("Search result:");
                Console.WriteLine(artwork.PrintArtwork());
            }
            else
            {
                Console.WriteLine("No matching artwork found.");
            }
        }

    }
}


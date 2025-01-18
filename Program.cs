using System.Reflection;
using System.Runtime.CompilerServices;

namespace Artwork_Database
{ 
            //  Export methods 
            //  Error correction and explanation 
            //  Add entry validation 
            //  Notes (The year validation, interface on main, choosing the three properties, adding  date and time, adding export, error catching and validation )
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Starry Night | Vincent van Gogh | 1889 | Painting";

            bool Is_Running = true;
            string filePath = "art_collection_basic.txt";
            string[] fileInput = FileReader.ReadFile(filePath);

            Artwork[] masterArtArray = CollectUnsortedArt(fileInput);
            int masterArtCount = 0;

            Artwork[] sortedByTitle = ObjSort(masterArtArray, "title", masterArtCount);
            Artwork[] sortedByArtist = ObjSort(masterArtArray, "artist", masterArtCount);
            Artwork[] sortedByYear = ObjSort(masterArtArray, "year", masterArtCount);

            while (Is_Running)
            {
                Console.WriteLine("1) Add Artwork 2) Search Artworks 3) Display Collections 4) Exit Program");
                int choice = PrintMenu(4);
                if (choice == 1)
                {
                    string[] userInputArt = UserEnteredArtwork();
                    Artwork newArtwork = CreateObject(userInputArt);
                    ObjInsert(sortedByTitle, newArtwork, masterArtCount, "title");
                    ObjInsert(sortedByArtist, newArtwork, masterArtCount, "artist");
                    ObjInsert(sortedByYear, newArtwork, masterArtCount, "year");
                    masterArtArray = MasterInsert(masterArtArray, newArtwork, filePath);

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
                    bool is_Displaying = true;
                    while (!is_Displaying)
                    {
                        Console.WriteLine("What order would you like to view inventory?\n1) By Title 2) By Artist 3) By Year 4) Return to previous menu");
                            int arrayChoice = PrintMenu(4); //  I would add print master array here 
                            switch (arrayChoice)
                            {
                                case 1:
                                    string[] artworksByTitle = PrintArray(sortedByTitle);
                                    Console.WriteLine(artworksByTitle);
                                    break;
                                case 2:
                                    string[] artworksByArtist = PrintArray(sortedByArtist);
                                    Console.WriteLine(artworksByArtist);
                                    break;
                                case 3:
                                    string[] artworksByYear = PrintArray(sortedByYear);
                                    Console.WriteLine(artworksByYear);
                                    break;
                                case 4:
                                    is_Displaying = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid Entry");
                                    break;
                            }
                    }
                } else if (choice == 4)
                {
                    Is_Running = false;
                }

            }
        }
            //  User Selection Methods 
        private static int PrintMenu(int options)
        {
            int intDecision;
            bool isValid;
            do
            {
                string decision = Console.ReadLine();
                isValid = int.TryParse(decision, out intDecision) && intDecision >= 1 && intDecision <= options;
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (!isValid);
            return intDecision;
        }

        //  User Artwork input
        private static string[] UserEnteredArtwork()
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
        private static string[] SplitLine(string line)
        {
            string[] splitText = line.Split("|");
            for (int i = 0; i < splitText.Length; i++)
            {
                splitText[i] = splitText[i].Trim();
            }
            return splitText;
        }
        private static Artwork CreateObject(string[] splitInput)
        {
            if (splitInput.Length != 4)
            {
                Console.WriteLine("Array must have exactly four elements"); // Will want to convert to a throw error 
            }
            Artwork newArtwork = new Artwork(splitInput[0], splitInput[1], splitInput[2], splitInput[3]);
            return newArtwork;
        }


        private static Artwork[] CollectUnsortedArt(string[] linesInput)
        {
            Artwork[] unsortedCollection = new Artwork[linesInput.Length];
            for (int i = 0; i < linesInput.Length; i++)
            {
                string[] lineProcessing = SplitLine(linesInput[i]);
                unsortedCollection[i] = CreateObject(lineProcessing);
            }
            return unsortedCollection;
        }



        private static Artwork[] ObjSort(Artwork[] masterArray, string targetProperty, int count)
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

        private static string ChooseProperty(Artwork artwork, string targetProperty)
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
        
        private static void ObjInsert(Artwork[] sortedArray, Artwork newArtwork, int count, string targetProperty)
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

        private static Artwork[] MasterInsert(Artwork[] masterArray, Artwork newObj, string filePath)
        {
            Artwork[] newArtArray = new Artwork[masterArray.Length + 1];
            newArtArray[masterArray.Length] = newObj;
            AppendToMasterTxt(filePath, newObj);
            return newArtArray;
        }
        private static void AppendToMasterTxt(string filePath, Artwork newObj)
        {
            
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(newObj.PrintArtwork());
                }
            } catch (Exception e)
            {
                Console.WriteLine($"An error occurred while appending to the file: {e.Message}");
            }
        }
        private static Artwork SearchByProperty(Artwork[] sortedArray, string targetProperty)
        {
            Console.WriteLine("Please enter your inquiry:");
            string userInput = Console.ReadLine().Trim().ToLower();

            int left = 0;
            int right = sortedArray.Length - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;
                string propertyCheck = ChooseProperty(sortedArray[middle], targetProperty).ToLower();
                int comparison = string.Compare(propertyCheck, userInput, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return sortedArray[middle];
                }
                else if (comparison < 0)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }

            Console.WriteLine("Selection not found.");
            return null;
        }
        private static void PrintSearchResult(Artwork artwork)
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
        private static string[] PrintArray(Artwork[] artArray)
        {
            string[] arrayDisplay = new string[artArray.Length];
            for (int i = 0; i < artArray.Length; i++)
            {
                arrayDisplay[i] = artArray[i].PrintArtwork();
            }
            return arrayDisplay;
        }
    }
}


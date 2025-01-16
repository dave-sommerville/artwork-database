namespace Artwork_Database
{
    public class Program
    {
        static void Main(string[] args)
        {
            string test = "Starry Night | Vincent van Gogh | 1889 | Painting";
        }
        public string[] SplitLine(string line)
        {
            string[] splitText = line.Split("|");
            for (int i = 0; i < splitText.Length; i++)
            {
                splitText[i] = splitText[i].Trim();
            }
            return splitText;
        }
        //  Should definitely be private, possibly rework to work with a binary search 
        public bool InsertAtNextIndex(string newString, string oldString)
        {
            int compResult = String.Compare(newString, oldString, StringComparison.OrdinalIgnoreCase);
            if (compResult < 0 )
            {
                return false;
            } else
            {
                return true;
            }
        }
                                                                                //  Insert index will be a string with the value of the object targeted (Into its own array)
        public string[] ObjectInsert(string newString, string[] stringArray, int insertIndex) 
        {
            string[] newArray = new string[stringArray.Length + 1];
            for (int i = 0, j = 0; i < newArray.Length; i++)
            {
                if (i == insertIndex)
                {
                    newArray[i] = newString;
                } else
                {
                    newArray[i] = stringArray[j];
                    j++;
                }
            }
            return newArray;
        }


    //  Accepting the file as an array of lines 
    //  A snipper method (with trim)                                                x
    //  Passes to Artwork method to construct objects                               x
    //  A multiplier method to pass the object insert method to every array 
    //  Comparision method to return sorting instruction                            x
    //  Insertion method to adjust the array                                        x
    //  File data has a for loop to insert sort every object into each array 
    //  Menu interface will be needed 
    //  User prompt to array method 
    //  Binary search method (call upon comparision method) 
    //  User menu to choose sorting criteria 
    //  Output options 

    //  Main issues with this are memory 
    //  Also, should subcategories be sorted in some way? 


}
}


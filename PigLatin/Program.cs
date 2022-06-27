string[] vowellist = { "A", "a", "E", "e", "I", "i", "O", "o", "U", "u", "Y", "y" };
string[] numsymbollist = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "|", "~", "`"};
string[] punctlist = { ".", ",", "?", "!" };



static string FirstLetter(string inputword)
{
    string firststring = Convert.ToString(inputword[0]);
    return firststring;
}

static bool CharCheck(string inputword1, string[] vlist)
{
    foreach (string vowel in vlist)
    {
        if (FirstLetter(inputword1).Contains(vowel))
        {
            return true;
            break;
        }
    }
    return false;
}

static string FindCase(string word)
{
    if (word == word.ToLower())
    {
        return "lower";
    }
    else if (word == word.ToUpper())
    {
        return "upper";
    }
    else if (FirstLetter(word) == FirstLetter(word).ToUpper() && word.Substring(1) == word.Substring(1).ToLower())
    {
        return "title";
    }
    else
    {
        return "unknown";
    }
}

//Program run loop
while (true)
{
    string unfiltered = "";
    string translated = "";

    // check if there is actually text
    while (unfiltered == "")
    {
        Console.WriteLine("Please enter some words: ");
        unfiltered = Console.ReadLine();
    }

    string[] words = unfiltered.Split(' ');

    //Loop over each word (separated by spaces)
    foreach (string wordraw in words)
    {
        bool firstlettervowel = false;
        bool symbolinside = false;
        string lastchar = Convert.ToString(wordraw.Last());
        string word = "";
        string popped = "";
        string punct = "";
        
        //Remove punctuation, set variable to hold the punctuation for later
        if (CharCheck(lastchar, punctlist) == true)
        {
            //following only checks last character for punctuation. the loop below can take more than one punct. mark at the end of the word
            //word = wordraw.Substring(0, wordraw.Length - 1);
            //punct = lastchar;

            for (int i = wordraw.Length - 1; i > 0; i--)
            {
                popped = wordraw.Substring(i);

                if (CharCheck(popped, punctlist) == false)
                {
                    punct = wordraw.Substring(i + 1);
                    word = wordraw.Substring(0, i+1);
                    break;
                }
            }
        }
        else
        {
            word = wordraw;
        }

        string wordcase = FindCase(word);

        //Check if symbol inside of word (then don't translate it)
        for (int i = 0; i < word.Length; i++)
        {
            popped = word.Substring(i, word.Length - i);

            if (CharCheck(popped, numsymbollist) == true)
            {
                translated = translated + word+ punct;
                symbolinside = true;
            }
        }
        //Check if first letter is vowel
        if (CharCheck(word, vowellist) == true && symbolinside == false)
        {
            firstlettervowel = true;

            //Check case of word
            if (wordcase == "upper")
            {
                translated = translated + word.Substring(1).ToUpper() + "WAY"+punct +" ";
            }
            else if (wordcase == "title")
            {
                translated = translated + word.Substring(1, 2).ToUpper() + word.Substring(2).ToLower() + "way" + punct + " ";
            }
            else
            {
                translated = translated + word.Substring(1) + "way" + punct + " ";
            }
        }

        //If first letter isn't vowel, loop over substrings to see where the first vowel is then apply rule
        if (firstlettervowel == false && symbolinside == false)
        {
            for (int i = 1; i < word.Length; i++)
            {
                string front = word.Substring(i, word.Length - i);

                if (CharCheck(front, vowellist) == true)
                {
                    string back = word.Substring(0, i);

                    if (wordcase == "upper")
                    {
                        translated = translated + front.ToUpper() + back.ToUpper() + "AY" + punct + " ";
                    }
                    else if (wordcase == "title")
                    {
                        translated = translated + front.Substring(0,1).ToUpper() + front.Substring(1).ToLower() + back.ToLower() + "ay" + punct + " ";
                    }
                    else
                    {
                        translated = translated + front + back + "ay" + punct + " ";
                    }
                    break;
                }
            }
        }
    }
    //Output translated words
    Console.WriteLine(translated);

    //Ask if want to continue
    Console.WriteLine("Would you like to translate something else? y/n");
    string keeprunning = Console.ReadLine();
    if (keeprunning.ToLower().Trim() != "y")
    {
        break;
    }
}
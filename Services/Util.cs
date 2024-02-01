using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;

namespace Lab1sharp.Services
{
    public static class Util
    {
        private static Dictionary<string, string> codeBook_fixed_encode = new Dictionary<string, string>();
        private static Dictionary<string, string> codeBook_ascii_encode = new Dictionary<string, string>();
        private static Dictionary<string, string> codeBook_fixed_decode = new Dictionary<string, string>();
        private static Dictionary<string, string> codeBook_ascii_decode = new Dictionary<string, string>();
        public static string Encode(string input)
        {
            input = input.ToLower();
            List<string> wordAndSpaces = SplitIntoWordsAndSpaces(input);
            List<string> encodeContainer = new List<string>();
            foreach(string s in wordAndSpaces)
            {

                if(codeBook_fixed_encode.ContainsKey(s))
                {
                    encodeContainer.Add(codeBook_fixed_encode[s]);
                }
                else
                {
                    foreach(char c in s)
                    {
                        if(c.ToString() == " ")
                        {
                            encodeContainer.Add(Random5DigitsFix());
                        }

                        if(c.ToString() == "r")
                        {
                            encodeContainer.Add(RandomOdd3Digits());
                        }

                        if(c.ToString() == "l")
                        {
                            encodeContainer.Add(RandomEven3Digits());
                        }
                        if(c.ToString() != " " && c.ToString() != "r" && c.ToString() != "l")
                        {
                            if((int)c <32 || ((int)c >= 65 && (int)c <=90) || (int)c >= 127)
                            {
                                return "error, character outside the ASCII range, input should be regular letters on the keyboard!";
                            }
                            string key = ((int)c).ToString();
                            encodeContainer.Add(codeBook_ascii_encode[key]);
                        }           
                    }
                }
            }

            string encodedStringRaw = string.Join(" ", encodeContainer);

            //next step, add random null symbol
            Random random = new Random();
           
            // Split the sentence into words and store in a list
            List<string> words = new List<string>(encodedStringRaw.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            // Calculate 30% of the word count
            int numberOfAsToInsert = (int)Math.Ceiling(words.Count * 0.3);

            for (int i = 0; i < numberOfAsToInsert; i++)
            {
                int randomPosition;
                do
                {
                    // Choose a random position in the list
                    randomPosition = random.Next(words.Count + 1); // +1 because we can also insert at the end of the list

                } while (IsAdjacentToA(words, randomPosition));

                // Insert ' a ' at the random position
                //words.Insert(randomPosition, "a");
                words.Insert(randomPosition, RandomNeg3Digits());
            }

            // Join the words back into a sentence
            string modifiedEncodedText = string.Join(" ", words);
            return modifiedEncodedText;
        }
        public static string Decode(string input) 
        {
            System.Console.WriteLine(input);
            //List<string> data = SplitIntoWordsExcludingA(input);
            List<string> data = SplitIntoWordsExcludingNegValue(input);
            System.Console.WriteLine("data Count: " + data.Count);
            List<string> decodedContainer = new List<string>();
            string tempWord = "";
            foreach(string s in data)
            {
                System.Console.WriteLine("tempword: " + tempWord);
                //step 1 check fixed dictionary
                if(codeBook_fixed_decode.ContainsKey(s))
                {
                    System.Console.WriteLine("fixed codebook found the key");
                    string value = codeBook_fixed_decode[s];
                    System.Console.WriteLine("fixed codebook value: " + value);
                    decodedContainer.Add(codeBook_fixed_decode[s]);
                }
                else
                {
                    if(int.TryParse(s, out int number) && number > 10000) //found a space
                    {
                        System.Console.WriteLine("found a space");
                        if(tempWord != "") //means existing word contruction ongoing
                        {
                            decodedContainer.Add(tempWord); //save the word
                            tempWord = ""; //reset
                        }
                        decodedContainer.Add(" "); //because found space, then add space
                        
                    }

                    if(codeBook_ascii_decode.ContainsKey(s)) //if is not a number
                    {
                        System.Console.WriteLine("a regular letter found in chart");
                        int asciiCode = int.Parse(codeBook_ascii_decode[s]);
                        tempWord += (char)asciiCode;
                    }
                    if (int.TryParse(s, out int number2) && number2 <= 1000)
                    {
                        System.Console.WriteLine("found homophonic word");
                        if(number2 % 2 == 0 && number2 > 0) //greater than zero make sure not the null symbol
                        {
                            tempWord += 'l';
                        }
                        if(number2 % 2 != 0 && number2 > 0)
                        {
                            tempWord += 'r';
                        }
                    }
                }
                
            }

            if(tempWord != "")
            {
                decodedContainer.Add(tempWord);
            }

            //convert and export
            string decodedText = string.Join("", decodedContainer);
            return decodedText;
        }
        static bool IsAdjacentToA(List<string> words, int position)
        {
            // Check if the position is adjacent to another 'a'
            if (position > 0 && words[position - 1] == "a")
            {
                return true;
            }
            if (position < words.Count && words[position] == "a")
            {
                return true;
            }
            return false;
        }
        private static List<string> SplitIntoWordsAndSpaces(string input)
        {
            List<string> result = new List<string>();
            string currentWord = "";

            foreach(char ch in input)
            {
                if(ch != ' ')
                {
                    currentWord += ch;
                }
                else
                {
                    if(currentWord != "")
                    {
                        result.Add(currentWord);
                        currentWord = "";
                    }
                    result.Add(" ");
                }
            }
            //if last character in the input is not a space. then add the last word
            if(currentWord != "")
            {
                result.Add(currentWord);
            }

            return result;
        }

        static List<string> SplitIntoWordsExcludingA(string input)
        {
            List<string> result = new List<string>();
            string currentWord = "";

            foreach (char ch in input)
            {
                if (ch != ' ')
                {
                    currentWord += ch;
                }
                else
                {
                    if (currentWord != "" && currentWord != "a")
                    {
                        result.Add(currentWord);
                    }
                    currentWord = "";
                }
            }

            // Add the last word if it's not empty and not 'a'
            if (currentWord != "" && currentWord != "a")
            {
                result.Add(currentWord);
            }

            return result;
        }

        static List<string> SplitIntoWordsExcludingNegValue(string input)
        {
            List<string> result = new List<string>();
        string currentWord = "";
        bool isNegativeNumber = false;

        foreach (char ch in input)
        {
            if (ch != ' ')
            {
                // Check if the character is the start of a negative number
                if (ch == '-' && currentWord == "")
                {
                    isNegativeNumber = true;
                }
                else if (!char.IsDigit(ch) && isNegativeNumber)
                {
                    // If a non-digit character is encountered, it's not a negative number
                    isNegativeNumber = false;
                }

                currentWord += ch;
            }
            else
            {
                if (currentWord != "" && !isNegativeNumber)
                {
                    result.Add(currentWord);
                }
                currentWord = "";
                isNegativeNumber = false;
            }
        }

        // Add the last word if it's not empty and not a negative number
        if (currentWord != "" && !isNegativeNumber)
        {
            result.Add(currentWord);
        }

        return result;
        }
        private static void ReadJson()
        {
            string jsonString = File.ReadAllText("Codebook.JSON");
            JObject jObject = JObject.Parse(jsonString);
            JObject namesObject = (JObject)jObject["names"]!;
            JObject placesObject = (JObject)jObject["places"]!;
            JObject setsObject = (JObject)jObject["sets"]!;
            JArray chartObjArray = (JArray)jObject["chart"]!;
            
            //load names into dictionary
            foreach(JProperty property in namesObject.Properties())
            {
                codeBook_fixed_encode.Add(property.Value.ToString(), property.Name);
                codeBook_fixed_decode.Add(property.Name, property.Value.ToString());
            }

            //load places into dictionary
            foreach(JProperty property in placesObject.Properties())
            {
                codeBook_fixed_encode.Add(property.Value.ToString(), property.Name);
                codeBook_fixed_decode.Add(property.Name, property.Value.ToString());
            }

            //load sets into dictionary
            foreach(JProperty property in setsObject.Properties())
            {
                codeBook_fixed_encode.Add(property.Value.ToString(), property.Name);
                codeBook_fixed_decode.Add(property.Name, property.Value.ToString());
            }

            //load ascii look up chart into dictionary
            foreach(JObject item in chartObjArray)
            {
                codeBook_ascii_encode.Add(item["ascii"]!.ToString(), item["value"]!.ToString());
                codeBook_ascii_decode.Add(item["value"]!.ToString(), item["ascii"]!.ToString());
            }

        }
        public static void InitializeOnStartUp()
        {
            ReadJson();
        }
        public static void test()
        {
            foreach(KeyValuePair<string, string> item in codeBook_fixed_encode)
            {
                Console.WriteLine("key: " + item.Key + "  value: " + item.Value);
            }
            foreach(KeyValuePair<string, string> item in codeBook_fixed_decode)
            {
                Console.WriteLine("key: " + item.Key + "  value: " + item.Value);
            }
            foreach(KeyValuePair<string, string> item in codeBook_ascii_encode)
            {
                Console.WriteLine("key: " + item.Key + "  value: " + item.Value);
            }
            foreach(KeyValuePair<string, string> item in codeBook_ascii_decode)
            {
                Console.WriteLine("key: " + item.Key + "  value: " + item.Value);
            }
        }
        private static string RandomOdd3Digits()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 500);
            int randOdd = randomNumber * 2 + 1;
            return randOdd.ToString(); 
        }
        private static string RandomEven3Digits()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 500);
            int randOdd = randomNumber * 2;
            return randOdd.ToString();
        }
        private static string Random5DigitsFix()
        {
            Random random = new Random();
            int randomNumber = random.Next(10001, 99999);
            return randomNumber.ToString();

        }
        private static string RandomNeg3Digits()
        {
            Random random = new Random();
            int randomNumber = random.Next(-499, -1);
            int randOdd = randomNumber * 2;
            return randOdd.ToString(); 
        }
    }
}

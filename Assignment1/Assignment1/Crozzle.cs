using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Assignment1
{
    /// <summary>
    /// In this class, contains a collection of methods:
    /// Open and validate wordlist file (obviously for determine the wordlist validation),
    /// Open and validate corzzle file (determining correctness of crozzle file),
    /// validate crozzle (to check openend crozzle in the crozzle area),
    /// scoring system (to count total score according to the difficulty level),
    /// crozzle field (create a copy of crozzle in crozzle area to be processed).
    /// </summary>
    class Crozzle
    {
        // Difficulty list
        const String easy = "EASY";
        const String medium = "MEDIUM";
        const String hard = "HARD";
        const String extreme = "EXTREME";

        // Validator variables
        private Boolean wordListFileValidator;
        private Boolean crozzleFileValidator;
        private Boolean crozzleValidator;

        private String[] gameProperties;
        private List<String> wordList;
        private List<String[]> crozzleRows;
        private List<String[]> crozzleColumns;
        private CrozzleWordData crozzleWordData;
 
        // Property requirements
        const int propertiesLength = 4;
        const int list = 1000; 
        const int minWords = 10; 
        const int maxWords = 1000; 
        const int minRows = 5;
        const int maxRows = 15;
        const int minColumns = 5;
        const int maxColumns = 15;

        #region constructors
        public Crozzle()
        {
            wordListFileValidator = false;
            crozzleFileValidator = false;
            crozzleValidator = false;
        }
        #endregion

        #region properties
        public Boolean WordlistFileValidator { get { return (wordListFileValidator); } }
        public Boolean CrozzleFileValidator { get { return (crozzleFileValidator); } }
        public Boolean CrozzleValidator { get { return (crozzleValidator); } }
        #endregion

        #region crozzle field
        private List<String[]> createCrozzleColumns()
        {
            int totalColumns;
            Int32.TryParse(gameProperties[2], out totalColumns);

            List<String[]> columns = new List<String[]>();

            int x = 0;
            //for (int x = 0; x < totalColumns; x++)
            if (x < totalColumns)
            {
                int j = 0;
                String[] column = new String[crozzleRows.Count];
                foreach (String[] row in crozzleRows)
                {
                    try
                    {
                        // something wrong on here.
                        column[j++] = row[x];
                    }

                    catch (Exception e)
                    {
                        column[j++] = "";
                    }
                }

                x++;
                columns.Add(column);
            }

            return (columns);
        }

        public override string ToString()
        {
            String str = "";

            foreach (String[] letters in crozzleRows)
            {
                foreach (String letter in letters)
                    if (letter.Length == 0)
                        str = str + " ";
                    else
                        str = str + letter;
                str = str + "\r\n";
            }
            return (str);
        }
        #endregion

        /// <summary>
        /// Score counter method with specific difficulty level criteria.
        /// </summary>
        /// <returns>it returns computed score from each difficulty levels.</returns>
        #region scoring system
        public int score()
        {
            int score = 0;

            // The difficulty chooser.
            if (gameProperties[3].Equals(easy, StringComparison.Ordinal))
                score = EasyScore();
            else if (gameProperties[3].Equals(medium, StringComparison.Ordinal))
                score = MediumScore();
            else if (gameProperties[3].Equals(hard, StringComparison.Ordinal))
                score = HardScore();
            else if (gameProperties[3].Equals(extreme, StringComparison.Ordinal))
                score = ExtremeScore();

            return (score);
        }

        private int EasyScore()
        {
            int score = 0;

            // Search for every letter in crozzle.
            foreach (String[] rowLetters in crozzleRows)
                foreach (String[] columnLetters in crozzleColumns)
                    score++;

            return (score);
        }

        private int MediumScore()
        {
            int score = 0;

            // Score requirements.
            int[] letterScores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 
                                        10, 11, 12, 13, 14, 15, 16, 17, 18, 
                                        19, 20, 21, 22, 23, 24, 25, 26 };

            // Compute the intersecting letter.
            List<Char> intersectingLetters = crozzleWordData.GetIntersectingLetters();
            foreach (Char letter in intersectingLetters)
                score = score + letterScores[(int)letter - (int)'A'];

            return (score);
        }

        private int HardScore()
        {
            int score = 0;
            const int wordScore = 10;

            // Score requirements.
            int[] letterScores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 
                                        10, 11, 12, 13, 14, 15, 16, 17, 18, 
                                        19, 20, 21, 22, 23, 24, 25, 26 };

            // Compute the intersecting letter.
            List<Char> intersectingLetters = crozzleWordData.GetIntersectingLetters();
            foreach (Char letter in intersectingLetters)
                //score = score + letterScores[(int)letter - (int)'A'];
                score = letterScores[(int)letter];

            // Increase the score for each word.
            score = score + crozzleWordData.Count * wordScore;

            return (score);
        }

        private int ExtremeScore()
        {
            int score = 0;
            int wordScore = 10;

            // Score requirements.
            int[] letterScores = { 1, 2, 2, 2, 1, 2, 2, 4, 1, 
                                   4, 4, 4, 4, 8, 1, 8, 8, 8, 
                                   16, 16, 1, 16, 32, 32, 32, 64 };

            // Compute the intersecting letter.
            List<Char> intersectingLetters = crozzleWordData.GetIntersectingLetters();
            foreach (Char letter in intersectingLetters)
                score = score + letterScores[(int)letter - (int)'A'];

            // Increase the score for each word.
            score = score + crozzleWordData.Count * wordScore;

            return (score);
        }        
        #endregion

        /// <summary>
        /// This method will check the wordlist by read the wordlist first
        /// and then validate the game properties (total words, total rows, total columns and difficulty levels)
        /// and lastly check each game property block in a loop.
        /// </summary>
        /// <param name="path">This method will take path from the wordlist open file dialog then process it.</param>
        #region open and validate wordlist file
        public void readWordlistFile(String path)
        {
            // Stream reader and writer properties.
            String fileName = Path.GetFileName(path);
            StreamReader reader = new StreamReader(path);
            StreamWriter logWriter = new StreamWriter(Path.GetDirectoryName(path) + @"\Assignment 1 log.txt", true);
            int line = 0;

            // Start writing log.
            Console.WriteLine("Start processing file: " + fileName);
            logWriter.WriteLine("Start processing file: " + fileName);

            // Validate file.
            gameProperties = validateGameProperties(reader, ref line, logWriter);
            wordList = validateWordlist(reader, ref line, logWriter);

            if (gameProperties != null && wordList != null)
            {
                int size;
                Int32.TryParse(gameProperties[0], out size);
                if (size != wordList.Count())
                {
                    Console.WriteLine("Error: word list size (" + gameProperties[0] + ") does not match with total words found (" + size + ")");
                    logWriter.WriteLine("Error: word list size (" + gameProperties[0] + ") does not match with total words found (" + size + ")");
                }

                else
                {
                    Console.WriteLine(fileName + "file is valid, cheers!");
                    logWriter.WriteLine(fileName + "file is valid, cheers!");
                }


                // Check first block contains how many words.
                int gameWords;
                Int32.TryParse(gameProperties[0], out gameWords);
                if (gameWords < minWords)
                {
                    Console.WriteLine("Wordlist contains (" + gameWords + "), minimum is " + minWords + " words.");
                    logWriter.WriteLine("Wordlist contains (" + gameWords + "), minimum is " + minWords + " words.");
                }
                else if (gameWords > maxWords)
                {
                    Console.WriteLine("Wordlist contains (" + gameWords + "), maximum is " + maxWords + " words.");
                    logWriter.WriteLine("Wordlist contains (" + gameWords + "), maximum is " + maxWords + " words.");
                }
            }

            // End  process.
            Console.WriteLine("End procesing file" + fileName);
            logWriter.WriteLine("End procesing file" + fileName);
            
            // End log and give notification.
            Console.WriteLine("Log file is closed");
            logWriter.WriteLine("Log file is closed");

            // Determine wordlist file validity.
            if (gameProperties == null || wordList == null)
                wordListFileValidator = false;
            else
                wordListFileValidator = true;

            // Close all files.
            reader.Close();
            logWriter.Close();
        }

        public String[] validateGameProperties(StreamReader reader, ref int line, StreamWriter logWriter)
        {
            String lineCounter;
            int totalErrors = 0;
            char[] separators = { ',' };
            String[] properties;
            String notification;

            lineCounter = reader.ReadLine();
            line++;
            properties = lineCounter.Split(separators);

            // Check the game properies not empty.
            if (properties.Length > 0)
                for (int i = 0; i < propertiesLength; i++)
                    if (properties[i].Length == 0)
                    {
                        totalErrors++;
                        Console.WriteLine("Error: line " + line.ToString() + ": block " + (i + 1).ToString() + " is empty");
                        logWriter.WriteLine("Error: line " + line.ToString() + ": block " + (i + 1).ToString() + " is empty");
                    }
            
            // Check the first block is an integer and in range.
            notification = propertiesValidator(properties[0], minWords, maxWords);
            if (notification.Length > 0)
            {
                totalErrors++;
                Console.WriteLine("Error: line " + line.ToString() + ": total words value (" + properties[0] + ") is " + notification);
                logWriter.WriteLine("Error: line " + line.ToString() + ": total words value (" + properties[0] + ") is " + notification);
            }

            // Check the second block is an integer and in range.
            notification = propertiesValidator(properties[1], minRows, maxRows);
            if (notification.Length > 0)
            {
                totalErrors++;
                Console.WriteLine("Error: line " + line.ToString() + ": row size value (" + properties[1] + ") is " + notification);
                logWriter.WriteLine("Error: line " + line.ToString() + ": row size value (" + properties[1] + ") is " + notification);
            }

            // Check the third block is an integer and in range.
            notification = propertiesValidator(properties[2], minColumns, maxColumns);
            if (notification.Length > 0)
            {
                totalErrors++;
                Console.WriteLine("Error: line " + line.ToString() + ": column size value (" + properties[2] + ") is " + notification);
                logWriter.WriteLine("Error: line " + line.ToString() + ": column size value (" + properties[2] + ") is " + notification);
            }

            // Check fourth block whether contains a right difficulty level or not.
            if (!properties[3].Equals(easy, StringComparison.Ordinal))
                if (!properties[3].Equals(medium, StringComparison.Ordinal))
                    if (!properties[3].Equals(hard, StringComparison.Ordinal))
                        if (!properties[3].Equals(extreme, StringComparison.Ordinal))
                        {
                            totalErrors++;
                            Console.WriteLine("Error: line" + line.ToString() + ": contains " + properties[3].ToString() + ", this is not a correct difficulty level or integer.");
                            logWriter.WriteLine("Error: line" + line.ToString() + ": contains " + properties[3].ToString() + ", this is not a correct difficulty level or integer.");
                        }

            if (totalErrors > 0)
                return (null);
            else
                return (properties);
        }

        private String propertiesValidator(String block, int min, int max)
        {
            String errors = "";
            int x;

            if (Int32.TryParse(block, out x))
            {
                if (x < min || x > max)
                    errors = "out of range";
            } else {
                errors = "not an integer";
            }
            return (errors);
        }

        private List<String> validateWordlist(StreamReader reader, ref int line, StreamWriter logWriter)
        {
            String lineCounter;
            int totalErrors = 0;
            char[] separators = { ',' };
            String[] blocks;
            int totalWords = 0;
            List<String> wordlist = new List<String>();

            while (!reader.EndOfStream)
            {
                lineCounter = reader.ReadLine();
                line++;
                blocks = lineCounter.Split(separators);
                int totalWordsLength = Convert.ToInt32(blocks[0]);
                int blocksLength = blocks.Count();

                if (blocks.Length - propertiesLength == 0)
                {                    
                    totalErrors++;
                    Console.WriteLine("Error: line " + line.ToString() + ": does not contain any block");
                    logWriter.WriteLine("Error: line " + line.ToString() + ": does not contain any block");                     
                }
                else
                {
                    for (int i = 4; i < blocksLength; i++)
                    {
                        if (Regex.IsMatch(blocks[i], @"^[a-zA-Z]+$"))
                        {
                            totalWords++;
                            wordlist.Add(blocks[i]);

                            // Check the word limit.
                            if (totalWords == maxWords + 1)
                            {
                                totalErrors++;
                                Console.WriteLine("Error: line " + line.ToString() + ": wordlist contains more than " + maxWords + " words");
                                logWriter.WriteLine("Error: line " + line.ToString() + ": wordlist contains more than " + maxWords + " words");
                            }
                        }
                        else
                        {
                            totalErrors++;
                            Console.WriteLine("Error: line " + line.ToString() + ": word (" + blocks[i] + ") is not a proper word and/or alphabetic");
                            logWriter.WriteLine("Error: line " + line.ToString() + ": word (" + blocks[i] + ") is not a proper word and/or alphabetic");
                        }
                    }
                }
            }

            if (totalErrors > 0)
                return (null);
            else
                return (wordlist);
        }
        #endregion
        
        /// <summary>
        /// This method will read the crozzle file by checking the crozzle that contains whether
        /// an integer or not then checks the row and column total with the game property
        /// and followed with boolean confirmation.
        /// </summary>
        /// <param name="path">This method will take path from the crozzle open file dialog path then process it.</param>
        #region open and validate crozzle file
        public void readCrozzleFile(String path)
        {
            // Stream reader and writer properties.
            String fileName = Path.GetFileName(path);
            StreamReader reader = new StreamReader(path);
            StreamWriter logWriter = new StreamWriter(Path.GetDirectoryName(path) + @"\Assignment 1 log.txt", true);
            
            // Variables declaration.
            int totalErrors = 0;
            String line;
            char[] separators = { ',' };
            String[] row;
            int rowNumber = 0;
            int columnNumber = 0;
            crozzleRows = new List<String[]>();

            // Start writing log.
            Console.WriteLine("Start processing file: " + fileName);
            logWriter.WriteLine("Start processing file: " + fileName);

            // Crozzle file validator.
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                rowNumber++;
                row = line.Split(separators);
                crozzleRows.Add(row);

                // Check the number of column blocks match the specification with game properties.
                /*int columns;
                Int32.TryParse(gameProperties[2], out columns);
                if (row.Length != columns)
                {
                    totalErrors++;
                    Console.WriteLine("Error: line " + rowNumber.ToString() + ": row contains " + row.Length.ToString() + " columns which is wrong, the right one is " + gameProperties[2] + " columns");
                    logWriter.WriteLine("Error: line " + rowNumber.ToString() + ": row contains " + row.Length.ToString() + " columns which is wrong, the right one is " + gameProperties[2] + " columns");

                    Console.WriteLine("Total rows should be equals with total columns");
                    logWriter.WriteLine("Total rows should be equals with total columns");
                } */

                for (columnNumber = 0; columnNumber < row.Length; columnNumber++)
                    // Check the block only has 1 alphabetic character or empty.
                    if (!Regex.IsMatch(row[columnNumber], @"^[a-zA-Z\s]"))
                    {
                        totalErrors++;
                        Console.WriteLine("Error: line " + rowNumber.ToString() + ": block[" + row.Length.ToString() + ", " + (columnNumber + 1).ToString() + "] has \"" + row[columnNumber] + "\", cell should be empty or has only 1 alphabetic character");
                        logWriter.WriteLine("Error: line " + rowNumber.ToString() + ": block[" + rowNumber.ToString() + ", " + (columnNumber + 1).ToString() + "] has \"" + row[columnNumber] + "\", cell should be empty or has only 1 alphabetic character");
                    }
            }

            // Check the number of row blocks match the specification with game properties.

            int rows;
            if (Int32.TryParse(gameProperties[1], out rows))
            {
                if (rowNumber != rows)
                {
                    totalErrors++;
                    Console.WriteLine("Error: line " + rowNumber.ToString() + ": file contains " + rowNumber.ToString() + " rows which is wrong, the right one is " + gameProperties[1] + " rows");
                    logWriter.WriteLine("Error: line " + rowNumber.ToString() + ": file contains " + rowNumber.ToString() + " rows which is wrong, the right one is " + gameProperties[1] + " rows");

                    Console.WriteLine("Total rows should be equals with total columns and game properties");
                    logWriter.WriteLine("Total rows should be equals with total columns and game properties");
                }
            }
            else
            {
                rows = maxRows;
                if (rowNumber != rows)
                {
                    totalErrors++;
                    Console.WriteLine("Error: line " + rowNumber.ToString() + ": file contains " + rowNumber.ToString() + " rows which is wrong, the right one is " + gameProperties[1] + " rows");
                    logWriter.WriteLine("Error: line " + rowNumber.ToString() + ": file contains " + rowNumber.ToString() + " rows which is wrong, the right one is " + gameProperties[1] + " rows");

                    Console.WriteLine("Total rows should be equals with total columns and game properties");
                    logWriter.WriteLine("Total rows should be equals with total columns and game properties");
                }
            }

            // End  process.
            Console.WriteLine("End procesing file" + fileName);
            logWriter.WriteLine("End procesing file" + fileName);

            // End log and give notification.
            Console.WriteLine("Log file is closed");
            logWriter.WriteLine("Log file is closed");

            // Close all files.
            reader.Close();
            logWriter.Close();

            // Determine crozzle file validity.
            if (totalErrors > 0)
                crozzleFileValidator = false;
            else
                crozzleFileValidator = true;
        }
        #endregion

        /// <summary>
        /// This method is being made for validating the opened crozzle.
        /// To validate the crozzle, the program will call methods from CrozzleWordData class 
        /// then checks if there are some errors.
        /// 
        /// There are 2 types of validation, the first is easy difficulty level validation that followed
        /// by a WordGap class then the rests of validation methods.
        /// While the second one is validation for the rest of difficulty levels, that does not have the
        /// WordGap validation method, because the rest of difficulty levels do not have gap constraint.
        /// </summary>
        /// <param name="path">This method will take path from the openend crozzle file then process it.</param>
        #region validate crozzle
        public void validateCrozzle(String path)
        {
            // Stream reader and writer properties.
            String fileName = Path.GetFileName(path);
            StreamReader reader = new StreamReader(path);
            StreamWriter logWriter = new StreamWriter(Path.GetDirectoryName(path) + @"\Assignment 1 log.txt", true);

            // Start writing log.
            Console.WriteLine("Start processing file: " + fileName);
            logWriter.WriteLine("Start processing file: " + fileName);

            // If the difficulty in game property is contains "EASY" the game should check for the word gap.
            if (gameProperties[3] == easy)
            {
                
                // Create crozzle columns are required for reading crozzle,
                // as well as crozzle rows (it's obtained when reading the file).
                crozzleColumns = createCrozzleColumns();

                // Get all sequences, but duplicates will be reported as errors.
                crozzleWordData = new CrozzleWordData(crozzleRows, crozzleColumns);

                // Scan Check word sequences with the wordlist.
                crozzleWordData.MissingWordsScanner(wordList);

                // Scan for inverted direction words.
                crozzleWordData.DirectionScanner(wordList);

                // Scan for each words intersects at least one another words.
                crozzleWordData.IntersectionsScanner();

                // Scan for one group of word connects to another group.
                crozzleWordData.CheckWordGroup(crozzleRows, crozzleColumns);

                crozzleWordData.WordGap();
            }

            else
            {
                // Create crozzle columns are required for reading crozzle,
                // as well as crozzle rows (it's obtained when reading the file).
                crozzleColumns = createCrozzleColumns();

                // Get all sequences, but duplicates will be reported as errors.
                crozzleWordData = new CrozzleWordData(crozzleRows, crozzleColumns);

                // Scan Check word sequences with the wordlist.
                crozzleWordData.MissingWordsScanner(wordList);

                // Scan for inverted direction words.
                crozzleWordData.DirectionScanner(wordList);

                // Scan for each words intersects at least one another words.
                crozzleWordData.IntersectionsScanner();

                // Scan for one group of word connects to another group.
                crozzleWordData.CheckWordGroup(crozzleRows, crozzleColumns);
            }

            // End  process.
            Console.WriteLine("End procesing file" + fileName);
            logWriter.WriteLine("End procesing file" + fileName);

            // End log and give notification.
            Console.WriteLine("Log file is closed");
            logWriter.WriteLine("Log file is closed");

            // Close all files.
            reader.Close();
            logWriter.Close();

            // Determine crozzle validity.
            if (crozzleWordData.ErrorDetector)
            {
                crozzleValidator = false;
                foreach (String notification in crozzleWordData.Errors)
                {
                    Console.WriteLine(notification);
                    //logWriter.WriteLine(notification);
                }
            }
            else
            {
                crozzleValidator = true;
            }


            
        }
        #endregion
    }
}

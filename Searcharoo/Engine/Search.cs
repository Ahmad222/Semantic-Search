using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Collections;
using Searcharoo.Common;
using System.Linq;
using System.Collections.Generic;


namespace Searcharoo.Engine
{
    public class Search
    {
        #region Private Fields: _Stemmer, _Stopper, _GoChecker, _DisplayTime, _Matches
        /// <summary>Stemmer to use</summary>
        private IStemming _Stemmer;
        /// <summary>Stopper to use</summary>
        private IStopper _Stopper;
        /// <summary>Go word parser to use</summary>
        private IGoWord _GoChecker;
        /// <summary>Display string: time the search too</summary>
        private string _DisplayTime;
        /// <summary>Display string: matches (links and number of)</summary>
        private string _Matches = "";

        public static int flag;
        public static string[,] mat = new string[20, 4];
        public ResultFile[] infiless = new ResultFile[999];
        public int [] sortranks = new int[999];
                   
        #endregion

        #region Public Properties: SearchQueryMatchHtml, DisplayTime
        public string SearchQueryMatchHtml
        {
            get { return _Matches; }
            set { _Matches = value; }
        }

        public string DisplayTime
        {
            get { return _DisplayTime; }
            set { _DisplayTime = value; }
        }
        #endregion
        public static string arabicSynonym(string[] sta)
        {



            DataWordsDataContext words = new DataWordsDataContext();
            
            string ll="";
            int counter = 0;
            foreach (string s in sta)
            {

                var wordID = (from a in words.Words
                              where a.Word1 == s
                              select new { a.Id }).SingleOrDefault();

                if (wordID == null)
                {
                    
                        ll += sta[counter] + " ";
                        counter++;
                   
                }
                else
                {
                    int W_ID = Convert.ToInt32((wordID).Id);
                    var synonyms = (from b in words.World_syns
                                    where b.Word_id == W_ID
                                    orderby b.distance
                                    select new { b.Word_S }).ToList();



             
                    for (int oo = 0; oo < synonyms.Count; oo++)
                    {
                        ll += synonyms[oo].Word_S.ToString() + " ";

                    }

                    counter++;
             

                }

               
            

            }
            return ll.ToString().Trim();
         
        }
        public string originalword = "";

        static System.IO.FileInfo files = new System.IO.FileInfo(@"D:\s5\WebApplication\result.txt");
        string lines;

        

        public SortedList GetResults(string searchterm, Catalog catalog)
        {
           // System.IO.TextWriter tw = new System.IO.StreamWriter(files.Open(System.IO.FileMode.Truncate));
            originalword = searchterm;
            SortedList output = new SortedList();
            

            Dictionary<int, ResultFile> DIC = new Dictionary<int, ResultFile>();
            

            // ----------------------- DOING A SEARCH ----------------------- 
            if ((null != searchterm) && (null != catalog))
            {
                string[] searchTermArray = null, searchTermDisplay = null;

                /****** Too *********/
                Regex r = new Regex(@"\s+");            //remove all whitespace
                searchterm = r.Replace(searchterm, " ");// to a single space
                searchTermArray = searchterm.Split(' '); // then split
                searchTermDisplay = (string[])searchTermArray.Clone();

                //3bi alarray++++++++++


                if (flag == 1)
                {
                    String Stoped = ""; String Stemmed = "";
                    int i = 1;// richTextBox1.Lines.Length;
                    ArabicStopWords SW = new ArabicStopWords();
                    for (int k = 0; k < i; k++)
                    {
                        string S = ""; string Temp = "";
                        Temp = searchterm;//richTextBox1.Lines[k];
                        for (int l = 0; l < Temp.Length; l++)
                            if (char.IsLetter(Temp, l))
                                S += Temp[l].ToString();
                            else
                                S += " ";
                        S = S.Trim();
                        string[] R = S.Split(' ');
                        for (int j = 0; j < R.Length; j++)
                        {
                            string Stem = SW.removing(R[j]);
                            Stem += " ";
                            Stoped += Stem;
                            // richTextBox4.AppendText(Stem);
                        }

                    }


                    //    richTextBox2.Clear();
                    i = 1;//richTextBox4.Lines.Length;
                    ISRI Stemmer = new ISRI();
                    for (int k = 0; k < i; k++)
                    {
                        string S = ""; string Temp = "";
                        Temp = Stoped;//richTextBox4.Lines[k];
                        for (int l = 0; l < Temp.Length; l++)
                            if (char.IsLetter(Temp, l))
                                S += Temp[l].ToString();
                            else
                                S += " ";
                        S = S.Trim();
                        string[] R = S.Split(' ');
                        for (int j = 0; j < R.Length; j++)
                        {
                            string Stem = Stemmer.Stemming(R[j]);
                            Stem += " ";
                            Stemmed += Stem;
                            searchTermArray[j] = Stem;
                            //richTextBox2.AppendText(Stem);
                        }
                    }
                }
                else
                {
                    SetPreferences();
                    for (int i = 0; i < searchTermArray.Length; i++)
                    {
                        if (_GoChecker.IsGoWord(searchTermArray[i]))
                        {	// was a Go word, just Lower it
                            searchTermArray[i] = searchTermArray[i].ToLower();
                        }
                        else
                        {	// Not a Go word, apply stemming
                            searchTermArray[i] = searchTermArray[i].Trim(' ', '?', '\"', ',', '\'', ';', ':', '.', '(', ')').ToLower();
                            searchTermArray[i] = _Stemmer.StemWord(searchTermArray[i].ToString());
                        }
                    }

                }


                //searchTermArray = arabicSynonym(searchTermArray);
                if (flag == 1)
                {
                              //remove all whitespace
                  //  searchterm = r.Replace(searchterm, " ");// to a single space
                  //  searchTermArray = searchterm.Split(' '); // then split


                    searchTermDisplay = (string[])searchTermArray.Clone();

                    string arabicarray = arabicSynonym(searchTermArray);

                    arabicarray = arabicarray.Trim();
                    searchterm = arabicarray;
                    searchTermArray = searchterm.Split(' '); // then split
                    searchTermDisplay = (string[])searchTermArray.Clone();

                    SortedList outi = new SortedList();
                    int jj = 999;
                    //-----------dor 3la al Original word bal2wl

                   




                    //-------------------- hl8 alklmat alfr3ieh
                    string testingresults = "";
                    //-------------------- hl8 alklmat alfr3ieh
                    string[] words = originalword.Trim().Split(' ');
                    string[] words3 = new string[words.Length];
                    int zz = 0;
                    for (int z = 0; z < words.Length; z++)
                    {
                        if (words[z] != "")
                        {
                            words3[zz] = words[z];
                            zz++;
                        }
                    }
                    for (int zozo = 0; zozo < words3.Count(); zozo++)
                    {
                        int pp = 0;
                        for (int koko = zz; koko > 0; koko--)
                        {

                            String lF31 = "";
                            for (int p = zozo; p < (zz - pp); p++)
                            {
                                if (words3[p] == null)
                                    break;
                                lF31 += words3[p] + " ";
                            }
                            string lF32 = lF31.Trim();
                            if (lF32 == "")
                            {
                                break;
                            }
                            testingresults += "  *  " + lF32;
                            string[] staF = lF32.Split(' '); // then split
                            string[] stdF = (string[])staF.Clone();
                            if (originalword != "")
                            {
                                outi = getresultss(lF32, staF, stdF, catalog);
                                
                                int i = 0;
                                foreach (object o in outi)
                                {
                                    if (!output.ContainsKey(jj))
                                    {
                                        output.Add(jj, infiless[i]);
                                        DIC.Add(jj, infiless[i]);
                                        jj++;
                                    }

                                    i++;
                                }
                                
                            }

                          

                            pp++;
                        }
                    }

                    string awl = "";
                    for (int oposit = 1; oposit < words3.Count(); oposit++)
                    {
                            awl = words3[0] + " "+ words3[oposit];
                       
                        string lF32 = awl.Trim();
                        if (lF32 == "" || lF32==words3[0])
                        {
                            break;
                        }
                        testingresults += "  **  " + lF32;
                        string[] staF = lF32.Split(' '); // then split
                        string[] stdF = (string[])staF.Clone();
                        if (originalword != "")
                        {
                            outi = getresultss(lF32, staF, stdF, catalog);
                       
                            int i = 0;
                            foreach (object o in outi)
                            {
                                
                                if (!output.ContainsKey(jj))
                                {
                                    output.Add(jj, infiless[i]);
                                    DIC.Add(jj, infiless[i]);
                                    jj++;
                                }

                                i++;
                            }
                            
                        }
                    }


                        
                        //------------------semantic
                        foreach (string s in arabicarray.Split(' '))
                        {
                            String l = s.Trim();
                            string[] sta = l.Split(' '); // then split
                            string[] std = (string[])sta.Clone();
                            if (s != "")
                            {
                                testingresults += "  /  " + s;
                                outi = getresultss(s, sta, std, catalog);
                               
                                int i = 0;
                                foreach (object o in outi)
                                {
                                    
                                    if (!output.ContainsKey(jj))
                                    {
                                        output.Add(jj, infiless[i]);
                                        DIC.Add(jj, infiless[i]);
                                        jj++;
                                    }

                                    i++;
                                }
                                // }
                            }
                        }


                    // Meaning  //
                        string EngWords = "";
                        ISRI Stemmer = new ISRI();
                        foreach (string s in originalword.Split(' '))
                        {
                            DataWordsDataContext words1DB = new DataWordsDataContext();
                            EngWords += s + " ";
                            String l = s.Trim();
                            string Stem = s;//Stemmer.Stemming(s);
                            var sm = (from a in words1DB.meanings
                                      where a.word1 == Stem
                                      select new { a.word2 }).ToList();

                            string[] MeanStr = new string[sm.Count];
                            for (int oo = 0; oo < sm.Count; oo++)
                            {
                                MeanStr [oo]= sm[oo].word2.ToString();

                            }


                            foreach (string smm in MeanStr)
                            {

                                string[] sta = smm.Split(' '); // then split
                                string[] std = (string[])sta.Clone();
                            
                                if (smm != "")
                                {
                                    testingresults += "  /  " + s;
                                    Searcharoo.Engine.Search.flag = 0;
                                    outi = getresultss(smm, sta, std, catalog);
                                    int i = 0;
                                    foreach (object o in outi)
                                    {
                                        if (!output.ContainsKey(jj))
                                        {
                                            output.Add(jj, infiless[i]);
                                            DIC.Add(jj, infiless[i]);
                                            jj++;
                                        }

                                        i++;
                                    }
                                }
                            }
                        }


                    //--------------------------------------



                    //output = outi;
                    testingresults += " ";


                    ResultFile [] sorte = new ResultFile[DIC.Count];

              
                  for (int zi = 0; zi < DIC.Count; zi++)
                    {
                        sorte[zi] = DIC[zi+999];
                    }
                  ResultFile[] droppe = new ResultFile[DIC.Count];
                    
                  int indexOfRank = 0;
                  
                    for (int bra = 0; bra < sorte.Count(); bra++)
                    {
                        indexOfRank = 0;
                        for (int jwa = bra+1; jwa < sorte.Count(); jwa++)
                        {
                            if (sorte[bra].Title == sorte[jwa].Title)
                            {
                                DIC.Remove(jwa + 999);

                                indexOfRank++;
                            }
                            else 
                            {
                                //indexOfRank--;
                            }
                            
                        }
                        if (bra >= DIC.Count)
                        {
                            break;
                        }
                        DIC.ElementAt(bra).Value.Rank += indexOfRank;
                    }
                    //DIC.Remove(999);
                    SortedList output2 = new SortedList();
             //       foreach(var a
                    string[] words_to_search = originalword.Trim().Split(' ');   // alklmat ali bda ttlon
                    bool flagi = false;
                    for (int ta = 0; ta < DIC.Count; ta++)
                    {
                    
                        output2.Add(ta + 999, DIC.ElementAt(ta).Value);
                    }
                    
                    return output2;

                }

               

                if (searchterm == String.Empty)
                {
                    // After trimming the search term, it was found to be empty!
                    return output;
                }
                else
                {	// we have a search term!
                    DateTime start = DateTime.Now;  // to show 'time taken' to perform search

                    // Array of arrays of results that match ONE of the search criteria
                    Hashtable[] searchResultsArrayArray = new Hashtable[searchTermArray.Length];
                    // finalResultsArray is populated with pages that *match* ALL the search criteria
                    HybridDictionary finalResultsArray = new HybridDictionary();

                    bool botherToFindMatches = true;
                    int indexOfShortestResultSet = -1, lengthOfShortestResultSet = -1;

                    for (int i = 0; i < searchTermArray.Length; i++)
                    {	// ##### THE SEARCH #####
                        searchResultsArrayArray[i] = catalog.Search(searchTermArray[i].ToString());
                        if (null == searchResultsArrayArray[i])
                        {
                            // shilllooooo
                            _Matches += searchTermDisplay[i] + " <font color=gray style='font-size:xx-small'>(not found)</font> ";
                            botherToFindMatches = false; // if *any one* of the terms isn't found, there won't be a 'set' of Matches
                        }
                        else
                        {
                            int resultsInThisSet = searchResultsArrayArray[i].Count;
                            _Matches += "<a href=\"?" + Preferences.QuerystringParameterName + "=" + searchTermDisplay[i] + "\" title=\"" + searchTermArray[i] + "\">"
                                    + searchTermDisplay[i]
                                    + "</a> <font color=gray style='font-size:xx-small'>(" + resultsInThisSet + ")</font> ";
                            if ((lengthOfShortestResultSet == -1) || (lengthOfShortestResultSet > resultsInThisSet))
                            {
                                indexOfShortestResultSet = i;
                                lengthOfShortestResultSet = resultsInThisSet;
                            }
                        }
                    }

                    // Find the common files from the array of arrays of documents
                    // matching ONE of the criteria
                    if (botherToFindMatches)                                            // all words have *some* matches
                    {																	// for each result set [NOT required, but maybe later if we do AND/OR searches)
                        int c = indexOfShortestResultSet;                               // loop through the *shortest* resultset
                        Hashtable searchResultsArray = searchResultsArrayArray[c];

                        foreach (object foundInFile in searchResultsArray)             // for each file in the *shortest* result set
                        {
                            DictionaryEntry fo = (DictionaryEntry)foundInFile;          // find matching files in the other resultsets

                            int matchcount = 0, totalcount = 0, weight = 0;

                            for (int cx = 0; cx < searchResultsArrayArray.Length; cx++)
                            {
                                totalcount += (cx + 1);                                // keep track, so we can compare at the end (if term is in ALL resultsets)
                                if (cx == c)                                      // current resultset
                                {
                                    matchcount += (cx + 1);                          // implicitly matches in the current resultset
                                    weight += (int)fo.Value;                       // sum the weighting
                                }
                                else
                                {
                                    Hashtable searchResultsArrayx = searchResultsArrayArray[cx];
                                    if (null != searchResultsArrayx)
                                    {
                                        foreach (object foundInFilex in searchResultsArrayx)
                                        {   // for each file in the result set
                                            DictionaryEntry fox = (DictionaryEntry)foundInFilex;
                                            if (fo.Key == fox.Key)
                                            {
                                                matchcount += (cx + 1);               // and if it matches, track the matchcount
                                                weight += (int)fox.Value;           // and weighting; then break out of loop, since
                                                break;                              // no need to keep looking through this resultset
                                            }
                                            else
                                            {

                                            }
                                        } // foreach
                                    } // if
                                } // else
                            } // for
                            if ((matchcount > 0) && (matchcount == totalcount))		// was matched in each Array
                            {   // we build the finalResults here, to pass to the formatting code below
                                // - we could do the formatting here, but it would mix up the 'result generation'
                                // and display code too much
                                fo.Value = weight; // set the 'weight' in the combined results to the sum of individual document matches
                                if (!finalResultsArray.Contains(fo.Key)) finalResultsArray.Add(fo.Key, fo);
                            } // if
                        } // foreach
                    }


                    // Time taken calculation
                    Int64 ticks = DateTime.Now.Ticks - start.Ticks;
                    TimeSpan taken = new TimeSpan(ticks);
                    if (taken.Seconds > 0)
                    {
                        _DisplayTime = taken.Seconds + " seconds";
                    }
                    else if (taken.TotalMilliseconds > 0)
                    {
                        _DisplayTime = Convert.ToInt32(taken.TotalMilliseconds) + " milliseconds";
                    }
                    else
                    {
                        _DisplayTime = "less than 1 millisecond";
                    }

                    // The preceding 80 lines (or so) replaces this single line from Version 1
                    //       Hashtable searchResultsArray = m_catalog.Search (searchterm);
                    // when only single-word-searches were supported. Look closely and you'll see this line
                    // labelled #THE SEARCH# still in the code above...

                    // Format the results
                    if (finalResultsArray.Count > 0)
                    {	// intermediate data-structure for 'ranked' result HTML
                        //SortedList 
                        output = new SortedList(finalResultsArray.Count); // empty sorted list
                        //                DictionaryEntry fo;
                        ResultFile infile;
                        
                        //                string result="";
                        int sortrank = 0;

                        // build each result row
                        foreach (object foundInFile in finalResultsArray.Keys)
                        {

                            // Create a ResultFile with it's own Rank
                            infile = new ResultFile((File)foundInFile);
                            lines += infile.Title + "+" + infile.Description + "*";
                            // Jim Harkins [sort for paging] ported from VB to C#
                            // http://www.codeproject.com/aspnet/spideroo.asp#xx927327xx

                            infile.Rank = (int)((DictionaryEntry)finalResultsArray[foundInFile]).Value;

                            sortrank = infile.Rank * -1000;		// Assume not 'thousands' of results
                            string soso = infile.Description;
                            string[] desc = infile.Description.Split(' ');

                            if (Searcharoo.Engine.Search.flag == 1)
                            {

                                int i;
                                for (int n = 0; n < desc.Length; n++)
                                {
                                    String st = null;
                                    String di = null;

                                    ISRI Stemmer = new ISRI();

                                    st = Stemmer.Stemming(searchterm);
                                    di = Stemmer.Stemming(desc[n]);

                                    
                                    if (di == st)
                                    {

                                       // desc[n] = @"<span style=""background-color: #FFCCFF"">" + desc[n] + @"</span>";
                                    }

                                    ////-----tloin aloriginal
                                    //string [] original = originalword.Split(' ');
                                    //for (int c = 0; c < original.Length; c++)
                                    //{
                                    //    st = Stemmer.Stemming(original[c]);
                                    //    di = Stemmer.Stemming(desc[n]);
                                    //    if (di == st)
                                    //    {

                                    //        desc[n] = @"<span style=""background-color: #FFCCFF"">" + desc[n] + @"</span>";
                                    //    }
                                    //}

                                    //--------------

                                }
                            }
                            else
                            {
                                // ll7zf +++++++++++++++++++
                                for (int i = 0; i < desc.Length; i++)
                                {
                                    //if (desc[i].ToLower() == searchterm.ToLower() )
                                    String st = _Stemmer.StemWord(searchterm);
                                    String di = _Stemmer.StemWord(desc[i]);
                                    if (di.ToLower() == st.ToLower())
                                    {
                                        //desc[i] = @"<span style=""background-color: #FFCCFF"">" + desc[i] + @"</span>";
                                    }
                                }
                            }

                            infile.Description = "";   // = Convert.ToString(desc);
                            for (int i = 0; i < desc.Length; i++)
                            {
                                infile.Description += " " + desc[i];
                            }

                            if (output.Contains(sortrank))
                            { // rank exists - drop key index one number until it fits
                                for (int i = 1; i < 999; i++)
                                {
                                    sortrank++;
                                    if (!output.Contains(sortrank))
                                    {
                                        output.Add(sortrank, infile);
                                        DIC.Add(sortrank, infile);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                output.Add(sortrank, infile);
                                DIC.Add(sortrank, infile);
                            }
                            sortrank = 0;	// reset for next pass
                        }
                        // Jim Harkins [paged results]
                        // http://aspnet.4guysfromrolla.com/articles/081804-1.aspx
                    } // else Count == 0, so output SortedList will be empty
                }
            }


            int sortrank2 = -1000 * (originalword.Split(' ').Length);
            //{{{{{{{{{{{
            sortrank2 = (-1000 * (originalword.Split(' ').Length)) + output.Count;
            SortedList outiJE = new SortedList();
            string[] words1 = originalword.Trim().Split(' ');
            string[] words31 = new string[words1.Length];
            int zz1 = 0;
            for (int z = 0; z < words1.Length; z++)
            {
                if (words1[z] != "")
                {
                    words31[zz1] = words1[z];
                    zz1++;
                }
            }
            for (int zozo = 0; zozo < words31.Count(); zozo++)
            {
                int pp = 0;
                for (int koko = zz1; koko > 0; koko--)
                {

                    String lF31 = "";
                    for (int p = zozo; p < (zz1 - pp); p++)
                    {
                        if (words31[p] == null)
                            break;
                        lF31 += words31[p] + " ";
                    }
                    string lF32 = lF31.Trim();
                    if (lF32 == "")
                    {
                        break;
                    }
                    //testingresults += "  *  " + lF32;
                    string[] staF = lF32.Split(' '); // then split
                    string[] stdF = (string[])staF.Clone();
                    if (originalword != "")
                    {
                        outiJE = getresultss(lF32, staF, stdF, catalog);

                        int i = 0;
                        foreach (object o in outiJE)
                        {
                            if (!output.ContainsKey(sortrank2))
                            {
                                output.Add(sortrank2, infiless[i]);
                                DIC.Add(sortrank2, infiless[i]);
                                sortrank2++;
                            }

                            i++;
                        }

                    }



                    pp++;
                }
            }

            string awl1 = "";
            for (int oposit = 1; oposit < words31.Count(); oposit++)
            {
                awl1 = words31[0] + " " + words31[oposit];

                string lF32 = awl1.Trim();
                if (lF32 == "" || lF32 == words31[0])
                {
                    break;
                }
                //testingresults += "  **  " + lF32;
                string[] staF = lF32.Split(' '); // then split
                string[] stdF = (string[])staF.Clone();
                if (originalword != "")
                {
                    outiJE = getresultss(lF32, staF, stdF, catalog);

                    int i = 0;
                    foreach (object o in outiJE)
                    {

                        if (!output.ContainsKey(sortrank2))
                        {
                            output.Add(sortrank2, infiless[i]);
                            DIC.Add(sortrank2, infiless[i]);
                            sortrank2++;
                        }

                        i++;
                    }

                }
            }


            //------------------semantic
            sortrank2 = -1000 * (originalword.Split(' ').Length);
            sortrank2 += output.Count;

            string[] semArr = searchterm.Split(' ');
            for (int sem = 0; sem < semArr.Length; sem++)
            {
                semArr[sem] = _Stemmer.StemWord(semArr[sem]);
            }

            string arabicarray1 = arabicSynonym(semArr);
            foreach (string s in arabicarray1.Split(' '))
            {
                SortedList outi = new SortedList();
                String l = s.Trim();
                string[] sta = l.Split(' '); // then split
                string[] std = (string[])sta.Clone();
                if (s != "")
                {
                    // testingresults += "  /  " + s;
                    outi = getresultss(s, sta, std, catalog);

                    int i = 0;
                    foreach (object o in outi)
                    {

                        if (!output.ContainsKey(sortrank2))
                        {
                            output.Add(sortrank2, infiless[i]);
                            DIC.Add(sortrank2, infiless[i]);
                            sortrank2++;
                        }

                        i++;
                    }
                    // }
                }
            }


            //}}}}}}}}
           
            // Meaning------------------------------
            //int sortrank2 = -1000 * (originalword.Split(' ').Length);
            sortrank2 = (-1000 * (originalword.Split(' ').Length)) + output.Count;
            foreach (string s in originalword.Split(' '))
            {
                DataWordsDataContext words2DB = new DataWordsDataContext();
                String l = s.Trim();

                var sm = (from a in words2DB.meanings
                          where a.word1 == s
                          select new { a.word2 }).ToList();

                string searchTermArray = "";

                
                 string stemd = "";
                string[] MeanStr2 = new string[sm.Count];
                for (int oo = 0; oo < sm.Count; oo++)
                {
                    
                   // int yi = 1;//richTextBox4.Lines.Length;
                    ISRI Stemmer = new ISRI();
                 //   for (int k = 0; k < yi; k++)
                  //  {
                        string S = ""; string Temp = "";
                        Temp = sm[oo].word2.ToString().Trim();//richTextBox4.Lines[k];
                        for (int lo = 0; lo < Temp.Length; lo++)
                            if (char.IsLetter(Temp, lo))
                                S += Temp[lo].ToString();
                            else
                                S += " ";
                        S = S.Trim();
                        string[] R = S.Split(' ');
                        for (int j = 0; j < R.Length; j++)
                        {
                            string Stem = Stemmer.Stemming(R[j]);
                            Stem += " ";
                            stemd += Stem;
                           // searchTermArray[j] = Stem;
                            //richTextBox2.AppendText(Stem);
                        }
                   // }

                    
                   // MeanStr2[oo] = stem;
                  //  searchTermArray += MeanStr2[oo] + " ";

                }

                MeanStr2 = stemd.Split(' ');
                string arabicarray = arabicSynonym(MeanStr2);

                MeanStr2 = arabicarray.Split(' ');


                foreach (string smm in MeanStr2)
                {
                    string[] sta = smm.Split(' '); // then split
                    string[] std = (string[])sta.Clone();
                    SortedList outi = new SortedList();
                    if (smm != "")
                    {
                        //testingresults += "  /  " + s;
                        Searcharoo.Engine.Search.flag = 1;
                        outi = getresultss(smm, sta, std, catalog);
                        int i = 0;
                        foreach (object o in outi)
                        {
                            if (!output.ContainsKey(sortrank2))
                            {
                                output.Add(sortrank2, infiless[i]);
                                DIC.Add(sortrank2, infiless[i]);
                                sortrank2++;
                            }

                            i++;
                        }
                    }
                }
            }


            //--------------------------------------



            //--------------------------------------
            ResultFile[] sorte2 = new ResultFile[DIC.Count];


            for (int zi = 0; zi < DIC.Count; zi++)
            {
                sorte2[zi] = DIC[zi - (1000)*(originalword.Split(' ').Count())];
            }
            ResultFile[] droppe2 = new ResultFile[DIC.Count];

            int indexOfRank2 = 0;

            for (int bra = 0; bra < sorte2.Count(); bra++)
            {
                indexOfRank2 = 0;
                for (int jwa = bra + 1; jwa < sorte2.Count(); jwa++)
                {
                    if (sorte2[bra].Title == sorte2[jwa].Title)
                    {
                        DIC.Remove(jwa + (-1000 * (originalword.Split(' ').Length)));
                        indexOfRank2++;
                    }
                    else
                    {
                        
                    }

                }
                if (bra >= DIC.Count)
                {
                    break;
                }
                DIC.ElementAt(bra).Value.Rank += indexOfRank2;
            }
            
            SortedList output22 = new SortedList();
            
            string[] words_to_search2 = searchterm.Split(' ');
            bool flagi2 = false;
            for (int ta = 0; ta < DIC.Count; ta++)
            {
                string[] des = DIC.ElementAt(ta).Value.Description.Split(' ');
                DIC.ElementAt(ta).Value.Description = "";

                foreach (string s in des)
                {
                    foreach (string ora in words_to_search2)
                    {
                        if (s.ToLower() == ora.ToLower())
                        {
                            // mteh
                            string ad = @"<span style=""background-color: #FFCCFF"">" + s + @"</span>";
                            DIC.ElementAt(ta).Value.Description += ad + " ";
                            flagi2 = true;
                        }

                    }
                    if (!flagi2)
                    {
                        DIC.ElementAt(ta).Value.Description += s + " ";
                    }
                    flagi2 = false;
                }

                output22.Add(ta + 999, DIC.ElementAt(ta).Value);
            }
         

            return output22;


            
        }
        public SortedList getresultss(string st, string[] sta, string[] std, Catalog catalog)
        {
            System.IO.TextWriter tw = new System.IO.StreamWriter(files.Open(System.IO.FileMode.Truncate));
            SortedList output2 = new SortedList();

            int count = 0;



            if (flag == 1)
            {
                String Stoped = ""; String Stemmed = "";
                int i = 1;// richTextBox1.Lines.Length;
                ArabicStopWords SW = new ArabicStopWords();
                for (int k = 0; k < i; k++)
                {
                    string S = ""; string Temp = "";
                    Temp = st;//richTextBox1.Lines[k];
                    for (int l = 0; l < Temp.Length; l++)
                        if (char.IsLetter(Temp, l))
                            S += Temp[l].ToString();
                        else
                            S += " ";
                    S = S.Trim();
                    string[] R = S.Split(' ');
                    for (int j = 0; j < R.Length; j++)
                    {
                        string Stem = SW.removing(R[j]);
                        Stem += " ";
                        Stoped += Stem;
                        // richTextBox4.AppendText(Stem);
                    }

                }


                //    richTextBox2.Clear();
                i = 1;//richTextBox4.Lines.Length;
                ISRI Stemmer = new ISRI();
                for (int k = 0; k < i; k++)
                {
                    string S = ""; string Temp = "";
                    Temp = Stoped;//richTextBox4.Lines[k];
                    for (int l = 0; l < Temp.Length; l++)
                        if (char.IsLetter(Temp, l))
                            S += Temp[l].ToString();
                        else
                            S += " ";
                    S = S.Trim();
                    string[] R = S.Split(' ');
                    for (int j = 0; j < R.Length; j++)
                    {
                        string Stem = Stemmer.Stemming(R[j]);
                        Stem += " ";
                        Stemmed += Stem;
                        sta[j] = Stem;
                        //richTextBox2.AppendText(Stem);
                    }
                }
            }
            else
            {
                SetPreferences();
                for (int i = 0; i < sta.Length; i++)
                {
                    if (_GoChecker.IsGoWord(sta[i]))
                    {	// was a Go word, just Lower it
                        sta[i] = sta[i].ToLower();
                    }
                    else
                    {	// Not a Go word, apply stemming
                        sta[i] = sta[i].Trim(' ', '?', '\"', ',', '\'', ';', ':', '.', '(', ')').ToLower();
                        sta[i] = _Stemmer.StemWord(sta[i].ToString());
                    }
                }

            }



            if (st == String.Empty)
            {
                // After trimming the search term, it was found to be empty!
                return output2;
            }
            else
            {	// we have a search term!
                DateTime start = DateTime.Now;  // to show 'time taken' to perform search

                // Array of arrays of results that match ONE of the search criteria
                Hashtable[] searchResultsArrayArray = new Hashtable[sta.Length];
                // finalResultsArray is populated with pages that *match* ALL the search criteria
                HybridDictionary finalResultsArray = new HybridDictionary();

                bool botherToFindMatches = true;
                int indexOfShortestResultSet = -1, lengthOfShortestResultSet = -1;

                for (int i = 0; i < sta.Length; i++)
                {	// ##### THE SEARCH #####
                    searchResultsArrayArray[i] = catalog.Search(sta[i].ToString());
                    if (null == searchResultsArrayArray[i])
                    {
                        // shilllooooo
                        _Matches += std[i] + " <font color=gray style='font-size:xx-small'>(not found)</font> ";
                        botherToFindMatches = false; // if *any one* of the terms isn't found, there won't be a 'set' of Matches
                    }
                    else
                    {
                        int resultsInThisSet = searchResultsArrayArray[i].Count;
                        _Matches += "<a href=\"?" + Preferences.QuerystringParameterName + "=" + std[i] + "\" title=\"" + sta[i] + "\">"
                                + std[i]
                                + "</a> <font color=gray style='font-size:xx-small'>(" + resultsInThisSet + ")</font> ";
                        if ((lengthOfShortestResultSet == -1) || (lengthOfShortestResultSet > resultsInThisSet))
                        {
                            indexOfShortestResultSet = i;
                            lengthOfShortestResultSet = resultsInThisSet;
                        }
                    }
                }

                // Find the common files from the array of arrays of documents
                // matching ONE of the criteria
                if (botherToFindMatches)                                            // all words have *some* matches
                {																	// for each result set [NOT required, but maybe later if we do AND/OR searches)
                    int c = indexOfShortestResultSet;                               // loop through the *shortest* resultset
                    Hashtable searchResultsArray = searchResultsArrayArray[c];

                    foreach (object foundInFile in searchResultsArray)             // for each file in the *shortest* result set
                    {
                        DictionaryEntry fo = (DictionaryEntry)foundInFile;          // find matching files in the other resultsets

                        int matchcount = 0, totalcount = 0, weight = 0;

                        for (int cx = 0; cx < searchResultsArrayArray.Length; cx++)
                        {
                            totalcount += (cx + 1);                                // keep track, so we can compare at the end (if term is in ALL resultsets)
                            if (cx == c)                                      // current resultset
                            {
                                matchcount += (cx + 1);                          // implicitly matches in the current resultset
                                weight += (int)fo.Value;                       // sum the weighting
                            }
                            else
                            {
                                Hashtable searchResultsArrayx = searchResultsArrayArray[cx];
                                if (null != searchResultsArrayx)
                                {
                                    foreach (object foundInFilex in searchResultsArrayx)
                                    {   // for each file in the result set
                                        DictionaryEntry fox = (DictionaryEntry)foundInFilex;
                                        if (fo.Key == fox.Key)
                                        {
                                            matchcount += (cx + 1);               // and if it matches, track the matchcount
                                            weight += (int)fox.Value;           // and weighting; then break out of loop, since
                                            break;                              // no need to keep looking through this resultset
                                        }
                                        else
                                        {

                                        }
                                    } // foreach
                                } // if
                            } // else
                        } // for
                        if ((matchcount > 0) && (matchcount == totalcount))		// was matched in each Array
                        {   // we build the finalResults here, to pass to the formatting code below
                            // - we could do the formatting here, but it would mix up the 'result generation'
                            // and display code too much
                            fo.Value = weight; // set the 'weight' in the combined results to the sum of individual document matches
                            if (!finalResultsArray.Contains(fo.Key)) finalResultsArray.Add(fo.Key, fo);
                        } // if
                    } // foreach
                }


                // Time taken calculation
                Int64 ticks = DateTime.Now.Ticks - start.Ticks;
                TimeSpan taken = new TimeSpan(ticks);
                if (taken.Seconds > 0)
                {
                    _DisplayTime = taken.Seconds + " seconds";
                }
                else if (taken.TotalMilliseconds > 0)
                {
                    _DisplayTime = Convert.ToInt32(taken.TotalMilliseconds) + " milliseconds";
                }
                else
                {
                    _DisplayTime = "less than 1 millisecond";
                }

                // Format the results
                if (finalResultsArray.Count > 0)
                {	// intermediate data-structure for 'ranked' result HTML
                    //SortedList 
                    output2 = new SortedList(finalResultsArray.Count); // empty sorted list
                    //                DictionaryEntry fo;
                    ResultFile infile;
                    ResultFile infile1;
                        
                    //                string result="";
                    int sortrank = 0;

                    // build each result row
                   // hi al nested loop mnshan alfilat ali mkrra n7zfa
                    foreach (object foundInFile in finalResultsArray.Keys)
                    {
                        
                        infile = new ResultFile((File)foundInFile);
                        lines += infile.Title + "+" + infile.Description + "*";
                        foreach (object foundInFile1 in finalResultsArray.Keys)
                        {

                            // Create a ResultFile with it's own Rank
                            infile1 = new ResultFile((File)foundInFile1);
                            if (infile1.Title == infile.Title  && infile1.Description != infile.Description)//&&infile1.key != infile.key
                            {
                                // iza tnin mtsawiin 7zof wa7ed mnon w zid alrank tb3 altani
                                //finalResultsArray.Remove(keyofthis);infile1
                               // infile.Rank++;
                            }

                        }
                        
                    }
                        foreach (object foundInFile in finalResultsArray.Keys)
                        {

                            // Create a ResultFile with it's own Rank
                            infile = new ResultFile((File)foundInFile);
                            lines += infile.Title + "+" + infile.Description + "*";
                            // Jim Harkins [sort for paging] ported from VB to C#
                            // http://www.codeproject.com/aspnet/spideroo.asp#xx927327xx
                            infile.Rank = (int)((DictionaryEntry)finalResultsArray[foundInFile]).Value;
                            sortrank = infile.Rank * -1000;		// Assume not 'thousands' of results
                            string soso = infile.Description;
                            string[] desc = infile.Description.Split(' ');

                            if (Searcharoo.Engine.Search.flag == 1)
                            {

                                int i;
                                for (int n = 0; n < desc.Length; n++)
                                {
                                    String st1 = null;
                                    String di = null;

                                    ISRI Stemmer = new ISRI();

                                   // st1 = Stemmer.Stemming(st);
                                   // di = Stemmer.Stemming(desc[n]);


                                   // if (di == st1)
                                   // {
                                        string dd = desc[n];
                                   //     desc[n] = @"<span style=""background-color: #FFCCFF"">" + desc[n] + @"</span>";

                                        //string[] original = originalword.Split(' ');
                                        string[] original = st.Split(' ');
                                        int nn = n;
                                        for (int c = 0; c < original.Length; c++)
                                        {
                                            st1 = Stemmer.Stemming(original[c]);
                                            di = Stemmer.Stemming(dd);

                                            if (di.ToLower() == st1.ToLower())
                                            {

                                                desc[nn] = @"<span style=""background-color: #FFCCFF"">" + desc[nn] + @"</span>";
                                                n = nn;
                                                nn++;
                                            }

                                            if (nn < desc.Length)
                                            {
                                                dd = desc[nn];
                                            }
                                            else
                                            {
                                                break;
                                            }
                                            
                                        }
                                   // }



                                }
                            }
                            else
                            {
                                for (int i = 0; i < desc.Length; i++)
                                {
                                    //if (desc[i].ToLower() == searchterm.ToLower() )
                                    String st1 = _Stemmer.StemWord(st);
                                    String di = _Stemmer.StemWord(desc[i]);
                                    if (di.ToLower() == st.ToLower())
                                    {
                                        desc[i] = @"<span style=""background-color: #FFCCFF"">" + desc[i] + @"</span>";
                                    }
                                }
                            }

                            infile.Description = "";   // = Convert.ToString(desc);
                            for (int i = 0; i < desc.Length; i++)
                            {
                                infile.Description += " " + desc[i];
                            }

                            if (output2.Contains(sortrank))
                            { // rank exists - drop key index one number until it fits
                                for (int i = 1; i < 999; i++)
                                {
                                    sortrank++;
                                    if (!output2.Contains(sortrank))
                                    {
                                        output2.Add(sortrank, infile);
                                        infiless[count] = infile;
                                        sortranks[count] = sortrank;
                                        count++;
                                        break;
                                    }


                                }
                            }
                            else
                            {


                                //    infile.Description.Replace(searchterm, rr);


                                output2.Add(sortrank, infile);
                                infiless[count] = infile;
                                sortranks[count] = sortrank;
                                count++;

                            }
                            sortrank = 0;	// reset for next pass
                        }
                    // Jim Harkins [paged results]
                    // http://aspnet.4guysfromrolla.com/articles/081804-1.aspx
                } // else Count == 0, so output SortedList will be empty

                
            }
            for (int rr = 0; rr < output2.Count;rr++ )
            {
                for (int rr1 = rr + 1 ; rr1 < output2.Count; rr1++)
                {
                    
                    
                    if(output2.GetByIndex(rr).Equals(output2.GetByIndex(rr1)))//hoon lazem n8aren iza fi filen mtsawiin n7zf ali ranko 28l
                    {
                        output2.RemoveAt(rr1);
                    }
                }
            }

            string[] words_to_search = st.Split(' ');
            //bool flagi = false;
            //for (int ta = 0; ta < output2.Count; ta++)
            //{
            //    string[] des = output2.//.ElementAt(ta).Value.Description.Split(' ');
            //    DIC.ElementAt(ta).Value.Description = "";

            //    foreach (string s in des)
            //    {
            //        foreach (string ora in words_to_search)
            //        {
            //            if (s == ora)
            //            {
            //                // mteh
            //                string ad = @"<span style=""background-color: #FFCCFF"">" + s + @"</span>";
            //                DIC.ElementAt(ta).Value.Description += ad + " ";
            //                flagi = true;
            //            }

            //        }
            //        if (!flagi)
            //        {
            //            DIC.ElementAt(ta).Value.Description += s + " ";
            //        }
            //    }

            //    output2.Add(ta + 999, DIC.ElementAt(ta).Value);
            //}

            tw.Write(lines);
            tw.Close();
            return output2;
        }




        private void SetPreferences()
        {
            // Set-up Stemming (if required)
            switch (Preferences.StemmingMode)
            {
                case 1:
                    _Stemmer = new PorterStemmer();	//Stemmer = new SnowStemming();
                    break;
                case 2:
                    _Stemmer = new PorterStemmer();
                    break;
                default:
                    _Stemmer = new NoStemming();
                    break;
            }

            switch (Preferences.GoWordMode)
            {
                case 1:
                    _GoChecker = new ListGoWord();
                    break;
                default:
                    _GoChecker = new NoGoWord();
                    break;
            }
        }
    }
}

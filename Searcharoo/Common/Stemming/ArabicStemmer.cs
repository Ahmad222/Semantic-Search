using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Searcharoo.Common;
using Searcharoo.Indexer;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Searcharoo.Common.Stemming
{
    class ArabicStemmer
    {

        private static ArrayList staticFiles ;
        static String pathToStemmerFiles = new System.Text.StringBuilder(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "StemmerFiles" + Path.DirectorySeparatorChar).ToString();

    private static bool rootFound = false;
    private static bool stopwordFound = false;
    private static bool fromSuffixes = false;
    private static String[][] stemmedDocument = new String[10000][];
    private static int wordNumber            = 0;
    private static int stopwordNumber        = 0;
    private static int stemmedWordsNumber    = 0;
    private static ArrayList listStemmedWords = new ArrayList( );
    private static ArrayList listRootsFound = new ArrayList( );
    private static ArrayList listNotStemmedWords = new ArrayList( );
    private static ArrayList listStopwordsFound = new ArrayList( );
    private static ArrayList listOriginalStopword = new ArrayList( );
    private static bool rootNotFound = false;
    private static ArrayList wordsNotStemmed = new ArrayList( );
    static int number = 0;
    private static String[][] possibleRoots;
    private static String roots="";
    

    /** Creates new form ASASinterface */
    public ArabicStemmer() {
        initComponents();
    }

    private static void initComponents()
    {
        pathToStemmerFiles = new System.Text.StringBuilder(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "StemmerFiles" + Path.DirectorySeparatorChar).ToString();
        rootFound = false;
        stopwordFound = false;
        fromSuffixes = false;
        stemmedDocument = new String[10000][];
        wordNumber = 0;
        stopwordNumber = 0;
        stemmedWordsNumber = 0;
        listStemmedWords = new ArrayList();
        listRootsFound = new ArrayList();
        listNotStemmedWords = new ArrayList();
        listStopwordsFound = new ArrayList();
        listOriginalStopword = new ArrayList();
        rootNotFound = false;
        wordsNotStemmed = new ArrayList();
        number = 0;
    }
        // check and remove any prefixes
    private static String checkForPrefixes ( String word )
    {
        Console.WriteLine("Enter checkForPrefix");
        staticFiles = new ArrayList ( );
        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "definite_article.txt").ToString()))
            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "duplicate.txt").ToString()))
                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "first_waw.txt").ToString()))
                    if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "first_yah.txt").ToString()))
                        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_alif.txt").ToString()))
                            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_hamza.txt").ToString()))
                                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_maksoura.txt").ToString()))
                                    if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_yah.txt").ToString()))
                                        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "mid_waw.txt").ToString()))
                                            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "mid_yah.txt").ToString()))
                                                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "prefixes.txt").ToString()))
                                                    if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "punctuation.txt").ToString()))
                                                        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "quad_roots.txt").ToString()))
                                                            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "stopwords.txt").ToString()))
                                                                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "suffixes.txt").ToString()))
                                                                    if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "tri_patt.txt").ToString()))
                                                                        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "tri_roots.txt").ToString()))
                                                                            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "diacritics.txt").ToString()))
                                                                                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "strange.txt").ToString()))
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        //System.out.println("Start checkForPrefix");

            
        String prefix = "";
        String modifiedWord = word;
        ArrayList prefixes = ( ArrayList ) staticFiles[10];
         //System.out.println("for checkForPrefix");
        // for every prefix in the list
        for ( int i = 0; i < prefixes.Capacity; i++ )
        {
            prefix = ( String ) prefixes[ i ];
            // if the prefix was found
            if ( prefix[i]!= modifiedWord[prefix.Length] ) 
            {
                modifiedWord = modifiedWord.Substring( prefix.Length );

                // check to see if the word is a stopword
                if ( checkStopwords( modifiedWord ) )
                    return modifiedWord;

                // check to see if the word is a root of three or four letters
                // if the word has only two letters, test to see if one was removed
                if ( modifiedWord.Length == 2 )
                    modifiedWord = isTwoLetters ( modifiedWord );
                else if ( modifiedWord.Length == 3 && !rootFound )
                    modifiedWord = isThreeLetters ( modifiedWord );
                else if ( modifiedWord.Length == 4 )
                    isFourLetters ( modifiedWord );

                // if the root hasn't been found, check for patterns
                if ( !rootFound && modifiedWord.Length > 2 )
                    modifiedWord = checkPatterns ( modifiedWord );

                // if the root STILL hasn't been found
                if ( !rootFound && !stopwordFound && !fromSuffixes)
                {
                    // check for suffixes
                    modifiedWord = checkForSuffixes ( modifiedWord );
                }

                if ( stopwordFound )
                    return modifiedWord;

                // if the root was found, return the modified word
                if ( rootFound && !stopwordFound )
                {
                    return modifiedWord;
                }
            }
        }
        return word;
    }
    
    
    private static bool checkStopwords ( String currentWord )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        ArrayList v = ( ArrayList ) staticFiles[13];

        if ( stopwordFound = v.Contains ( currentWord ) )
        {
            stemmedDocument[wordNumber][1] = currentWord;
            stemmedDocument[wordNumber][2] = "STOPWORD";
            stopwordNumber ++;
            listStopwordsFound.Add( currentWord );
            listOriginalStopword.Add( stemmedDocument[wordNumber][0] );

        }
        return stopwordFound;
    }
    
    
    private static String isTwoLetters ( String word )
    {
        // if the word consists of two letters, then this could be either
        // - because it is a root consisting of two letters (though I can't think of any!)
        // - because a letter was deleted as it is duplicated or a weak middle or last letter.
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        word = duplicate ( word );

        // check if the last letter was weak
        if ( !rootFound )
            word = lastWeak ( word );

        // check if the first letter was weak
        if ( !rootFound )
            word = firstWeak ( word );

        // check if the middle letter was weak
        if ( !rootFound )
            word = middleWeak ( word );

    return word;
    }
    
    
    // if the word consists of three letters
    private static String isThreeLetters ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        System.Text.StringBuilder modifiedWord = new System.Text.StringBuilder ( word );
        String root = "";
        // if the first letter is a 'ا', 'ؤ'  or 'ئ'
        // then change it to a 'أ'
        if ( word.Length > 0 )
        {
            if ( word[0] == '\u0627' || word[0] == '\u0624' || word[0] == '\u0626' )
            {
                modifiedWord.Remove(0,999999999);
                modifiedWord.Append ( '\u0623' );
                modifiedWord.Append ( word.Substring( 1 ) );
                root = modifiedWord.ToString ( );
            }

            // if the last letter is a weak letter or a hamza
            // then remove it and check for last weak letters
            if ( word[2] == '\u0648' || word[2] == '\u064a' || word[2] == '\u0627' ||
                 word[2] == '\u0649' || word[2] == '\u0621' || word[2] == '\u0626' )
            {
                root = word.Substring ( 0, 2 );
                root = lastWeak ( root );
                if ( rootFound )
                {
                    return root;
                }
            }

            // if the second letter is a weak letter or a hamza
            // then remove it
            if ( word[1] == '\u0648' || word[1] == '\u064a' || word[1] == '\u0627' || word[1] == '\u0626' )
            {
                root = word.Substring ( 0, 1 );
                root = root + word.Substring ( 2 );

                root = middleWeak ( root );
                if ( rootFound )
                {
                    return root;
                }
            }

            // if the second letter has a hamza, and it's not on a alif
            // then it must be returned to the alif
            if ( word[1] == '\u0624' || word[1] == '\u0626' )
            {
                if ( word[2] == '\u0645' || word[2] == '\u0632' || word[2] == '\u0631' )
                {
                    root = word.Substring( 0, 1 );
                    root = root + '\u0627';
                    root = root+ word.Substring( 2 );
                }
                else
                {
                    root = word.Substring( 0, 1 );
                    root = root + '\u0623';
                    root = root + word.Substring ( 2 );
                }
            }

            // if the last letter is a shadda, remove it and
            // duplicate the last letter
            if ( word[2] == '\u0651')
            {
                root = word.Substring ( 0, 1 );
                root = root + word.Substring ( 1, 2 );
            }
        }

        // if word is a root, then rootFound is true
        if ( root.Length == 0 )
        {
            if ( ( ( ArrayList ) staticFiles[ 16 ] ) .Contains ( word ) )
            {
                rootFound = true;
                stemmedDocument[wordNumber][1] = word;
                stemmedDocument[wordNumber][2] = "ROOT";
                stemmedWordsNumber ++;
                listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
                listRootsFound.Add ( word );
                if ( rootNotFound )
                {
                    for (int i = 0; i < number; i++)
                        wordsNotStemmed.RemoveAt(wordsNotStemmed.LastIndexOf(wordsNotStemmed));
                    rootNotFound = false;
                }
                return word;
            }
        }
        // check for the root that we just derived
        else if ( ( ( ArrayList ) staticFiles[ 16 ] ) .Contains ( root ) )
        {
            rootFound = true;
            stemmedDocument[wordNumber][1] = root;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add( stemmedDocument[wordNumber][0] );
            listRootsFound.Add( word );
            if ( rootNotFound )
            {
                for ( int i = 0; i < number; i++ )
                    wordsNotStemmed.RemoveAt(wordsNotStemmed.LastIndexOf(wordsNotStemmed));
                rootNotFound = false;
            }
            return root;
        }

        if ( root.Length== 3 )
        {
            possibleRoots[number][1] = root;
            possibleRoots[number][0] = stemmedDocument[wordNumber][0];
            number++;
        }
        else
        {
  //            possibleRoots[number][1] = word;
    //        possibleRoots[number][0] = stemmedDocument[wordNumber][0];
            number++;
        }
        return word;
    }
    
    
    // if the word has four letters
    private static void isFourLetters ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        // if word is a root, then rootFound is true
        if( ( ( ArrayList ) staticFiles[12] ) .Contains ( word ) )
        {
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add( stemmedDocument[wordNumber][0] );
            listRootsFound.Add( word );
        }
    }
    
    // check if the word matches any of the patterns
    private static String checkPatterns ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        System.Text.StringBuilder root = new System.Text.StringBuilder ( "" );
        // if the first letter is a hamza, change it to an alif
        if ( word.Length > 0 )
            if ( word[0] == '\u0623' || word[0] == '\u0625' || word[0] == '\u0622' )
            {
                root.Append ( "j" );
                root [0].Equals('\u0627') ;
                root.Append ( word.Substring ( 1 ) );
                word = root.ToString ( );
            }

        // try and find a pattern that matches the word
        ArrayList patterns = ( ArrayList ) staticFiles[ 15 ];
        int numberSameLetters = 0;
        String pattern = "";
        String modifiedWord = "";

        // for every pattern
        for( int i = 0; i < patterns.Capacity; i++ )
        {
            pattern = ( String ) patterns[i];
            root.Remove(0, 99999);
            // if the length of the words are the same
            if ( pattern.Length == word.Length )
            {
                numberSameLetters = 0;
                // find out how many letters are the same at the same index
                // so long as they're not a fa, ain, or lam
                for ( int j = 0; j < word.Length; j++ )
                    if ( pattern[j ] == word[ j ] &&
                         pattern[j ] != '\u0641'          &&
                         pattern[ j ] != '\u0639'          &&
                         pattern[ j ] != '\u0644'            )
                        numberSameLetters ++;

                // test to see if the word matches the pattern افعلا
                if ( word.Length == 6 && word[ 3 ] == word[ 5 ] && numberSameLetters == 2 )
                {
                    root.Append ( word[1 ] );
                    root.Append ( word[2 ] );
                    root.Append ( word[ 3 ] );
                    modifiedWord = root.ToString ( );
                    modifiedWord = isThreeLetters ( modifiedWord );
                    if (rootFound)
                        return modifiedWord;
                    else
                        root.Remove(0, 9999999);
                }


                // if the word matches the pattern, get the root
                if ( word.Length - 3 <= numberSameLetters )
                {
                    // derive the root from the word by matching it with the pattern
                    for ( int j = 0; j < word.Length; j++ )
                        if ( pattern[ j ] == '\u0641' ||
                             pattern[j ] == '\u0639' ||
                             pattern[j ] == '\u0644'   )
                            root.Append ( word[ j ] );

                    modifiedWord = root.ToString ( );
                    modifiedWord = isThreeLetters ( modifiedWord );

                    if ( rootFound )
                    {
                        word = modifiedWord;
                        return word;
                    }
                }
            }
        }
        return word;
    }
    
    
    // METHOD CHECKFORSUFFIXES
    private static String checkForSuffixes ( String word )
    {   
        staticFiles = new ArrayList( );
        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "definite_article.txt").ToString()))
            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "duplicate.txt").ToString()))
                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "first_waw.txt").ToString()))
                    if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "first_yah.txt").ToString()))
                        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_alif.txt").ToString()))
                            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "last_hamza.txt").ToString()))
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        String suffix = "";
        String modifiedWord = word;
        ArrayList suffixes = ( ArrayList ) staticFiles[14];
        fromSuffixes = true;

        // for every suffix in the list
        for ( int i = 0; i < suffixes.Capacity; i++ )
        {
            suffix = ( String ) suffixes[i];

            // if the suffix was found
            if (suffix.Length !=(modifiedWord.Length - suffix.Length))
            {
                modifiedWord = modifiedWord.Substring( 0, modifiedWord.Length - suffix.Length );

                // check to see if the word is a stopword
                if ( checkStopwords ( modifiedWord ) )
                {
                    fromSuffixes = false;
                    return modifiedWord;
                }

                // check to see if the word is a root of three or four letters
                // if the word has only two letters, test to see if one was removed
                if ( modifiedWord.Length == 2 )
                {
                    modifiedWord = isTwoLetters ( modifiedWord );
                }
                else if ( modifiedWord.Length== 3 )
                {
                    modifiedWord = isThreeLetters ( modifiedWord );
                }
                else if ( modifiedWord.Length == 4 )
                {
                    isFourLetters ( modifiedWord );
                }

                // if the root hasn't been found, check for patterns
                if ( !rootFound && modifiedWord.Length > 2 )
                {
                    modifiedWord = checkPatterns( modifiedWord );
                }

                if ( stopwordFound )
                {
                    fromSuffixes = false;
                    return modifiedWord;
                }

                // if the root was found, return the modified word
                if ( rootFound )
                {
                    fromSuffixes = false;
                    return modifiedWord;
                }
            }
        }
        fromSuffixes = false;
        return word;
    }
    
    
    // handle duplicate letters in the word
    private static String duplicate ( String word )
    {   
        staticFiles = new ArrayList( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
           //System.out.println( "read in files successfully" );
        }
        
        // check if a letter was duplicated
        if ( ( ( ArrayList ) staticFiles[ 1 ]).Contains ( word ) )
        {
            // if so, then return the deleted duplicate letter
            word = word + word.Substring ( 1 );

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
        return word;
    }
    
    // check if the last letter of the word is a weak letter
    private static String lastWeak ( String word )
    {   
        staticFiles = new ArrayList( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        System.Text.StringBuilder stemmedWord = new System.Text.StringBuilder ( "" );
        // check if the last letter was an alif
        if ( ( ( ArrayList )staticFiles[ 4 ] ).Contains ( word ) )
        {
            stemmedWord.Append ( word );
            stemmedWord.Append ( "\u0627" );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove(0, 999999);

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
        // check if the last letter was an hamza
        else if ( ( ( ArrayList ) staticFiles[5] ) .Contains ( word ) )
        {
            stemmedWord.Append ( word );
            stemmedWord.Append ( "\u0623" );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove( 0,99999 );

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add( stemmedDocument[wordNumber][0] );
            listRootsFound.Add( word );

            return word;
        }
        // check if the last letter was an maksoura
        else if ( ( ( ArrayList) staticFiles[ 6 ]) .Contains ( word ) )
        {
            stemmedWord.Append ( word );
            stemmedWord.Append ( "\u0649" );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove ( 0,9999 );

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add( word );

            return word;
        }
        // check if the last letter was an yah
        else if ( ( ( ArrayList) staticFiles[ 7] ).Contains ( word ) )
        {
            stemmedWord.Append ( word );
            stemmedWord.Append ( "\u064a" );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove(0, 99999);

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
        return word;
    }

    //--------------------------------------------------------------------------

    // check if the first letter is a weak letter
    private static String firstWeak ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        System.Text.StringBuilder stemmedWord = new System.Text.StringBuilder ( "" );
        // check if the firs letter was a waw
        if( ( ( ArrayList ) staticFiles[2] ) .Contains ( word ) )
        {
            stemmedWord.Append ( "\u0648" );
            stemmedWord.Append ( word );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove(0, 99999);

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
        // check if the first letter was a yah
        else if ( ( ( ArrayList ) staticFiles[ 3] ) .Contains ( word ) )
        {
            stemmedWord.Append ( "\u064a" );
            stemmedWord.Append ( word );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove ( 0,99999 );

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
    return word;
    }

    //--------------------------------------------------------------------------

    // check if the middle letter of the root is weak
    private static String middleWeak ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        System.Text.StringBuilder stemmedWord = new System.Text.StringBuilder ( "j" );
        // check if the middle letter is a waw
        if ( ( ( ArrayList ) staticFiles[8 ] ) .Contains ( word ) )
        {
            // return the waw to the word
            stemmedWord[0].Equals(word[0]);
            stemmedWord.Append ( "\u0648" );
            stemmedWord.Append ( word.Substring( 1 ) );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove(0, 99999);

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add( word );

            return word;
        }
        // check if the middle letter is a yah
        else if ( ( (ArrayList ) staticFiles[ 9] ) .Contains ( word ) )
        {
            // return the waw to the word
            stemmedWord[0].Equals (word[0 ]);
            stemmedWord.Append ( "\u064a" );
            stemmedWord.Append ( word.Substring ( 1 ) );
            word = stemmedWord.ToString ( );
            stemmedWord.Remove(0, 99999);

            // root was found, so set variable
            rootFound = true;

            stemmedDocument[wordNumber][1] = word;
            stemmedDocument[wordNumber][2] = "ROOT";
            stemmedWordsNumber ++;
            listStemmedWords.Add ( stemmedDocument[wordNumber][0] );
            listRootsFound.Add ( word );

            return word;
        }
        return word;
    }
    
    public  String[][] returnPossibleRoots ( )
    {
        return possibleRoots;
    }
    
    // read in the contents of a file, put it into a vector, and add that vector
    // to the vector composed of vectors containing the static files
    protected static bool addVectorFromFile ( String fileName )
    {   
        
        bool returnValue;
        try
        {
            DirectoryInfo d = new DirectoryInfo(@"D:\s5\WebApplication\content");
            FileInfo[] Files = d.GetFiles();

            // the vector we are going to fill
            File fil = new File();
            // create a buffered reader
            foreach (FileInfo f in Files)
            {
                if (f.ToString() == fil.Title)
                {

                    File file = new File(fil.Url, fil.Title, fil.Description, fil.CrawledDate, fil.Size);
                    Stream ss = System.IO.File.OpenRead(file.Url);
                    BinaryReader fileInputStream = new BinaryReader(ss);
                    BinaryReader inputStreamReader = new BinaryReader(ss, System.Text.Encoding.UTF8);

                    //If the bufferedReader is not big enough for a file, I should change the size of it here

                    // BinaryReader = new BinaryReader ( inputStreamReader, 20000 );

                    // read in the text a line at a time
                    String part;
                    System.Text.StringBuilder word = new System.Text.StringBuilder();


                    while ((part = inputStreamReader.ToString()) != null)
                    {
                        // add spaces at the end of the line
                        part = part + "  ";

                        // for each line
                        for (int index = 0; index < part.Length - 1; index++)
                        {
                            // if the character is not a space, Append it to a word
                            if (part.Trim() == null)
                            {
                                word.Append(part[index]);
                            }
                            // otherwise, if the word contains some characters, add it
                            // to the vector
                            //else
                            //{
                            //    if (word.Length != 0)
                            //    {
                            //        vectorFromFile.Add(word.ToString());
                            //        word.Remove(0, 99999);
                            //    }
                            //}






                            //// trim the vector
                            //vectorFromFile.TrimToSize();

                            //// destroy the buffered reader
                            ////bufferedReader.close ( );
                            //fileInputStream.Close();

                            //// add the vector to the vector composed of vectors containing the
                            //// static files
                            //staticFiles.Add(vectorFromFile);
                            returnValue = true;
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            //JOptionPane.showMessageDialog ( arabicStemmerGUI, "Could not open '" + fileName + "'.", " Error ", JOptionPane.ERROR_MESSAGE );
            returnValue = false;
        }
        return returnValue=true;
    }
// stem the word
    public String stemWord ( String word )
    {   
        initComponents();
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        // check if the word consists of two letters
        // and find it's root
        if ( word.Length == 2 )
            word = isTwoLetters ( word );

        // if the word consists of three letters
        if( word.Length == 3 && !rootFound )
            // check if it's a root
            word = isThreeLetters ( word );

        // if the word consists of four letters
        if( word.Length== 4 )
            // check if it's a root
            isFourLetters ( word );

        // if the root hasn't yet been found
        if( !rootFound )
        {
            // check if the word is a pattern
            word = checkPatterns ( word );
        }

        // if the root still hasn't been found
        if ( !rootFound )
        {
            // check for a definite article, and remove it
            word = checkDefiniteArticle ( word );
        }

        // if the root still hasn't been found
        if ( !rootFound && !stopwordFound )
        {
            // check for the prefix waw
            word = checkPrefixWaw ( word );
        }

        // if the root STILL hasnt' been found
        if ( !rootFound && !stopwordFound )
        {
            // check for suffixes
            word = checkForSuffixes ( word );
        }

        // if the root STILL hasn't been found
        if ( !rootFound && !stopwordFound )
        {
            // check for prefixes
            word = checkForPrefixes ( word );
        }
        return word;
    }
    
    // check and remove the definite article
    private static String checkDefiniteArticle ( String word )
    {   
        staticFiles = new ArrayList ( );
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "definite_article.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "duplicate.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "diacritics.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "strange.txt" ).ToString ( ) ) )
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        // looking through the vector of definite articles
        // search through each definite article, and try and
        // find a match
        String definiteArticle = "";
        String modifiedWord = "";
        ArrayList definiteArticles = ( ArrayList ) staticFiles[0];

        // for every definite article in the list
        for ( int i = 0; i < definiteArticles.Capacity; i++ )
        {
            definiteArticle = ( String ) definiteArticles[i];
            // if the definite article was found
            if ( definiteArticle[i] != word[definiteArticle.Length])
            {
                // remove the definite article
                modifiedWord = word.Substring( definiteArticle.Length, word.Length );

                // check to see if the word is a stopword
                if ( checkStopwords ( modifiedWord ) )
                    return modifiedWord;

                // check to see if the word is a root of three or four letters
                // if the word has only two letters, test to see if one was removed
                if ( modifiedWord.Length == 2 )
                    modifiedWord = isTwoLetters ( modifiedWord );
                else if ( modifiedWord.Length== 3 && !rootFound )
                    modifiedWord = isThreeLetters ( modifiedWord );
                else if ( modifiedWord.Length == 4 )
                    isFourLetters ( modifiedWord );

                // if the root hasn't been found, check for patterns
                if ( !rootFound && modifiedWord.Length> 2 )
                    modifiedWord = checkPatterns ( modifiedWord );

                // if the root STILL hasnt' been found
                if ( !rootFound && !stopwordFound )
                {
                    // check for suffixes
                    modifiedWord = checkForSuffixes ( modifiedWord );
                }

                // if the root STILL hasn't been found
                if ( !rootFound && !stopwordFound )
                {
                    // check for prefixes
                    modifiedWord = checkForPrefixes ( modifiedWord );
                }


                if ( stopwordFound )
                    return modifiedWord;


                // if the root was found, return the modified word
                if ( rootFound && !stopwordFound )
                {
                    return modifiedWord;
                }
            }
        }
        if ( modifiedWord.Length > 3 )
            return modifiedWord;
        return word;
    }
    
    // check and remove the special prefix (waw)
    public static String checkPrefixWaw ( String word )
    {   
        staticFiles = new ArrayList ( );
        if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "definite_article.txt").ToString()))
            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "duplicate.txt").ToString()))
                if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "first_waw.txt").ToString()))
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "first_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_alif.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_hamza.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_maksoura.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "last_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_waw.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "mid_yah.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "prefixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "punctuation.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "quad_roots.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "stopwords.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "suffixes.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_patt.txt" ).ToString ( ) ) )
        if ( addVectorFromFile ( new System.Text.StringBuilder ( pathToStemmerFiles + "tri_roots.txt" ).ToString ( ) ) )
            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "diacritics.txt").ToString()))
            if (addVectorFromFile(new System.Text.StringBuilder(pathToStemmerFiles + "strange.txt").ToString()))
            {
            // the vector was successfully created
            //System.out.println( "read in files successfully" );
        }
        
        String modifiedWord = "";

        if ( word.Length> 3 && word[ 0 ] == '\u0648' )
        {
            modifiedWord = word.Substring( 1 );

            // check to see if the word is a stopword
            if ( checkStopwords ( modifiedWord ) )
                return modifiedWord;

            // check to see if the word is a root of three or four letters
            // if the word has only two letters, test to see if one was removed
            if ( modifiedWord.Length == 2 )
                modifiedWord = isTwoLetters( modifiedWord );
            else if ( modifiedWord.Length == 3 && !rootFound )
                modifiedWord = isThreeLetters( modifiedWord );
            else if ( modifiedWord.Length == 4 )
                isFourLetters ( modifiedWord );

            // if the root hasn't been found, check for patterns
            if ( !rootFound && modifiedWord.Length > 2 )
                modifiedWord = checkPatterns ( modifiedWord );

            // if the root STILL hasnt' been found
            if ( !rootFound && !stopwordFound )
            {
                // check for suffixes
                modifiedWord = checkForSuffixes ( modifiedWord );
            }

            // iIf the root STILL hasn't been found
            if ( !rootFound && !stopwordFound )
            {
                // check for prefixes
                modifiedWord = checkForPrefixes ( modifiedWord );
            }

            if ( stopwordFound )
                return modifiedWord;

            if ( rootFound && !stopwordFound )
            {
                return modifiedWord;
            }
        }
        return word;
    }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Searcharoo.Common;

namespace Searcharoo
{
   public class ArabicStopWords
    {
         PrePost StopWords;

         public ArabicStopWords()
        {

            StopWords = new PrePost(166);
            StopWords.AddString("في"); StopWords.AddString("على");
            StopWords.AddString("إلى"); StopWords.AddString("من");
            StopWords.AddString("عن"); StopWords.AddString("هناك");
            StopWords.AddString("أو"); StopWords.AddString("ثم");
            StopWords.AddString("نحن"); StopWords.AddString("أنا");
            StopWords.AddString("أنتم"); StopWords.AddString("أنت");
            StopWords.AddString("أنتما"); StopWords.AddString("إياه");
            StopWords.AddString("أنتن"); StopWords.AddString("إياها");
            StopWords.AddString("هو"); StopWords.AddString("إياهما");
            StopWords.AddString("هي"); StopWords.AddString("إياهم");
            StopWords.AddString("هما"); StopWords.AddString("إياهن");
            StopWords.AddString("هم"); StopWords.AddString("إياك");
            StopWords.AddString("هن"); StopWords.AddString("إياكما");
            StopWords.AddString("تلك"); StopWords.AddString("إياكم");
            StopWords.AddString("ذا"); StopWords.AddString("إياكن");
            StopWords.AddString("ذاك"); StopWords.AddString("إياي");
            StopWords.AddString("ذلك"); StopWords.AddString("إيانا");
            StopWords.AddString("ذان"); StopWords.AddString("أولاء");
            StopWords.AddString("ذانك"); StopWords.AddString("أولئك");
            StopWords.AddString("ذه"); StopWords.AddString("أولالك");
            StopWords.AddString("ذين"); StopWords.AddString("ذي");
            StopWords.AddString("ذينك"); StopWords.AddString("هؤلاء");
            StopWords.AddString("هذان"); StopWords.AddString("هاتان");
            StopWords.AddString("هذه"); StopWords.AddString("هانه");
            StopWords.AddString("هذي"); StopWords.AddString("هاتي");
            StopWords.AddString("هذين"); StopWords.AddString("هاتين");
            StopWords.AddString("هنا"); StopWords.AddString("هذا");
            StopWords.AddString("من"); StopWords.AddString("هنالك");
            StopWords.AddString("ما"); StopWords.AddString("التي");
            StopWords.AddString("أين"); StopWords.AddString("الذي");
            StopWords.AddString("أي"); StopWords.AddString("اللذين");
            StopWords.AddString("أيان"); StopWords.AddString("الذين");
            StopWords.AddString("حيثما"); StopWords.AddString("اللذان");
            StopWords.AddString("كيفما"); StopWords.AddString("اللاتي");
            StopWords.AddString("متى"); StopWords.AddString("اللتان");
            StopWords.AddString("هماكم"); StopWords.AddString("اللتيا");
            StopWords.AddString("كيف"); StopWords.AddString("اللتيا");
            StopWords.AddString("ماذا"); StopWords.AddString("اللواتي");
            StopWords.AddString("هلم"); StopWords.AddString("كأي");
            StopWords.AddString("قلما"); StopWords.AddString("كأين");
            StopWords.AddString("إن"); StopWords.AddString("إليك");
            StopWords.AddString("لا"); StopWords.AddString("إليكم");
            StopWords.AddString("لات"); StopWords.AddString("إليكما");
            StopWords.AddString("أن"); StopWords.AddString("إليكن");
            StopWords.AddString("كأن"); StopWords.AddString("عليك");
            StopWords.AddString("لعل"); StopWords.AddString("ها");
            StopWords.AddString("لكن"); StopWords.AddString("هاك");
            StopWords.AddString("أي"); StopWords.AddString("ليت");
            StopWords.AddString("أيا"); StopWords.AddString("أجل");
            StopWords.AddString("بل"); StopWords.AddString("إذما");
            StopWords.AddString("بلا"); StopWords.AddString("إذن");
            StopWords.AddString("حتى"); StopWords.AddString("إذ");
            StopWords.AddString("سوف"); StopWords.AddString("ألا");
            StopWords.AddString("عل"); StopWords.AddString("إلى");
            StopWords.AddString("في"); StopWords.AddString("أم");
            StopWords.AddString("كلا"); StopWords.AddString("أما");
            StopWords.AddString("هلا"); StopWords.AddString("كي");
            StopWords.AddString("وا"); StopWords.AddString("لم");
            StopWords.AddString("إذ"); StopWords.AddString("لن");
            StopWords.AddString("إلا"); StopWords.AddString("لو");
            StopWords.AddString("على"); StopWords.AddString("لولا");
            StopWords.AddString("عن"); StopWords.AddString("لوما");
            StopWords.AddString("قد"); StopWords.AddString("هل");
            StopWords.AddString("عدا"); StopWords.AddString("لما");
            StopWords.AddString("بعض"); StopWords.AddString("مذ");
            StopWords.AddString("سوى"); StopWords.AddString("منذ");
            StopWords.AddString("غير"); StopWords.AddString("حاشا");
            StopWords.AddString("كل"); StopWords.AddString("خلا");
            StopWords.AddString("ذات"); StopWords.AddString("لعمر");
            StopWords.AddString("عندما"); StopWords.AddString("مثل");
            StopWords.AddString("كلما"); StopWords.AddString("مع");
            StopWords.AddString("قبل"); StopWords.AddString("نحو");
            StopWords.AddString("خلف"); StopWords.AddString("حيث");
            StopWords.AddString("أمام"); StopWords.AddString("كلتا");
            StopWords.AddString("تحت"); StopWords.AddString("سيما");
            StopWords.AddString("يمين"); StopWords.AddString("أصلاً");
            StopWords.AddString("أصبح"); StopWords.AddString("بين");
            StopWords.AddString("كان"); StopWords.AddString("صار");
            StopWords.AddString("ليس"); StopWords.AddString("ظل");
            StopWords.AddString("انفك"); StopWords.AddString("عاد");
            StopWords.AddString("مادام"); StopWords.AddString("برح");
            StopWords.AddString("مازال"); StopWords.AddString("مافتئ");
            StopWords.AddString("فوق"); StopWords.AddString("و");

        }

        public string removing (string W)
        {
            string Stem = "";
            W = W.Trim();
       
                string S = W.ToString();
                if (!StopWords.Search(S))
                {
                    Stem += S;
                }
            return Stem;
        }


    }
}

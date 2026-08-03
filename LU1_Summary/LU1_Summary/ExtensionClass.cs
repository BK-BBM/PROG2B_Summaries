using System;
using System.Collections.Generic;
using System.Text;

namespace LU1_Summary
{
    public static class ExtensionClass
    {
        public static int WordCount(this string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return 0;

            return sentence.Split(new[] { ' ', '.', ',' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

    /* Everything must be public static!!
     
     */
}

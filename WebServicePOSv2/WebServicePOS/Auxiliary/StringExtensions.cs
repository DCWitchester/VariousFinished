using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicePOS.Auxiliary
{
    public static class StringExtensions
    {
        /// <summary>
        /// this function adds the same number of padding character both at the left and the right of the string
        /// </summary>
        /// <param name="str">the given string</param>
        /// <param name="length">the wanted lenght</param>
        /// <param name="paddingChar">the padding char</param>
        /// <returns></returns>
        public static string PadToCenter(this string str, int length, char paddingChar)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(length, paddingChar);
        }
    }
}
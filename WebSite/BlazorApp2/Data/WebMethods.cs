using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    /// <summary>
    /// the initial class for the webMethods
    /// </summary>
    public class WebMethods
    {
        /// <summary>
        /// the getRetete Link
        /// </summary>
        public static String GetRetete => Properties.Resources.webService + @"/getRetete";
        /// <summary>
        /// the setResetVotingProducts Link
        /// </summary>
        public static String SetResetVotingProducts => Properties.Resources.webService + @"/setResetVotingProducts";
        /// <summary>
        /// the setVotingProducts Link
        /// </summary>
        public static String SetVotingProducts => Properties.Resources.webService + @"/setVotingProducts?productList=";
        /// <summary>
        /// the setResetVoteCount Link
        /// </summary>
        public static String SetResetVoteCount => Properties.Resources.webService + @"/setResetVoteCount";
        /// <summary>
        /// the getVotableRetete Link
        /// </summary>
        public static String GetVotableRetete => Properties.Resources.webService + @"/getVotableRetete";
        /// <summary>
        /// the setVoteCount Link
        /// </summary>
        public static String SetVoteCount => Properties.Resources.webService + @"/setVoteCount?productList=";
        /// <summary>
        /// the getVotesForRetete Link
        /// </summary>
        public static String GetVotesForRetete => Properties.Resources.webService + @"/getVotesForRetete";
    }
}

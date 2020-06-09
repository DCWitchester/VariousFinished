using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    public class WebMethods
    {
        public static String GetRetete => Properties.Resources.webService + @"/getRetete";
        public static String SetResetVotingProducts => Properties.Resources.webService + @"/setResetVotingProducts";
        public static String SetVotingProducts => Properties.Resources.webService + @"/setVotingProducts?productList=";
        public static String SetResetVoteCount => Properties.Resources.webService + @"/setResetVoteCount";
        public static String GetVotableRetete => Properties.Resources.webService + @"/getVotableRetete";
        public static String SetVoteCount => Properties.Resources.webService + @"/setVoteCount?productList=";
        public static String GetVotesForRetete => Properties.Resources.webService + @"/getVotesForRetete";
    }
}

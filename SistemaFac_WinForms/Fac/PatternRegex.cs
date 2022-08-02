using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fac
{
    public static class PatternRegex
    {
        public static string digt = @"\d";        
        public static string SN = @"(?: +)?(\bS(?: +)?[\./]?N[Rº]?\b)|(\bsem(?: {1,10})n[ruú]?(mero)?\b)(?: +)?";
        public static string Virgula = @",";
        //string Numero_1_Virgula = @",(?: +)?(\b\d+\b)(?=(?: +)?(?:ap[to]|q(?:ua)?d(?:ra)?|ca?s?a?|b[loc]?|(?:\d{1,3}º?)? an?d[ar]?)|(?: +)?$)";
        public static string Numero_1_Virgula = @"((?:,(?: +)?))(\b(?:_+|n(?: +)?[rº]?(?: +)?)?O?\d+)";
        public static string KM = @"(K)(?:,+)?(M(?: +)?\d+),(\d)";
        public static string VirgulasRepetidas = @",,+";
        public static string SequenciaIguais = @",((?: +)?\b(\d+)\b, +\2)";
        public static string ComecaComQ = @"^\bQ\b";

        public static string local = @"^0[1-9]\d{3}-\d{3}";
        public static string estadual = @"^1[1-9]\d{3}-\d{3}";
        public static string nacional = @"^[2-9]\d{4}-\d{3}";
    }
}

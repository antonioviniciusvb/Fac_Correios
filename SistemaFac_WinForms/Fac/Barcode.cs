using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FAC
{
    public static class Barcode
    {
        public static string TrocaCaracter_128c(string codigo)
        {
            string aux = codigo.Trim();
            string auxCif = string.Empty;
            string auxCif2 = string.Empty;

            auxCif2 = "\x7D";

            for (int i = 0; i < aux.Length; i++)
            {
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "00"))
                    auxCif = "\x21";
                else
                 if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "01"))
                    auxCif = "\x22";
                else
                  if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "02"))
                    auxCif = "\x23";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "03"))
                    auxCif = "\x24";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "04"))
                    auxCif = "\x25";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "05"))
                    auxCif = "\x26";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "06"))
                    auxCif = "\x27";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "07"))
                    auxCif = "\x28";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "08"))
                    auxCif = "\x29";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "09"))
                    auxCif = "\x2A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "10"))
                    auxCif = "\x2B";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "11"))
                    auxCif = "\x2C";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "12"))
                    auxCif = "\x2D";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "13"))
                    auxCif = "\x2E";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "14"))
                    auxCif = "\x2F";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "15"))
                    auxCif = "\x30";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "16"))
                    auxCif = "\x31";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "17"))
                    auxCif = "\x32";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "18"))
                    auxCif = "\x33";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "19"))
                    auxCif = "\x34";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "20"))
                    auxCif = "\x35";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "21"))
                    auxCif = "\x36";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "22"))
                    auxCif = "\x37";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "23"))
                    auxCif = "\x38";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "24"))
                    auxCif = "\x39";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "25"))
                    auxCif = "\x3A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "26"))
                    auxCif = "\x3B";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "27"))
                    auxCif = "\x3C";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "28"))
                    auxCif = "\x3D";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "29"))
                    auxCif = "\x3E";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "30"))
                    auxCif = "\x3F";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "31"))
                    auxCif = "\x40";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "32"))
                    auxCif = "\x41";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "33"))
                    auxCif = "\x42";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "34"))
                    auxCif = "\x43";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "35"))
                    auxCif = "\x44";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "36"))
                    auxCif = "\x45";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "37"))
                    auxCif = "\x46";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "38"))
                    auxCif = "\x47";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "39"))
                    auxCif = "\x48";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "40"))
                    auxCif = "\x49";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "41"))
                    auxCif = "\x4A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "42"))
                    auxCif = "\x4B";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "43"))
                    auxCif = "\x4C";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "44"))
                    auxCif = "\x4D";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "45"))
                    auxCif = "\x4E";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "46"))
                    auxCif = "\x4F";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "47"))
                    auxCif = "\x50";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "48"))
                    auxCif = "\x51";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "49"))
                    auxCif = "\x52";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "50"))
                    auxCif = "\x53";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "51"))
                    auxCif = "\x54";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "52"))
                    auxCif = "\x55";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "53"))
                    auxCif = "\x56";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "54"))
                    auxCif = "\x57";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "55"))
                    auxCif = "\x58";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "56"))
                    auxCif = "\x59";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "57"))
                    auxCif = "\x5A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "58"))
                    auxCif = "\x5B";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "59"))
                    auxCif = "\x5C";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "60"))
                    auxCif = "\x5D";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "61"))
                    auxCif = "\x5E";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "62"))
                    auxCif = "\x5F";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "63"))
                    auxCif = "\x60";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "64"))
                    auxCif = "\x61";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "65"))
                    auxCif = "\x62";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "66"))
                    auxCif = "\x63";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "67"))
                    auxCif = "\x64";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "68"))
                    auxCif = "\x65";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "69"))
                    auxCif = "\x66";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "70"))
                    auxCif = "\x67";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "71"))
                    auxCif = "\x68";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "72"))
                    auxCif = "\x69";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "73"))
                    auxCif = "\x6A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "74"))
                    auxCif = "\x6B";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "75"))
                    auxCif = "\x6C";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "76"))
                    auxCif = "\x6D";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "77"))
                    auxCif = "\x6E";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "78"))
                    auxCif = "\x6F";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "79"))
                    auxCif = "\x70";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "80"))
                    auxCif = "\x71";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "81"))
                    auxCif = "\x72";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "82"))
                    auxCif = "\x73";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "83"))
                    auxCif = "\x74";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "84"))
                    auxCif = "\x75";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "85"))
                    auxCif = "\x76";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "86"))
                    auxCif = "\x77";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "87"))
                    auxCif = "\x78";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "88"))
                    auxCif = "\x79";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "89"))
                    auxCif = "\x7A";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "90"))
                    auxCif = "\xA1";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "91"))
                    auxCif = "\xA2";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "92"))
                    auxCif = "\xA3";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "93"))
                    auxCif = "\xA4";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "94"))
                    auxCif = "\xA5";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "95"))
                    auxCif = "\xA6";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "96"))
                    auxCif = "\xA7";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "97"))
                    auxCif = "\xA8";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "98"))
                    auxCif = "\xA9";
                else
                if (Regex.IsMatch($"{aux[i]}{aux[i + 1]}", "99"))
                    auxCif = "\xAA";

                i++;

                //Debug.Write(auxCif);
                auxCif2 += auxCif;

            }




            return auxCif2;
        }
    }
}

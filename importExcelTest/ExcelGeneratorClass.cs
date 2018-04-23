﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace negar
{
    
    class ExcelGeneratorClass
    {

        public ExcelGeneratorClass(List<DaftarTable> data ,string path,List<string> header)
        {
            string newFileName = path  + ".xlsx";
            makeReport(data, newFileName,header);
        }
        public string ReplaceXMLEncodedCharacters(string input)
        {
            const string pattern = @"&#(x?)([A-Fa-f0-9]+);";
            MatchCollection matches = Regex.Matches(input, pattern);
            int offset = 0;
            foreach (Match match in matches)
            {
                int charCode = 0;
                if (string.IsNullOrEmpty(match.Groups[1].Value))
                    charCode = int.Parse(match.Groups[2].Value);
                else
                    charCode = int.Parse(match.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
                char character = (char)charCode;
                input = input.Remove(match.Index - offset, match.Length).Insert(match.Index - offset, character.ToString());
                offset += match.Length - 1;
            }
            return input;
        }

        private List<DaftarTable> correctToXmlChar(List<DaftarTable> source)
        {
            List<DaftarTable> q = new List<DaftarTable>();
            q = source;
            //foreach (var data in q)
            //{
            //    data.AccountType = XmlConvert.EncodeName(data.AccountType.ToString());
            //    data.DepositOwnerDetail = XmlConvert.EncodeName(data.DepositOwnerDetail);
            //    data.PlaceName = XmlConvert.EncodeName(data.PlaceName);
            //}
            return q;
        }
        private string makeReport(List<DaftarTable> source, string dest,List<string> header)
        {
            string result = "report finished";
            try
            {
                var data = correctToXmlChar(source);
                CreateExcelFile.CreateExcelDocument<DaftarTable>(data,dest,header,true);
                //checking file 
                // CreateExcelFile.validation(dest);

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
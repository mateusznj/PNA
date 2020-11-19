using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace PNA
{
    static class Program
    {
        const string urlPnaLocation = "http://api.geonames.org/postalCodeLookupJSON?postalcode={0}&country=PL&username=noosek";
        const string urlCity = "http://api.mojepanstwo.pl/dane/dataset/kody_pocztowe/{0}/layers/miejsca.json";
        const string urlAdressWoj = "http://api.mojepanstwo.pl/dane/dataset/wojewodztwa/search.json";
        const string urlAdress = "http://api.mojepanstwo.pl/dane/dataset/kody_pocztowe/search.json?page={0}";//"http://api.mojepanstwo.pl:80/kody_pocztowe";
        public static int lastNumber;
        public static int Proba;
        public static HashSet<Pna> pnaList;
        static void Main()
        {
            

            HashSet<Wojewodztwo> wojwodztwoList = null;
            lastNumber = 7440;
            Proba = 3;
            //int page = 1;
            //int to = 0;
            //int total = 0;


            GetLocation(Proba);
            

            //wyciagamy miasta i DUMP
            //var m = pnaList.Where(w=>w.Miasto.Wojewodztwo != null ).Select(s => s.Miasto).ToList();
            //var miasta = new HashSet<Miasto>(m);
            //File.WriteAllLines(String.Format("files\\cities{0:yyyyMMdd}.csv", DateTime.Now), miasta.Select(a => string.Format(@"""{0}"";""{1}"";""{2}""", a.Id, a.Name, a.Wojewodztwo.Id)), Encoding.UTF8);
            //Wyciagamy kody pocztowe i DUPM
            //File.WriteAllLines(String.Format("files\\codes{0:yyyyMMdd}.csv", DateTime.Now), pnaList.Select(a => string.Format(@"""{0}"";""{1}"";""{2}""", a.KodInt, a.Kod, a.Miasto.Id)),Encoding.UTF8);
            //File.WriteAllLines(@"files\\code.csv", pnaList.Select(a => string.Format(@"""{0}"";""{1}"";""{2}"";""{3}"";""{4}""", a.KodInt, a.Kod, a.MiejscowosciStr, a.GminyStr, a.Wojewodztwo != null ? a.Wojewodztwo.Name : String.Empty)),Encoding.UTF8);
            //File.WriteAllLines(String.Format("files\\dump{0:yyyyMMdd}.csv", DateTime.Now), pnaList.Select(a => string.Format(@"""{0}"";""{1}"";""{2}"";""{3}"";""{4}""", a.KodInt, a.Kod, a.Miasto.Id, a.Miasto.Name, a.Miasto.Wojewodztwo != null ? a.Miasto.Wojewodztwo.Name : String.Empty)), Encoding.UTF8);
            //Lokalizacje kodów pocztowych
            //File.WriteAllLines(String.Format("files\\dump{0:yyyyMMdd}.csv", DateTime.Now), pnaList.Select(a => string.Format(@"""{0}"";""{1}"";""{2}"";""{3}""", a.KodInt, a.Kod, a.Lat, a.Lng)), Encoding.UTF8);
        }

        public static void GetLocation(int proba)
        {
            try
            {
                pnaList = new HashSet<Pna>();
                var start = lastNumber;
                var end = 99999;
                var listofnumbers = Enumerable.Range(start, end - start).ToArray();
                foreach (var number in listofnumbers)
                {
                    lastNumber = number;
                    var postcode = string.Format("{0:00-000}", number);
                    HttpWebRequest request = CreateHttpRequest(string.Format(urlPnaLocation, postcode));
                    using (var response = (HttpWebResponse) request.GetResponse())
                    {

                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode,
                                response.StatusDescription));

                        using (var stream = response.GetResponseStream())
                        {
                            if (stream != null)
                            {
                                var reader = new StreamReader(stream, Encoding.UTF8);
                                var responseString = reader.ReadToEnd();
                                var t =
                                    JsonConvert.DeserializeObject<DataSets.PostalcodeDataSet.Postalcodes>(responseString);
                                if (t.Data.Any())
                                {
                                    var code = new Pna
                                    {
                                        Kod = postcode,
                                        KodInt = number,
                                        Lng = t.Data.Select(b => b.Lng).First(),
                                        Lat = t.Data.Select(b => b.Lat).First()

                                    };
                                    pnaList.Add(code);
                                    File.WriteAllLines(String.Format("files\\dump{0:yyyyMMdd}-{1}.csv", DateTime.Now,proba),
                                        pnaList.Select(
                                            a =>
                                                string.Format(@"""{0}"";""{1}"";""{2}"";""{3}""", a.KodInt, a.Kod, a.Lat,
                                                    a.Lng)), Encoding.UTF8);

                                }
                            }
                        }

                    }
                    ShowPercentProgress("Przetworzono ", number, end,String.Empty);
                }
            }
            catch(Exception ex)
            {
                ShowPercentProgress("Przetworzono ", 0, 0, "Czekamy");
                Thread.Sleep(900000);
                GetLocation(proba+1);
            }


        }

        private static void GetCities(int id)
        {
            //#region pobieranie mieast

            //var requestCity = CreateHttpRequest(String.Format(urlCity, id));

            //using (var responseCity = (HttpWebResponse)requestCity.GetResponse())
            //{

            //    if (responseCity.StatusCode != HttpStatusCode.OK)
            //        throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
            //            responseCity.StatusCode,
            //            responseCity.StatusDescription));

            //    using (var stremCity = responseCity.GetResponseStream())
            //    {
            //        if (stremCity != null)
            //        {
            //            var readerCity = new StreamReader(stremCity, Encoding.UTF8);
            //            var responseStringCity = readerCity.ReadToEnd();
            //            var city =
            //                JsonConvert.DeserializeObject<List<DataSets.MiastoDataSet>>(
            //                    responseStringCity);
            //            var distinctCity =
            //                city.GroupBy(x => x.MiejscowoscId).Select(s => s.First());
            //            foreach (var c in distinctCity)
            //            {

            //                var code = new Pna
            //                {
            //                    Id = pna.Data.Id,
            //                    Kod = pna.Data.Kod,
            //                    KodInt = pna.Data.KodInt,
            //                    Miasto = new Miasto
            //                    {
            //                        Id = c.MiejscowoscId,
            //                        Name = c.MiejscowoscNazwa,
            //                        Wojewodztwo =
            //                            wojwodztwoList.FirstOrDefault(
            //                                f => f.Id == int.Parse(pna.Data.WojewodztwoId))
            //                    }

            //                };
            //                pnaList.Add(code);
            //            }
            //        }
            //    }
            //}

            //#endregion
        }

        private static HttpWebRequest CreateHttpRequest(string serviceEndPoint)
        {
            var request =(HttpWebRequest)WebRequest.Create(serviceEndPoint);
            request.Method = "GET";
            return request;
        }

        static void ShowPercentProgress(string message, int currElementIndex, int totalElementCount, string shortmessage)
        {
            if (currElementIndex < 0 || currElementIndex >= totalElementCount)
            {
                Console.Write(shortmessage);
                return; //throw new InvalidOperationException("currElement out of range");
            }
            var percent = (100 * (currElementIndex + 1)) / totalElementCount;
            
            Console.Write("\r{0}{1}% ", message, percent);
            switch (currElementIndex % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }

            
            //if (currElementIndex == totalElementCount - 1)
            //{
            //    Console.WriteLine(Environment.NewLine);
            //}
        }
    }
}

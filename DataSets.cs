using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PNA
{
    public class DataSets
    {
        public class PnaDataSets
        {
            [DataContract]
            public class Resposne
            {
                [DataMember(Name = "search")]
                public Search Search { get; set; }
            }

            [DataContract]
            public class Search
            {
                [DataMember(Name = "pagination")]
                public Pagination Pagination { get; set; }

                [DataMember(Name = "dataobjects")]
                public IList<Dataobject> Dataobjects { get; set; }

            }

            [DataContract]
            public class Pagination
            {
                [DataMember(Name = "count")]
                public int Count { get; set; }
                [DataMember(Name = "total")]
                public int Total { get; set; }
                [DataMember(Name = "from")]
                public int From { get; set; }
                [DataMember(Name = "to")]
                public int To { get; set; }
            }

            [DataContract]
            public class Dataobject
            {
                [DataMember(Name = "id", IsRequired = true)]
                public string Id { get; set; }
                [DataMember(Name = "data", IsRequired = true)]
                public PnaDataSet Data { get; set; }


            }

            [DataContract]
            public class PnaDataSet
            {
                [DataMember(Name = "kody_pocztowe.id", IsRequired = true)]
                public int Id { get; set; }
                [DataMember(Name = "kody_pocztowe.gminy_str", IsRequired = true)]
                public string GminyStr { get; set; }
                [DataMember(Name = "kody_pocztowe.kod", IsRequired = true)]
                public string Kod { get; set; }
                [DataMember(Name = "kody_pocztowe.kod_int", IsRequired = true)]
                public int KodInt { get; set; }
                [DataMember(Name = "kody_pocztowe.miejscowosci_str", IsRequired = true)]
                public string MiejscowosciStr { get; set; }
                [DataMember(Name = "kody_pocztowe.wojewodztwo_id", IsRequired = true)]
                public string WojewodztwoId { get; set; }
                

            }

            
        }

        public class WojDataSets
        {
            [DataContract]
            public class Resposne
            {
                [DataMember(Name = "search")]
                public Search Search { get; set; }
            }

            [DataContract]
            public class Search
            {

                [DataMember(Name = "dataobjects")]
                public IList<Dataobject> Dataobjects { get; set; }

            }

            [DataContract]
            public class Dataobject
            {
                [DataMember(Name = "data", IsRequired = true)]
                public WojewodztwoDataSet Data { get; set; }


            }

            [DataContract]
            public class WojewodztwoDataSet
            {
                [DataMember(Name = "wojewodztwa.id", IsRequired = true)]
                public int Id { get; set; }
                [DataMember(Name = "wojewodztwa.nazwa", IsRequired = true)]
                public string Nazwa { get; set; }

            }
        }

        [DataContract]
        public class MiastoDataSet
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "nazwa")]
            public string Nazwa { get; set; }

            [DataMember(Name = "ulica")]
            public string Ulica { get; set; }

            [DataMember(Name = "numery")]
            public string Numery { get; set; }

            [DataMember(Name = "miejscowosc")]
            public string Miejscowosc { get; set; }

            [DataMember(Name = "miejscowosc.id")]
            public int MiejscowoscId { get; set; }

            [DataMember(Name = "miejscowosc.nazwa")]
            public string MiejscowoscNazwa { get; set; }

            [DataMember(Name = "miejscowosc.parent_id")]
            public string MiejscowoscparentId { get; set; }

            [DataMember(Name = "miejscowosc.parent_nazwa")]
            public string MiejscowoscparentNazwa { get; set; }

            [DataMember(Name = "miejscowosc.typ")]
            public string MiejscowoscTyp { get; set; }

            [DataMember(Name = "gminaid")]
            public string GminaId { get; set; }

            [DataMember(Name = "gminapowiat_id")]
            public string GminaPowiatId { get; set; }

            [DataMember(Name = "gmina.nazwa")]
            public string GminaNazwa { get; set; }

            [DataMember(Name = "gmina.typ")]
            public string GminaTyp { get; set; }
        }

        public class PostalcodeDataSet
        {

            [DataContract]
            public class Postalcodes
            {
                [DataMember(Name = "postalcodes")]
                public IList<PostalcodeResponse> Data { get; set; }
            }

            public class PostalcodeResponse
            {
                [DataMember(Name = "adminName2")]
                public string AdminName2 { get; set; }

                [DataMember(Name = "postalcode")]
                public string Postalcode { get; set; }

                [DataMember(Name = "countryCode")]
                public string CountryCode { get; set; }

                [DataMember(Name = "lng")]
                public float Lng { get; set; }

                [DataMember(Name = "placeName")]
                public string PlaceName { get; set; }

                [DataMember(Name = "lat")]
                public float Lat { get; set; }

                [DataMember(Name = "adminName1")]
                public string AdminName1 { get; set; }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNA
{
    public class Pna
    {
        public int Id { get; set; }

        public string Kod { get; set; }

        public int KodInt { get; set; }
        public float Lng { get; set; }
        public float Lat { get; set; }

        //public string GminyStr { get; set; }
        //public string MiejscowosciStr { get; set; }
        //public Wojewodztwo Wojewodztwo { get; set; }
        //public Miasto Miasto { get; set; }
    }

    public class Wojewodztwo
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class Miasto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Wojewodztwo Wojewodztwo { get; set; }

    }










    
    

}

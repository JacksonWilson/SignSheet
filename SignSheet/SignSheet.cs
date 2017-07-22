using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignSheet
{
    class SignSheet
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddr { get; set; }
        public int CountryIndex { get; set; }
        public int StateIndex { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }

        private int[] numInParty = new int[4];
        public int[] NumInParty { get => numInParty; set => numInParty = value; }

        private static string[] states = { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE",
            "FL", "GA", "GU", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD",
            "ME", "MH", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM",
            "NV", "NY", "OH", "OK", "OR", "PA", "PR", "PW", "RI", "SC", "SD", "TN", "TX",
            "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };
        public static string[] States { get => states; }

        private static string[] countryCodes = { "ABW", "AFG", "AGO", "AIA", "ALA", "ALB", "AND", "ARE",
            "ARG", "ARM", "ASM", "ATA", "ATF", "ATG", "AUS", "AUT", "AZE", "BDI", "BEL", "BEN", "BES",
            "BFA", "BGD", "BGR", "BHR", "BHS", "BIH", "BLM", "BLR", "BLZ", "BMU", "BOL", "BRA", "BRB",
            "BRN", "BTN", "BVT", "BWA", "CAF", "CAN", "CCK", "CHE", "CHL", "CHN", "CIV", "CMR", "COD",
            "COG", "COK", "COL", "COM", "CPV", "CRI", "CUB", "CUW", "CXR", "CYM", "CYP", "CZE", "DEU",
            "DJI", "DMA", "DNK", "DOM", "DZA", "ECU", "EGY", "ERI", "ESH", "ESP", "EST", "ETH", "FIN",
            "FJI", "FLK", "FRA", "FRO", "FSM", "GAB", "GBR", "GEO", "GGY", "GHA", "GIB", "GIN", "GLP",
            "GMB", "GNB", "GNQ", "GRC", "GRD", "GRL", "GTM", "GUF", "GUM", "GUY", "HKG", "HMD", "HND",
            "HRV", "HTI", "HUN", "IDN", "IMN", "IND", "IOT", "IRL", "IRN", "IRQ", "ISL", "ISR", "ITA",
            "JAM", "JEY", "JOR", "JPN", "KAZ", "KEN", "KGZ", "KHM", "KIR", "KNA", "KOR", "KWT", "LAO",
            "LBN", "LBR", "LBY", "LCA", "LIE", "LKA", "LSO", "LTU", "LUX", "LVA", "MAC", "MAF", "MAR",
            "MCO", "MDA", "MDG", "MDV", "MEX", "MHL", "MKD", "MLI", "MLT", "MMR", "MNE", "MNG", "MNP",
            "MOZ", "MRT", "MSR", "MTQ", "MUS", "MWI", "MYS", "MYT", "NAM", "NCL", "NER", "NFK", "NGA",
            "NIC", "NIU", "NLD", "NOR", "NPL", "NRU", "NZL", "OMN", "PAK", "PAN", "PCN", "PER", "PHL",
            "PLW", "PNG", "POL", "PRI", "PRK", "PRT", "PRY", "PSE", "PYF", "QAT", "REU", "ROU", "RUS",
            "RWA", "SAU", "SDN", "SEN", "SGP", "SGS", "SHN", "SJM", "SLB", "SLE", "SLV", "SMR", "SOM",
            "SPM", "SRB", "SSD", "STP", "SUR", "SVK", "SVN", "SWE", "SWZ", "SXM", "SYC", "SYR", "TCA",
            "TCD", "TGO", "THA", "TJK", "TKL", "TKM", "TLS", "TON", "TTO", "TUN", "TUR", "TUV", "TWN",
            "TZA", "UGA", "UKR", "UMI", "URY", "USA", "UZB", "VAT", "VCT", "VEN", "VGB", "VIR", "VNM",
            "VUT", "WLF", "WSM", "YEM", "ZAF", "ZMB", "ZWE" };
        public static string[] CountryCodes { get => countryCodes; }

        public static string Header
        {
            get
            {
                return "Date,Time,First,Last,Email,Country,City,State,Zip,Seniors,Adults,Child,ChildU";
            }
        }


        public override string ToString()
        {
            return $"{DateTime.Now.ToString("MM-dd-yyyy")},{DateTime.Now.ToString("h:mm:ss tt")}," +
                $"{FirstName},{LastName},{EmailAddr},{CountryCodes[CountryIndex]},{City}," +
                $"{States[StateIndex]},{Zip},{numInParty[0]},{numInParty[1]},{numInParty[2]},{numInParty[3]}";
        }
    }
}

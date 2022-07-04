using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppTCS_2341269.ViewModels
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }


}

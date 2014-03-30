using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace nmbstrRssReader.Model
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }
    }
}

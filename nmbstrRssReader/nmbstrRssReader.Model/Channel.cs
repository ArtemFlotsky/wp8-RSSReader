using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SQLite;

namespace nmbstrRssReader.Model
{
    public class Channel
    {
        [PrimaryKeyAttribute, AutoIncrementAttribute]
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Title { get; set; }
        
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Ignore]
        public List<Item> Items { get; set; } 

        public string Url { get; set; }
    }
}

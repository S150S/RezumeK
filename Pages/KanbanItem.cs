using System;

namespace Rezume.Pages
{
    internal class KanbanItem
    {
        public string Title { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }

        public int Category { get; set; }

        public  string[] Tags { get; set; }

        public  Uri ImageURL { get; set; }
  

                   
    }
}
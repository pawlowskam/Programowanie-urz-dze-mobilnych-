using System;
using System.Collections.Generic;
using System.Text;

namespace LaCucina.Model
{
    public class Recipe : ISqliteModel
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        public CategoryDataType Category { get; set; }

        public string Name { get; set; }
        public string Ingredient { get; set; }
        public string Recipe_Text_Area { get; set; }
        public uint Rate { get; set; }
        public string FilePath { get; set; }

    }
}

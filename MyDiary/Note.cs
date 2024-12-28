using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreateTime { get; }
        public Note(string title, string text, DateTime dateTime) {
            Title = title;
            Text = text;
            DateTime = dateTime;
            CreateTime = DateTime.Now;
        }
        public Note()
        {
        }
    }
}







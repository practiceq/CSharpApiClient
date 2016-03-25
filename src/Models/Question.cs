using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeQ.ApiClient.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string QuestionType { get; set; }
        public List<Row> Rows { get; set; }
        public List<string> ColumnNames { get; set; }

        public Question()
        {
            ColumnNames = new List<string>();
            Rows = new List<Row>();
        }
    }

    public class Row
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
    }

}

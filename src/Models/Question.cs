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
        public bool OfficeUse { get; set; }
        public List<Row> Rows { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<Attachment> Attachments { get; set; }

        public Question()
        {
            ColumnNames = new List<string>();
            Rows = new List<Row>();
            Attachments = new List<Attachment>();
        }
    }

    public class Row
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }

}

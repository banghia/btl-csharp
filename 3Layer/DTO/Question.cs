using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Layer.DTO
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public char Correct { get; set; }
        public int Level { get; set; }
        public int CatagoryId { get; set; }
    }
}

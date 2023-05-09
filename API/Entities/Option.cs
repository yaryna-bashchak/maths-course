using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImgUrl { get; set; } = "";
        public bool isAnswer { get; set; }
        public Test Test { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestHelper
{
    public class LessonParams
    {
        public int? MaxImportance { get; set; }
        public bool? OnlyUncompleted { get; set; }
        public string SearchTerm { get; set; }
    }
}
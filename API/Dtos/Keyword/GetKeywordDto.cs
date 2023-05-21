using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace API.Dtos.Keyword
{
    public class GetKeywordDto
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
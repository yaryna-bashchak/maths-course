using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Option;
using API.Entities;

namespace API.Dtos.Test
{
    public class AddTestDto
    {
        public int LessonId { get; set; }
        public string Question { get; set; }
        public string Type { get; set; } = "radio";
        public string ImgUrl { get; set; } = "";
        public List<AddOptionDto> Options { get; set; }
    }
}
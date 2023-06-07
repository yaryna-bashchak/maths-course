using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Option;
using API.Entities;

namespace API.Dtos.Test
{
    public class GetTestDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public string ImgUrl { get; set; } = "";
        public List<GetOptionDto> Options { get; set; }
    }
}
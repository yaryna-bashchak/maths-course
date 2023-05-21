using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Keyword;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeywordsController : ControllerBase
    {
        public CourseContext Context { get; }
        public IMapper Mapper { get; }
        public KeywordsController(CourseContext context, IMapper mapper)
        {
            Mapper = mapper;
            Context = context;
        }

        [HttpGet]
        public ActionResult<List<GetKeywordDto>> GetKeywords()
        {
            var dbKeywords = Context.Keywords;

            var keywords = dbKeywords
                .Select(l => Mapper.Map<GetKeywordDto>(l)).ToList();
            
            return Ok(keywords);
        }

        [HttpGet("{id}")]
        public ActionResult<GetKeywordDto> GetKeyword(int id)
        {
            try
            {
                var dbKeyword = Context.Keywords
                    .First(l => l.Id == id);

                var keyword = Mapper.Map<GetKeywordDto>(dbKeyword);
                return Ok(keyword);
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }
    }
}
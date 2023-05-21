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

        [HttpPost]
        public ActionResult<List<GetKeywordDto>> AddKeyword(AddKeywordDto newKeyword)
        {
            var keyword = Mapper.Map<Keyword>(newKeyword);
            keyword.Id = Context.Keywords.Max(l => l.Id) + 1;
            Context.Keywords.Add(keyword);
            Context.SaveChanges();
            
            var keywords = Context.Keywords.Select(l => Mapper.Map<GetKeywordDto>(l)).ToList();
            return Ok(keywords);
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

        [HttpPut("id")]
        public ActionResult<GetKeywordDto> UpdateKeyword(GetKeywordDto updatedKeyword)
        {
            try
            {
                var dbKeyword = Context.Keywords.FirstOrDefault(l => l.Id == updatedKeyword.Id);
                
                dbKeyword.Word = updatedKeyword.Word ?? dbKeyword.Word;
                Context.SaveChanges();
                
                var keyword = Mapper.Map<GetKeywordDto>(dbKeyword);
                return Ok(keyword);
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<List<GetKeywordDto>> DeleteKeyword(int id)
        {
            try
            {
                var dbkeyword = Context.Keywords.First(l => l.Id == id);
                
                Context.Keywords.Remove(dbkeyword);
                Context.SaveChanges();
            
                var keywords = Context.Keywords.Select(l => Mapper.Map<GetKeywordDto>(l)).ToList();
                return Ok(keywords);
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }
    }
}
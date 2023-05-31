using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Keyword;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<GetKeywordDto>>> AddKeyword(AddKeywordDto newKeyword)
        {
            var keyword = Mapper.Map<Keyword>(newKeyword);
            keyword.Id = Context.Keywords.Max(l => l.Id) + 1;
            await Context.Keywords.AddRangeAsync(keyword);
            await Context.SaveChangesAsync();
            
            var keywords = await Context.Keywords.Select(l => Mapper.Map<GetKeywordDto>(l)).ToListAsync();
            return keywords;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetKeywordDto>>> GetKeywords()
        {
            var dbKeywords = Context.Keywords;

            var keywords = await dbKeywords
                .Select(l => Mapper.Map<GetKeywordDto>(l)).ToListAsync();
            
            return keywords;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetKeywordDto>> GetKeyword(int id)
        {
            try
            {
                var dbKeyword = await Context.Keywords
                    .FirstAsync(l => l.Id == id);

                var keyword = Mapper.Map<GetKeywordDto>(dbKeyword);
                return keyword;
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult<GetKeywordDto>> UpdateKeyword(GetKeywordDto updatedKeyword)
        {
            try
            {
                var dbKeyword = await Context.Keywords.FirstOrDefaultAsync(l => l.Id == updatedKeyword.Id);
                
                dbKeyword.Word = updatedKeyword.Word ?? dbKeyword.Word;
                await Context.SaveChangesAsync();
                
                var keyword = Mapper.Map<GetKeywordDto>(dbKeyword);
                return keyword;
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetKeywordDto>>> DeleteKeyword(int id)
        {
            try
            {
                var dbkeyword = await Context.Keywords.FirstAsync(l => l.Id == id);
                
                Context.Keywords.Remove(dbkeyword);
                await Context.SaveChangesAsync();
            
                var keywords = await Context.Keywords.Select(l => Mapper.Map<GetKeywordDto>(l)).ToListAsync();
                return keywords;
            }
            catch (System.Exception)
            {
                return NotFound("Keyword with the provided ID not found.");
            }
        }
    }
}
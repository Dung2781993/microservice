using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        public SearchController(ISearchService searchService) 
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(Models.SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
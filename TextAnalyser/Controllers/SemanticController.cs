using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAnalyser.Logic;
using TextAnalyser.Models;

namespace TextAnalyser.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SemanticController : ControllerBase
    {
        private readonly AnalysisLogic _logic;

        public SemanticController(mph_uaContext context)
        {
            _logic = new AnalysisLogic(context);
        }

        [ActionName("Semantic")]
        [HttpPost]
        public async Task<IActionResult> SemanticAnalys([FromBody]RequestBody req)
        {
            SemanticModel semantic = await _logic.Semantic(req.TextForAnalysis);
            return Ok(semantic);
        }

        [ActionName("Zipf")]
        [HttpPost]
        public ActionResult ZipfAnalys([FromBody]RequestBody req)
        {
            var semantic = _logic.Zipf(req.TextForAnalysis);
            return Ok(semantic);
        }
    }
}
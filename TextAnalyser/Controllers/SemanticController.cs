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
        [HttpGet]
        public ActionResult SemanticAnalys(string textForAnalysis)
        {
            SemanticModel semantic = _logic.Semantic(textForAnalysis).Result;
            return Ok(semantic);
        }

        [ActionName("Zipf")]
        [HttpGet]
        public ActionResult ZipfAnalys(string textForAnalysis)
        {
            var semantic = _logic.Zipf(textForAnalysis);
            return Ok(semantic);
        }
    }
}
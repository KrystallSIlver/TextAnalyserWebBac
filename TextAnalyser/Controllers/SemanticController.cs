﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            var zipf = _logic.Zipf(req.TextForAnalysis);
            return Ok(zipf);
        }

        [ActionName("Orthography")]
        [HttpPost]
        public IActionResult OrthographyAnalysis([FromBody]LanguageToolRequestBody reqData)
        {
            //Створення об'єкту запиту
            var request = WebRequest.Create("https://languagetool.org/api/v2/check");
            //Налаштування запиту
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string data = $"text={reqData.Text}&language={reqData.Language}&enabledRules=MORFOLOGIK_RULE_UK_UA&enabledCategories=TYPOS&enabledOnly=true";
            //Перетворення текстових данних в масив байтів
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;
            //Передача даних в запит
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            string res = string.Empty;
            try
            {
                //Отримання відповіді на запит
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        res = reader.ReadToEnd();
                    }
                }
                return Ok(res);

            }
            catch
            {
                return Ok(res);
            }


        }

        [ActionName("Map")]
        [HttpPost]
        public async Task<ActionResult> TextMap([FromBody]RequestBody req)
        {
            var map = await _logic.Map(req.TextForAnalysis);
            return Ok(map);
        }
    }
}
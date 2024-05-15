using Azure;
using DotNetTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IHomeRepo _homeRepo;

        public HomeController(ILogger<HomeController> logger, IHomeRepo homeRepo)
        {
            _logger = logger;
            _homeRepo = homeRepo;
        }
        [HttpPost]
        [Route("createApplication")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateApplication(CreateApplication createApplication)
        {
            var response = await _homeRepo.CreateApplication(createApplication);
            return Ok(response);
        }
        [HttpPost]
        [Route("createQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateQuestion(QuestionsList questionsList)
        {
            var response = await _homeRepo.CreateQuestion(questionsList);
            return Ok(response);
        }
        [HttpPost]
        [Route("submitApplicationForm")]
        [AllowAnonymous]
        public async Task<ActionResult> SubmitApplicationForm(ApplicationForm questionsList)
        {
            var response = await _homeRepo.SubmitApplicationForm(questionsList);
            return Ok(response);
        }
        [HttpGet]
        [Route("getQuestions")]
        [AllowAnonymous]
        public async Task<ActionResult> GetQuestions()
        {
            var response = await _homeRepo.GetQuestions();
            return Ok(response);
        }
        [HttpGet]
        [Route("getForm")]
        [AllowAnonymous]
        public async Task<ActionResult> GetForm()
        {
            var response = await _homeRepo.GetForm();
            return Ok(response);
        }
        [HttpPut]
        [Route("updateQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateQuestion(QuestionsList createApplication)
        {
            var response = await _homeRepo.UpdateQuestion(createApplication);
            return Ok(response);
        }
        [HttpDelete]
        [Route("deleteQuestion")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteQuestion(string id,string type)
        {
            var response = await _homeRepo.DeleteQuestion(id,type);
            return Ok(response);
        }
        
    }
}

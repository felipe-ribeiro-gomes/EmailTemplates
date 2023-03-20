using EmailTemplates.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace EmailTemplates.Controllers
{
    public class SharedController : Controller
    {
        private readonly ILogger<SharedController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SharedController(ILogger<SharedController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
		[Route("[controller]/[action]/{id}")]
        public IActionResult ObtemTemplate(string id)
        {
            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/db/db.json");
            var json = System.IO.File.ReadAllText(fullPath);
            var db = JsonSerializer.Deserialize<Db>(json);
            var template = db.Templates.FirstOrDefault(q => q.Id == id);
            return Ok(template);
        }

        public IActionResult Teste()
        {
            try
            {
				var host = "smtp.office365.com";
				var port = 587;
				var enableSsl = true;
				var login = "felipegomes@educacao.rj.gov.br";
				var password = "Zinedine1972";

				var smtp = new SmtpClient();
				smtp.Host = host;
				smtp.Port = port;
				smtp.EnableSsl = enableSsl;
				smtp.Credentials = new NetworkCredential(login, password);

				var from = new MailAddress("felipegomes@educacao.rj.gov.br", "Felipe Ribeiro Gomes");
				var toList = new List<MailAddress>
			    {
				    new MailAddress("im.felipe@hotmail.com"),
			    };
				var ccList = new List<MailAddress>
			    {
				    new MailAddress("im.felipe@hotmail.com"),
			    };
				var bccList = new List<MailAddress>
			    {
				    new MailAddress("im.felipe@hotmail.com"),
			    };
				var subject = "[Sistema] TESTE";
				var body = "teste teste teste";

				var msg = new MailMessage();
				msg.From = from;
				toList.ForEach(d => msg.To.Add(d));
				ccList.ForEach(d => msg.To.Add(d));
				bccList.ForEach(d => msg.To.Add(d));
				msg.Subject = subject;
				msg.Body = body;

				smtp.Send(msg);

                return Ok();
			}
            catch (Exception ex)
            {
                throw;
            }
		}
    }
}

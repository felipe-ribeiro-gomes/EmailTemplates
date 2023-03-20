using EmailTemplates.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace EmailTemplates.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/db/db.json");
            var json = System.IO.File.ReadAllText(fullPath);
            var db = JsonSerializer.Deserialize<Db>(json);

            var viewModel = new HomeViewModel();

            viewModel.Templates = db.Templates.Select(s => new SelectListItem(s.Id, s.Id)).ToList();
            viewModel.Remetente = db.Remetente.Email;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel viewModel)
        {
            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, $"wwwroot/templates/{viewModel.TemplateId}.txt");
            var template = System.IO.File.ReadAllText(fullPath);

            var smtp = new SmtpClient();
            smtp.Host = _configuration.GetValue<string>("Mail:Host");
            smtp.Port = _configuration.GetValue<int>("Mail:Port");
            smtp.Credentials = new NetworkCredential(viewModel.Remetente, "Zinedine1972");
            smtp.EnableSsl = true;

            var message = new MailMessage();
            message.From = new MailAddress(viewModel.Remetente);
            foreach (var destinatario in viewModel.DestinatarioIds)
                message.To.Add(new MailAddress(destinatario));
            message.Subject = viewModel.Assunto;
            message.Body = template;
            message.IsBodyHtml = false;

            smtp.Send(message);

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

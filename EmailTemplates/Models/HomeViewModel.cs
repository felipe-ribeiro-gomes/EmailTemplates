using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmailTemplates.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Templates = new List<SelectListItem>();
            Destinatarios = new List<SelectListItem>();
        }

        public ICollection<SelectListItem> Templates { get; set; }
        public ICollection<SelectListItem> Destinatarios { get; set; }

        public string TemplateId { get; set; }
        public string Remetente { get; set; }
        public ICollection<string> DestinatarioIds { get; set; }
        public string Assunto { get; set; }
        public ICollection<string> Variaveis { get; set; }
        public string Conteudo { get; set; }
     }
}

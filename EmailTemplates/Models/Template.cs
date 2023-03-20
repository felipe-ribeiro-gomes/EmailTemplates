using System.Collections.Generic;

namespace EmailTemplates.Models
{
    public class Template
    {
        public string Id { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public ICollection<Destinatario> Destinatarios { get; set; }
        public ICollection<string> Variaveis { get; set; }
    }
}

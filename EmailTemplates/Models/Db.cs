using System.Collections.Generic;

namespace EmailTemplates.Models
{
    public class Db
    {
        public Remetente Remetente { get; set; }

        public ICollection<Template> Templates { get; set; }
    }
}

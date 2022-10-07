using System;

namespace Odevez.Repository.Models
{
    public class Usuario
    {
        public long CODIGO { get; set; }
        public long ID { get; set; }
        public DateTime DATULTALT { get; set; }
        public string NOME { get; set; }
        public string SOBRENOME { get; set; }
        public string APELIDO { get; set; }
        public string EMAIL { get; set; }
        public string SENHAHASH { get; set; }
        public string CELULAR { get; set; }
        public string CPF { get; set; }
    }
}

using System;

namespace Odevez.DTO
{
    public class UsuarioDTO
    {
        public long Codigo { get; set; }
        public long Id { get; set; }
        public DateTime DatUltAlt { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Imagem { get; set; }
        public string SenhaHash { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
    }
}

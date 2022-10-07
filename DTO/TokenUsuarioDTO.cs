namespace Odevez.DTO
{
    public class TokenUsuarioDTO
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public int ExpireIn { get; set; }
        public string Apelido { get; set; }
        public long Codigo { get; set; }
    }
}

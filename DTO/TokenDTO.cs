namespace Odevez.DTO
{
    public sealed class TokenDTO
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public int ExpireIn { get; set; }
    }
}

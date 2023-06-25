namespace APITracker.Entities
{
    public class EnderecoApi
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public int TimeOutEmMinutos { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public string Ambiente { get; set; }
    }
}

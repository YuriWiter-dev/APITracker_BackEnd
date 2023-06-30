using APITracker.Enums;

namespace APITracker.Data.DTO;

public class StatusDTO
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public string Endereco { get; set; }
    public int StatusCode { get; set; }
    public string Error { get; set; }
    public int TimeOutEmMinutos { get; set; }
    public Method Method { get; set; }
    public string Body { get; set; }
    public Ambiente Ambiente { get; set; }
    public Sistema Sistema { get; set; }
}

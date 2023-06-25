namespace APITracker.Data.DTO;

public class RequisicaoDTO
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public string Endereco { get; set; }
    public int TimeOutEmMinutos { get; set; }
    public string Method { get; set; }
    public string Body { get; set; } = string.Empty;
}

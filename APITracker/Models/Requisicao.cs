using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace APITracker.Models;

public class Requisicao
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Endereco é obrigatório.")]
    public string Endereco { get; set; }


}

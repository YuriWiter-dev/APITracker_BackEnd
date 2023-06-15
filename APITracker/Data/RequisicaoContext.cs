using APITracker.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
namespace APITracker.Data;


public class RequisicaoContext :DbContext
{
    public RequisicaoContext(DbContextOptions<RequisicaoContext> opts) : base(opts){ }

    public DbSet<Requisicao> requisicaos { get; set; }
}

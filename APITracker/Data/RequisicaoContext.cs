﻿using APITracker.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APITracker.Data;

public class BaseContext :DbContext
{
    protected IDbContextTransaction _contextoTransaction { get; set; }

    public BaseContext(DbContextOptions<BaseContext> opts) : base(opts){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EnderecoApiMap());
    }
    public async Task RollBack()
    {
        if (_contextoTransaction != null)
        {
            await _contextoTransaction.RollbackAsync();
        }
    }

    private async Task Salvar()
    {
        try
        {
            ChangeTracker.DetectChanges();
            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await RollBack();
            throw new Exception(ex.Message);
        }
    }

    public async Task Commit()
    {
        if (_contextoTransaction != null)
        {
            await _contextoTransaction.CommitAsync();
            await _contextoTransaction.DisposeAsync();
            _contextoTransaction = null;
        }
    }

    public async Task SalvarMudancas(bool commit = true)
    {
        await Salvar();
        if (commit)
            await Commit();
    }
    public async Task<IDbContextTransaction> IniciarTransaction()
    {
        if (_contextoTransaction == null)
        {
            _contextoTransaction = await this.Database.BeginTransactionAsync();
        }
        return _contextoTransaction;
    }
}

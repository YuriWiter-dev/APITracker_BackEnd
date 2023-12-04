using APITracker.Data;
using APITracker.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APITracker.Repositories
{
    public interface IEnderecoApiRepository : IBaseRepositorio
    {
        Task<object> Incluir(EnderecoApi entidade); 
        Task<EnderecoApi> BuscarPorId(int id);
        Task<EnderecoApi> BuscarComPesquisa(Expression<Func<EnderecoApi, bool>> expression); 
        Task<IEnumerable<EnderecoApi>> BuscarTodos();
        Task<IEnumerable<EnderecoApi>> BuscarTodosComPesquisa(Expression<Func<EnderecoApi, bool>> expression);
        Task Alterar(EnderecoApi entidade);
    }

    public class EnderecoApiRepository : BaseRepositorio, IEnderecoApiRepository
    {
        private readonly DbSet<EnderecoApi> _dbSet;

        public EnderecoApiRepository(BaseContext context) : base(context)
        {
            _dbSet = context.Set<EnderecoApi>();
        }

        public async Task<object> Incluir(EnderecoApi entidade)
        {
            await IniciarTransaction();
            var obj = await _dbSet.AddAsync(entidade);
            await SalvarMudancas();
            return obj.Entity.Id;
        }

        public virtual async Task<EnderecoApi> BuscarPorId(int id)
        {
            return await Buscar(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<EnderecoApi> BuscarComPesquisa(Expression<Func<EnderecoApi, bool>> expression)
        {
            return await Buscar(expression).FirstOrDefaultAsync();
        }

        public IQueryable<EnderecoApi> Buscar(Expression<Func<EnderecoApi, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<EnderecoApi>> BuscarTodos()
        {
            return await Buscar(x => true).ToListAsync();
        }

        public async Task<IEnumerable<EnderecoApi>> BuscarTodosComPesquisa(Expression<Func<EnderecoApi, bool>> expression)
        {
            return await Buscar(expression).ToListAsync();
        }

        public async Task Alterar(EnderecoApi entidade)
        {
            await IniciarTransaction();
            _context.Entry(entidade).State = EntityState.Detached;
            await SalvarMudancas();
        }
    }
}

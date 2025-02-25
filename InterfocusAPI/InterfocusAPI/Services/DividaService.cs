using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using NHibernate;
using NHibernate.Linq;

namespace InterfocusAPI.Services
{
    public class DividaService
    {
        private readonly ISessionFactory sessionFactory;
        public DividaService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public async Task<Divida> CadastrarDivida(int id, Divida divida)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var cliente = await session.GetAsync<Cliente>(id);
            var somadividas = cliente.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor);
            if (cliente == null || divida.Valor + somadividas > 200)
            {
                throw new Exception();
            }
            divida.Cliente = cliente;
            divida.ClienteId = cliente.ClienteId;
            cliente.Dividas.Add(divida);
            await session.SaveAsync(divida);
            await session.UpdateAsync(cliente);
            await transaction.CommitAsync();
            return divida;
        }
        public async Task<Divida> MarcarDividaPaga(int id, DividaDTO dividadto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var divida = await session.GetAsync<Divida>(id);
            if (divida == null)
            {
                throw new Exception("Divida não encontrada");
            }
            divida.Situacao = dividadto.Situacao;
            await session.UpdateAsync(divida);
            await transaction.CommitAsync();
            return divida;
        }
        public async Task<int> SomaDividasCliente(int id)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = await session.GetAsync<Cliente>(id);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            var somadividas = cliente.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor);
            return somadividas;
        }
        public async Task<List<Divida>> VerDividas(int id)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = await session.GetAsync<Cliente>(id);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            return cliente.Dividas.ToList();
        }
        public async Task<Divida> RemoverDivida(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var divida = await session.GetAsync<Divida>(id);
            if (divida == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            await session.DeleteAsync(divida);
            await transaction.CommitAsync();
            return divida;
        }
    }
}

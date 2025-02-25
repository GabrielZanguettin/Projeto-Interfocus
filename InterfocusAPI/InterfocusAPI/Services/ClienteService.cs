using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using NHibernate;
using NHibernate.Linq;

namespace InterfocusAPI.Services
{
    public class ClienteService
    {
        private readonly ISessionFactory sessionFactory;
        public ClienteService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public async Task<Cliente> CadastrarCliente(Cliente cliente) // Post
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            await session.SaveAsync(cliente);
            await transaction.CommitAsync();
            return cliente;
        }
        public async Task<List<Cliente>> ProcurarPorNome(string nome) // Get
        {
            using var session = sessionFactory.OpenSession();
            var clientes = await session.Query<Cliente>().Where(c => c.Nome == nome).ToListAsync();
            return clientes;
        }
        public async Task<List<Cliente>> MostrarClientes() // Get
        {
            using var session = sessionFactory.OpenSession();
            var clientes = await session.Query<Cliente>().OrderByDescending(c => c.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor)).ToListAsync();
            return clientes;
        }
        public async Task<Cliente> AtualizarCliente(int id, ClienteDTO clientedto) // Put
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var Cliente = await session.GetAsync<Cliente>(id);
            if (Cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            Cliente.Nome = clientedto.Nome;
            Cliente.CPF = clientedto.CPF;
            Cliente.DataNasc = clientedto.DataNasc;
            Cliente.Email = clientedto.Email;
            await session.UpdateAsync(Cliente);
            await transaction.CommitAsync();
            return Cliente;
        }
        public async Task<Cliente> RemoverCliente(int id) // Delete
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var cliente = await session.QueryOver<Cliente>()
                           .Where(c => c.ClienteId == id)
                           .Fetch(d => d.Dividas).Eager
                           .SingleOrDefaultAsync();
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            await session.DeleteAsync(cliente);
            await transaction.CommitAsync();
            return cliente;
        }
        public async Task<bool> ValidarCliente(string cpf, string email)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = await session.Query<Cliente>().FirstOrDefaultAsync(c => c.CPF == cpf || c.Email == email);
            if (cliente == null)
            {
                return true;
            }
            return false;
        }
    }
}

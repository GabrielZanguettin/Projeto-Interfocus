using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using NHibernate;

namespace InterfocusAPI.Services
{
    public class ClienteService
    {
        private readonly ISessionFactory sessionFactory;
        public ClienteService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public bool CadastrarCliente(Cliente cliente) // Post
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Save(cliente);
            transaction.Commit();
            return true;
        }
        public List<Cliente> ProcurarPorNome(string nome) // Get
        {
            using var session = sessionFactory.OpenSession();
            var clientes = session.Query<Cliente>().Where(c => c.Nome == nome).ToList();
            return clientes;
        }
        public List<Cliente> MostrarClientes() // Get
        {
            using var session = sessionFactory.OpenSession();
            var clientes = session.Query<Cliente>().OrderByDescending(c => c.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor)).ToList();
            return clientes;
        }
        public Cliente AtualizarCliente(int id, ClienteDTO clientedto) // Put
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var Cliente = session.Get<Cliente>(id);
            if (Cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            Cliente.Nome = clientedto.Nome;
            Cliente.CPF = clientedto.CPF;
            Cliente.DataNasc = clientedto.DataNasc;
            Cliente.Email = clientedto.Email;
            session.Update(Cliente);
            transaction.Commit();
            return Cliente;
        }
        public Cliente RemoverCliente(int id) // Delete
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var cliente = session.QueryOver<Cliente>()
                           .Where(c => c.ClienteId == id)
                           .Fetch(d => d.Dividas).Eager
                           .SingleOrDefault();
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            session.Delete(cliente);
            transaction.Commit();
            return cliente;
        }
        public bool ValidarCliente(string cpf, string email)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = session.Query<Cliente>().FirstOrDefault(c => c.CPF == cpf || c.Email == email);
            if (cliente == null)
            {
                return true;
            }
            return false;
        }
    }
}

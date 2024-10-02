using InterfocusAPI.Dtos;
using InterfocusAPI.Entidades;
using NHibernate;

namespace InterfocusAPI.Services
{
    public class DividaService
    {
        private readonly ISessionFactory sessionFactory;
        public DividaService(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public Divida CadastrarDivida(int id, Divida divida)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var cliente = session.Get<Cliente>(id);
            var somadividas = cliente.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor);
            if (cliente == null || divida.Valor + somadividas > 200)
            {
                throw new Exception();
            }
            divida.Cliente = cliente;
            divida.ClienteId = cliente.ClienteId;
            cliente.Dividas.Add(divida);
            session.Save(divida);
            session.Update(cliente);
            transaction.Commit();
            return divida;
        }
        public Divida MarcarDividaPaga(int id, DividaDTO dividadto)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var divida = session.Get<Divida>(id);
            if (divida == null)
            {
                throw new Exception("Divida não encontrada");
            }
            divida.Situacao = dividadto.Situacao;
            session.Update(divida);
            transaction.Commit();
            return divida;
        }
        public int SomaDividasCliente(int id)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = session.Get<Cliente>(id);
            if (cliente == null)
            {
                return 0;
            }
            var somadividas = cliente.Dividas.Where(d => d.Situacao == false).Sum(d => d.Valor);
            return somadividas;
        }
        public List<Divida> VerDividas(int id)
        {
            using var session = sessionFactory.OpenSession();
            var cliente = session.Get<Cliente>(id);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            return cliente.Dividas.ToList();
        }
        public Divida RemoverDivida(int id)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            var divida = session.Get<Divida>(id);
            if (divida == null)
            {
                return null;
            }
            session.Delete(divida);
            transaction.Commit();
            return divida;
        }
    }
}

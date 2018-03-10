using System.Collections.Generic;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Domain;

namespace EpicSoftware.Fulfilment.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
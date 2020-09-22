using System.Threading.Tasks;

namespace RGRL
{
    public interface IRepository<T>
    {
        Task<int> Create(T obj);
    }
}

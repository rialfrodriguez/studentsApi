using System.Threading.Tasks;

namespace studentsApi.Core
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}
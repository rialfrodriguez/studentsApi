using System.Threading.Tasks;
using studentsApi.Core;

namespace studentsApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext context;
        public UnitOfWork(MyDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
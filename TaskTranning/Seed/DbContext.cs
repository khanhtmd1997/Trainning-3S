using Microsoft.EntityFrameworkCore;
using TaskTranning.Models;

namespace TaskTranning.Seed
{
    public static class DbContext
    {
        public static void Initialize(CodeFirstDataContext context)
        {
            context.Database.Migrate();
        }
        
    }
}
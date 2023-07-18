using Microsoft.EntityFrameworkCore;

namespace Bristows.TRACR.DAL.Infrastructure.Interfaces
{
    public interface IDbFactory<TR> where TR : DbContext
    {
        TR Init();
    }
}
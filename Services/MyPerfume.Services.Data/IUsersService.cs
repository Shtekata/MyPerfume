namespace MyPerfume.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> All<T>();
    }
}

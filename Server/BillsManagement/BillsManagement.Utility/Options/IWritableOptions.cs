namespace BillsManagement.Utility.Options
{
    using Microsoft.Extensions.Options;
    using System;

    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}

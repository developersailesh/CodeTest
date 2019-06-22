namespace AppServer.Interfaces
{
    using AppServer.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for People Client Service inorder to consume People APi
    /// </summary>
    public interface IPeopleClientService
    {
        /// <summary>
        /// Interface method GetPeopleData
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<People>> GetPeopleData(CancellationToken cancellationToken);
    }
}

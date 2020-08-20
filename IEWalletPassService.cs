namespace Demo.PASS.Faring.Abstractions.Services
{
    using System.Threading.Tasks;
    using Demo.Core.DataAccess.Abstraction.Paging;
    using Demo.Core.DataAccess.Utilities.Criteria;
    using Demo.PASS.Faring.DataTransferModels.EWalletPass;

    /// <summary>
    /// A service to perform operations on the eWalletPass objects.
    /// </summary>
    public interface IEWalletPassService
    {
        /// <summary>
        /// Gets all of the information for eWalletPass.
        /// </summary>
        /// <param name="pagingInfo">An <see cref="IPagingStrategy"/> with the pagination information.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<IPaginatedList<EZWalletPassReadDto>> GetEWalletPassesAsync(IPagingStrategy pagingInfo);

        /// <summary>
        /// Gets all of the information for a specific ezWalletPass.
        /// </summary>
        /// <param name="ezWalletPassId">The eWalletPass entity to get.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<EZWalletPassReadDto> GetEWalletPassByIdAsync(int ezWalletPassId);

        /// <summary>
        /// Add eWalletPass.
        /// </summary>
        /// <param name="ezWalletPassInsertDto">The eWalletPass insert dto.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> AddEWalletPassAsync(EZWalletPassInsertDto ezWalletPassInsertDto);

        /// <summary>
        /// update the information for a specific eWalletPass.
        /// </summary>
        /// <param name="ezWalletPassId">The id of the eWalletPass to update.</param>
        /// <param name="ezWalletPassUpdateDto">The eWalletPass update dto.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> UpdateEWalletPassAsync(int ezWalletPassId, EZWalletPassUpdateDto ezWalletPassUpdateDto);

        /// <summary>
        /// delete the information for a specific eWalletPass.
        /// </summary>
        /// <param name="ezWalletPassId">The id of the eWalletPass to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> DeleteEWalletPassAsync(int ezWalletPassId);
    }
}

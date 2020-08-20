namespace Demo.PASS.Faring.Services
{
    using AutoMapper;
    using System.Threading.Tasks;
    using Demo.Core.DataAccess.Abstraction.Extensions;
    using Demo.Core.DataAccess.Abstraction.Paging;
    using Demo.Core.DataAccess.Utilities.Criteria;
    using Demo.PASS.Faring.Abstractions.Repositories;
    using Demo.PASS.Faring.Abstractions.Services;
    using Demo.PASS.Faring.DataTransferModels.EWalletPass;
    using Demo.PASS.Faring.Model.Domain;

    /// <summary>
    /// An instance of the EWalletPass service used to acess/modify the ezWalletPass data.
    /// </summary>
    public sealed class EWalletPassService : IEWalletPassService
    {
        /// <summary>
        /// Defines the _walletPassRepo.
        /// </summary>
        private readonly IEWalletPassRepository _walletPassRepo;

        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EWalletPassService"/> class.
        /// </summary>
        /// <param name="eWalletPassRepo">Repository instance.</param>
        /// <param name="mapper">A <see cref="IMapper"/> used to translate EZWalletPass models.</param>
        public EWalletPassService(IEWalletPassRepository eWalletPassRepo, IMapper mapper)
        {
            _walletPassRepo = eWalletPassRepo;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IPaginatedList<EZWalletPassReadDto>> GetEWalletPassesAsync(IPagingStrategy pagingInfo)
        {
            var criteria = new FilterCriteria<EWalletPass>();
            criteria.Paging = pagingInfo;
            var result = await _walletPassRepo.FetchByCriteriaAsync(criteria, 100).ConfigureAwait(false);

            return result.Map<EWalletPass, EZWalletPassReadDto>(_mapper);
        }

        /// <inheritdoc/>
        public async Task<int> AddEWalletPassAsync(EZWalletPassInsertDto eWalletPassInsertDto)
        {
            var eWalletPassEntity = _mapper.Map<EWalletPass>(eWalletPassInsertDto);
            return await _walletPassRepo.AddAsync(eWalletPassEntity).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<EZWalletPassReadDto> GetEWalletPassByIdAsync(int ezWalletPassId)
        {
            return _mapper.Map<EZWalletPassReadDto>(await _walletPassRepo.GetByAsync(x => x.EWalletPassId == ezWalletPassId).ConfigureAwait(false));
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEWalletPassAsync(int ezWalletPassId, EZWalletPassUpdateDto eWalletPassUpdateDto)
        {
            var eWalletPassEntity = _mapper.Map<EWalletPass>(eWalletPassUpdateDto);
            eWalletPassEntity.EWalletPassId = ezWalletPassId;

            return await _walletPassRepo.UpdateAsync(eWalletPassEntity).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteEWalletPassAsync(int ezWalletPassId)
        {
            return await _walletPassRepo.DeleteAsync(new EWalletPass() { EWalletPassId = ezWalletPassId }).ConfigureAwait(false);
        }
    }
}

namespace Demo.PASS.Faring.DataTransferModels.EWalletPass
{
    /// <summary>
    /// The read data transfer object for the EZWalletPass.
    /// </summary>
    public class EZWalletPassReadDto : EZWalletPassInsertDto
    {
        /// <summary>
        /// Gets or sets the Identifier of an existing EZ-Wallet period pass..
        /// </summary>
        public int EZWalletPassId { get; set; }
    }
}

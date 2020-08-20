namespace Demo.PASS.Faring.DataTransferModels.EWalletPass
{
    /// <summary>
    /// The update data transfer object for the EZWalletPass.
    /// </summary>
    public class EZWalletPassUpdateDto
    {
        /// <summary>
        /// Gets or sets the Name of the pass..
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Start date of the period associated with the pass..
        /// </summary>
        public string PeriodStart { get; set; }

        /// <summary>
        /// Gets or sets the End date of the period associated with the pass..
        /// </summary>
        public string PeriodEnd { get; set; }

        /// <summary>
        /// Gets or sets the First date that the pass can be purchased..
        /// </summary>
        public int SaleStart { get; set; }

        /// <summary>
        /// Gets or sets the Last date that the pass can be purchased..
        /// </summary>
        public int SaleEnd { get; set; }

        /// <summary>
        /// Gets or sets the Cost of the pass..
        /// </summary>
        public int SaleAmount { get; set; }

        /// <summary>
        /// Gets or sets the EZWalletPassTypeId, pass type associated with the pass (e.g., Senior, Adult, Student)..
        /// </summary>
        public int EZWalletPassTypeId { get; set; }

        /// <summary>
        /// Gets or sets the FromAge, Age the purchaser must be older than to buy the pass..
        /// </summary>
        public int FromAge { get; set; }

        /// <summary>
        /// Gets or sets the ToAge, Age the purchaser must be younger than to buy the pass..
        /// </summary>
        public int ToAge { get; set; }

        /// <summary>
        /// Gets or sets the StudentEligibilityFromDate, Start date of student eligibility period (used for purchasing student passes)..
        /// </summary>
        public string StudentEligibilityFromDate { get; set; }

        /// <summary>
        /// Gets or sets the StudentEligibilityToDate, End date of student eligibility period (used for purchasing student passes)..
        /// </summary>
        public string StudentEligibilityToDate { get; set; }

        /// <summary>
        /// Gets or sets the Medicaid From Date, Start date from which the client's Medicaid account is valid..
        /// </summary>
        public string MedicaidFromDate { get; set; }

        /// <summary>
        /// Gets or sets the MedicaidToDate, End date after which the client's Medicaid account is no longer valid..
        /// </summary>
        public string MedicaidToDate { get; set; }

        /// <summary>
        /// Gets or sets the TimePeriod, Length of time the pass is valid..
        /// </summary>
        public int TimePeriod { get; set; }

        /// <summary>
        /// Gets or sets the Not for sale flag..
        /// </summary>
        public string NotForSale { get; set; }
    }
}

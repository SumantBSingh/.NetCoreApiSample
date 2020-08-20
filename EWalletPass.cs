namespace Demo.PASS.Faring.Model.Domain
{
    using Demo.Core.DataAccess.Models;

    /// <summary>
    /// EWalletPass entity.
    /// </summary>
    public class EWalletPass : EntityBase
    {
        /// <summary>
        /// Gets or sets the EWalletPassId.
        /// </summary>
        public int EWalletPassId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the PeriodStart.
        /// </summary>
        public string PeriodStart { get; set; }

        /// <summary>
        /// Gets or sets the PeriodEnd.
        /// </summary>
        public string PeriodEnd { get; set; }

        /// <summary>
        /// Gets or sets the SaleStart.
        /// </summary>
        public int SaleStart { get; set; }

        /// <summary>
        /// Gets or sets the SaleEnd.
        /// </summary>
        public int SaleEnd { get; set; }

        /// <summary>
        /// Gets or sets the SaleAmount.
        /// </summary>
        public int SaleAmount { get; set; }

        /// <summary>
        /// Gets or sets the EWalletPassTypeId ..
        /// </summary>
        public int EWalletPassTypeId { get; set; }

        /// <summary>
        /// Gets or sets the FromAge.
        /// </summary>
        public int FromAge { get; set; }

        /// <summary>
        /// Gets or sets the ToAge.
        /// </summary>
        public int ToAge { get; set; }

        /// <summary>
        /// Gets or sets the StudentEligFromDate.
        /// </summary>
        public string StudentEligFromDate { get; set; }

        /// <summary>
        /// Gets or sets the StudentEligToDate.
        /// </summary>
        public string StudentEligToDate { get; set; }

        /// <summary>
        /// Gets or sets the MedicaidFrDate.
        /// </summary>
        public string MedicaidFrDate { get; set; }

        /// <summary>
        /// Gets or sets the MedicaidToDate.
        /// </summary>
        public string MedicaidToDate { get; set; }

        /// <summary>
        /// Gets or sets the TimePeriod.
        /// </summary>
        public int TimePeriod { get; set; }

        /// <summary>
        /// Gets or sets the NotForSale.
        /// </summary>
        public string NotForSale { get; set; }
    }
}

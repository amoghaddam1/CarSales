/*
 * Name: Ali Moghaddam
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2019-09-12
 * Updated: 2019-10-22
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Moghaddam.Ali.Business
{
    /// <summary>
    /// This class contains functionality to make invoices for the car wash department.
    /// The ServiceInvoice class derives from the Invoice class.
    /// </summary>
    public class CarWashInvoice : Invoice
    {
        private decimal packageCost;
        private decimal fragranceCost;

        /// <summary>
        /// This constructor takes in the provincial sales tax and the
        /// goods and services tax.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <param name="provincialTaxRate">This is the variable for the provincial sales tax.</param>
        /// <param name="goodsAndServicesTaxRate">This is the variable for the goods and services tax.</param>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate) : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {
            this.packageCost = 0;
            this.fragranceCost = 0;
        }

        /// <summary>
        /// This constructor takes in the provincial sales tax and the goods and services tax 
        /// as well as the package cost and the fragrance cost.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <param name="provincialTaxRate">This is the variable for the provincial sales tax.</param>
        /// <param name="goodsAndServicesTaxRate">This is the variable for the goods and services tax.</param>
        /// <param name="packageCost">This is the variable for the package cost.</param>
        /// <param name="fragranceCost">this is the variable for the fragrance cost.</param>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate, decimal packageCost, decimal fragranceCost) : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {
            if (packageCost < 0)
            {
                throw new ArgumentOutOfRangeException("provincialSalesTaxRate", "The argument cannot be less than 0.");
            }
            if (fragranceCost < 0)
            {
                throw new ArgumentOutOfRangeException("provincialSalesTaxRate", "The argument cannot be less than 0.");
            }
            this.packageCost = packageCost;
            this.fragranceCost = fragranceCost;
        }

        /// <summary>
        /// This is an event for when PackageCost is changed.
        /// </summary>
        public event EventHandler PackageCostChanged;

        /// <summary>
        /// This is an event for when PackageCost is changed.
        /// </summary>
        public event EventHandler FragranceCostChanged;

        /// <summary>
        /// This property sets and returns the package cost.
        /// </summary>
        public decimal PackageCost
        {
            get
            {
                return this.packageCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                if (value != this.packageCost)
                {
                    OnPackageCostChanged();
                    this.packageCost = value;
                }
            }
        }

        /// <summary>
        /// This property sets and returns the fragrance cost.
        /// </summary>
        public decimal FragranceCost
        {
            get
            {
                return this.fragranceCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                if (value != this.fragranceCost)
                {
                    OnFragranceCostChanged();
                    this.fragranceCost = value;
                }
            }
        }

        /// <summary>
        /// This property returns the provincial sales tax charged by multiplying the 
        /// provincial tax rate by the subtotal.
        /// </summary>
        public override decimal ProvincialSalesTaxCharged
        {
            get
            {
                return this.ProvincialSalesTaxRate * this.SubTotal;
            }
        }

        /// <summary>
        /// This property returns the provincial sales tax charged by multiplying the 
        /// provincial tax rate by the subtotal.
        /// </summary>
        public override decimal GoodsAndServicesTaxCharged
        {
            get
            {
                return this.GoodsAndServicesTaxRate * this.SubTotal;
            }
        }

        /// <summary>
        /// This property calculates the subtotal by adding the package and fragrance costs together.
        /// </summary>
        public override decimal SubTotal
        {
            get
            {
                return this.FragranceCost + this.PackageCost;
            }
        }

        /// <summary>
        /// This property returns the total by adding the taxes to the subtotal.
        /// </summary>
        public decimal Total
        {
            get
            {
                return this.SubTotal + this.ProvincialSalesTaxCharged + this.GoodsAndServicesTaxCharged;
            }
        }

        /// <summary>
        /// On method for the PackageCostChanged event.
        /// </summary>
        protected virtual void OnPackageCostChanged()
        {
            if (PackageCostChanged != null)
            {
                PackageCostChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// On method for the FragranceCostChanged event.
        /// </summary>
        protected virtual void OnFragranceCostChanged()
        {
            if (FragranceCostChanged != null)
            {
                FragranceCostChanged(this, new EventArgs());
            }
        }
    }
}

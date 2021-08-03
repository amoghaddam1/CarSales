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
    /// This is an abstract class that sets up Invoice 
    /// functionality to be used in other classes.
    /// </summary>
    public abstract class Invoice
    {
        private decimal provincialSalesTaxRate;
        private decimal goodsAndServicesTaxRate;

        /// <summary>
        /// This constructor takes in the provincial sales tax and the
        /// goods and services tax.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <param name="provincialSalesTaxRate"> This is the variable for the provincial sales tax.</param>
        /// <param name="goodsAndServicesTaxRate">This is the variable for the goods and services tax.</param>
        public Invoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate)
        {
            if (provincialSalesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("provincialSalesTaxRate", "The argument cannot be less than 0.");
            }
            if (provincialSalesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("provincialSalesTaxRate", "The argument cannot be greater than 1.");
            }
            if (goodsAndServicesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("goodsAndServicesTaxRate", "The argument cannot be less than 0.");
            }
            if (goodsAndServicesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("goodsAndServicesTaxRate", "The argument cannot be greater than 1.");
            }
            this.provincialSalesTaxRate = provincialSalesTaxRate;
            this.goodsAndServicesTaxRate = goodsAndServicesTaxRate;
        }

        /// <summary>
        /// This is an event for when ProvincialSalesTaxRate is changed.
        /// </summary>
        public event EventHandler ProvincialSalesTaxRateChanged;

        /// <summary>
        /// This is an event for when GoodsAndServicesTaxRate is changed.
        /// </summary>
        public event EventHandler GoodsAndServicesTaxRateChanged;

        /// <summary>
        /// This property gets and sets the provincial sales tax rate.
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// </summary>
        public decimal ProvincialSalesTaxRate
        {
            get
            {
                return this.provincialSalesTaxRate;
            }
            set
            {
                if (ProvincialSalesTaxRate < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                else if (ProvincialSalesTaxRate > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The argument cannot be greater than 1.");
                }
                if (value != this.provincialSalesTaxRate)
                {
                    this.provincialSalesTaxRate = value;
                    OnProvincialSalesTaxRateChanged();
                }
            }
        }

        /// <summary>
        /// This property gets and sets the goods and services tax rate.
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// </summary>
        public decimal GoodsAndServicesTaxRate
        {
            get
            {
                return this.goodsAndServicesTaxRate;
            }
            set
            {
                if (GoodsAndServicesTaxRate < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }
                if (GoodsAndServicesTaxRate > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The argument cannot be greater than 1.");
                }
                if (value != this.goodsAndServicesTaxRate)
                {
                    this.goodsAndServicesTaxRate = value;
                    OnGoodsAndServicesTaxRateChanged();
                }
            }
        }

        /// <summary>
        /// This is an abstract property for other classes to set the provincial sales tax charged.
        /// </summary>
        public abstract decimal ProvincialSalesTaxCharged
        {
            get;
        }

        /// <summary>
        /// This is an abstract property for other classes to set the goods and services tax charged.
        /// </summary>
        public abstract decimal GoodsAndServicesTaxCharged
        {
            get;
        }

        /// <summary>
        /// this is an abstract class for other classes to set the subtotal.
        /// </summary>
        public abstract decimal SubTotal
        {
            get;
        }

        /// <summary>
        /// This property gets the total of the subtotal and taxes.
        /// </summary>
        public decimal total
        {
            get
            {
                return SubTotal + provincialSalesTaxRate + goodsAndServicesTaxRate;
            }
        }

        /// <summary>
        /// On method for the ProvincialSalesTaxRateChanged event.
        /// </summary>
        protected virtual void OnProvincialSalesTaxRateChanged()
        {
            if (ProvincialSalesTaxRateChanged != null)
            {
                ProvincialSalesTaxRateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// On method for the GoodsAndServicesTaxRateChanged event.
        /// </summary>
        protected virtual void OnGoodsAndServicesTaxRateChanged()
        {
            if (GoodsAndServicesTaxRateChanged != null)
            {
                GoodsAndServicesTaxRateChanged(this, new EventArgs());
            }
        }
    }
}

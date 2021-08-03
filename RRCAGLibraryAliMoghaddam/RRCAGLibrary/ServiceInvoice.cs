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
    /// This class contains functionality to make invoices for the service department.
    /// The ServiceInvoice class derives from the Invoice class.
    /// </summary>
    public class ServiceInvoice : Invoice
    {

        /// <summary>
        /// This constructor takes in the provincial sales tax and the
        /// goods and services tax.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <param name="provincialTaxRate">This is the variable for the provincial sales tax.</param>
        /// <param name="goodsAndServicesTaxRate">This is the variable for the goods and services tax.</param>
        public ServiceInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate) : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {

        }

        /// <summary>
        /// This is an event for when CostAdded is changed.
        /// </summary>
        public event EventHandler CostAdded;

        /// <summary>
        /// This is a read-only property for labour cost.
        /// </summary>
        public decimal LabourCost
        {
            get; private set;
        }

        /// <summary>
        /// This is a read-only property for parts cost.
        /// </summary>
        public decimal PartsCost
        {
            get; private set;
        }

        /// <summary>
        /// This is a read-only property for material cost.
        /// </summary>
        public decimal MaterialCost
        {
            get; private set;
        }

        /// <summary>
        /// This property returns the provincial sales tax charged.
        /// </summary>
        public override decimal ProvincialSalesTaxCharged
        {
            get
            {
                //This adds the parts cost to the material cost
                //then multiplies that number by the provincial tax rate.
                return this.ProvincialSalesTaxRate * (this.PartsCost + this.MaterialCost);
            }
        }

        /// <summary>
        /// This property returns the goods and services tax charged.
        /// </summary>
        public override decimal GoodsAndServicesTaxCharged
        {
            get
            {
                return this.GoodsAndServicesTaxRate * this.SubTotal;
            }
        }

        /// <summary>
        /// This property calculates the subtotal by adding the labour, material and parts cost.
        /// </summary>
        public override decimal SubTotal
        {
            get
            {
                return this.LabourCost + this.MaterialCost + this.PartsCost;
            }
        }

        /// <summary>
        /// This property calculates the total cost by adding tax to the subtotal.
        /// </summary>
        public decimal Total
        {
            get
            {
                return this.SubTotal + this.ProvincialSalesTaxCharged + this.GoodsAndServicesTaxCharged;
            }
        }

        /// <summary>
        /// This method allows you to add the costs for parts, labour and material.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified enum is not a valid value.</exception>
        /// <param name="type"> This is the type of cost to add.</param>
        /// <param name="amount">This is the amount of money to charge.</param>
        public void AddCost(CostType type, decimal amount)
        {
            if (!Enum.IsDefined(typeof(CostType), type))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("The value is an invalid enumeration value.");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount", "The amount cannot be less than 0.");
            }

            //This if statement determines what to charge based on the cost type.
            if (type == CostType.Labour)
            {
                this.LabourCost += amount;
            }
            else if (type == CostType.Part)
            {
                this.PartsCost += amount;
            }
            else if (type == CostType.Material)
            {
                this.MaterialCost += amount;
            }
            OnCostAdded();
        }
        /// <summary>
        /// On method for the CostAdded event.
        /// </summary>
        protected virtual void OnCostAdded()
        {
            if (CostAdded != null)
            {
                CostAdded(this, new EventArgs());
            }
        }
    }
}

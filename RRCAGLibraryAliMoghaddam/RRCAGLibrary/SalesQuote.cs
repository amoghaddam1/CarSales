/*
 * Name: Ali Moghaddam
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2019-09-12
 * Updated: 2019-10-22
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moghaddam.Ali.Business
{
    /// <summary>
    /// This class takes in sales numbers and features chosen by the customer in order
    /// to output the price of the vehicle.
    /// </summary>
    public class SalesQuote
    {
        private decimal vehicleSalePrice;
        private decimal tradeInAmount;
        private decimal salesTaxRate;
        private Accessories accessoriesChosen;
        private ExteriorFinish exteriorFinishChosen;
        /// <summary>
        /// This constructor takes in the sales information of a vehicle as well as 
        /// chosen accessories and exterior finish.
        /// </summary>
        /// <param name="vehicleSalePrice">The vehicle sales price.</param>
        /// <param name="tradeInAmount">The trade in amount.</param>
        /// <param name="salesTaxRate">The sales tax rate.</param>
        /// <param name="accessoriesChosen">The accessories chosen.</param>
        /// <param name="exteriorFinishChosen">The exterior finish chosen.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified enum is not a valid value.</exception>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount,
        decimal salesTaxRate, Accessories accessoriesChosen,
        ExteriorFinish exteriorFinishChosen)
        {
            if (vehicleSalePrice <= 0)
            {
                throw new ArgumentOutOfRangeException("vehicleSalesPrice", "The argument cannot be less than or equal to 0.");
            }
            if (tradeInAmount < 0)
            {
                throw new ArgumentOutOfRangeException("tradeInAmount", "The argument cannot be less than 0.");
            }
            if (salesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be less than 0.");
            }
            if (salesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be greater than 1.");
            }
            if (!Enum.IsDefined(typeof(Accessories), accessoriesChosen))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("The argument is an invalid enumeration value.");
            }
            if (!Enum.IsDefined(typeof(ExteriorFinish), exteriorFinishChosen))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("The argument is an invalid enumeration value.");
            }
            this.vehicleSalePrice = vehicleSalePrice;
            this.tradeInAmount = tradeInAmount;
            this.salesTaxRate = salesTaxRate;
            this.accessoriesChosen = accessoriesChosen;
            this.exteriorFinishChosen = exteriorFinishChosen;
        }

        /// <summary>
        /// This constructor takes in the sales information of a vehicle with no 
        /// accessories or sales information.
        /// </summary>
        /// <param name="vehicleSalePrice">The vehicle sales price.</param>
        /// <param name="tradeInAmount">The trade in amount.</param>
        /// <param name="salesTaxRate"> The sales tax rate.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount, decimal salesTaxRate)
         : this(vehicleSalePrice, tradeInAmount, salesTaxRate, Accessories.None, ExteriorFinish.None)
        {

        }

        /// <summary>
        /// This is an event for when VehicleSalesPrice is changed.
        /// </summary>
        public event EventHandler VehiclePriceChanged;

        /// <summary>
        /// This is an event for when TradeInPrice is changed.
        /// </summary>
        public event EventHandler TradeInPriceChanged;

        /// <summary>
        /// This is an event for when AccessoriesChosen is changed.
        /// </summary>
        public event EventHandler AccessoriesChosenChanged;

        /// <summary>
        /// This is an event for when ExteriorFinishChosen is changed.
        /// </summary>
        public event EventHandler ExteriorFinishChanged;

        /// <summary>
        /// This property gets and sets the sale price of the vehicle.
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// </summary>
        public decimal VehicleSalesPrice
        {
            get
            {
                return this.vehicleSalePrice;
            }
            set
            {
                if (VehicleSalesPrice <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                if (value != this.vehicleSalePrice)
                {
                    this.vehicleSalePrice = value;
                    OnVehiclePriceChanged();
                }
            }

        }

        /// <summary>
        /// This property gets and sets the trade in amount.
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// </summary>
        public decimal TradeInAmount
        {
            get
            {
                return this.tradeInAmount;
            }
            set
            {
                if (TradeInAmount < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                if (value != this.tradeInAmount)
                {
                    this.tradeInAmount = value;
                    OnTradeInPriceChanged();
                }
            }
        }

        /// <summary>
        /// This property Gets and sets the chosen accessories.
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified enum is not a valid value.</exception>
        /// </summary>
        public Accessories AccessoriesChosen
        {
            get
            {
                return this.accessoriesChosen;
            }
            set
            {
                if (!Enum.IsDefined(typeof(Accessories), value))
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException("The value is an invalid enumeration value.");
                }

                if (value != this.accessoriesChosen)
                {
                    OnAccessoriesChosenChanged();
                    this.accessoriesChosen = value;
                }
            }

        }

        /// <summary>
        /// This property Gets and sets the chosen exterior finish.
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The specified enum is not a valid value.</exception>
        /// </summary>
        public ExteriorFinish ExteriorFinishChosen
        {
            get
            {
                return this.exteriorFinishChosen;
            }
            set
            {
                if (!Enum.IsDefined(typeof(ExteriorFinish), value))
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException("The value is an invalid enumeration value.");
                }
                if (value != this.exteriorFinishChosen)
                {
                    OnExteriorFinishChanged();
                    this.exteriorFinishChosen = value;
                }
            }
        }

        /// <summary>
        /// This property gets the cost of the chosen accessories.
        /// </summary>
        public decimal AccessoriesCost
        {
            get
            {
                const decimal STEREO_SYSTEM = 505.05M;
                const decimal LEATHER_INTERIOR = 1010.10M;
                const decimal COMPUTER_NAVIGATION = 1515.15M;
                decimal cost = 0M;

                //If statements to decide the price based on what accessory is chosen
                if (accessoriesChosen == Accessories.StereoSystem)
                {
                    cost = STEREO_SYSTEM;
                }
                else if (accessoriesChosen == Accessories.LeatherInterior)
                {
                    cost = LEATHER_INTERIOR;
                }
                else if (accessoriesChosen == Accessories.CumputerNavigation)
                {
                    cost = COMPUTER_NAVIGATION;
                }
                else if (accessoriesChosen == Accessories.StereoAndLeather)
                {
                    cost = STEREO_SYSTEM + LEATHER_INTERIOR;
                }
                else if (accessoriesChosen == Accessories.StereoandNavigation)
                {
                    cost = STEREO_SYSTEM + COMPUTER_NAVIGATION;
                }
                else if (accessoriesChosen == Accessories.LeatherAndNavigation)
                {
                    cost = LEATHER_INTERIOR + COMPUTER_NAVIGATION;
                }
                else if (accessoriesChosen == Accessories.All)
                {
                    cost = STEREO_SYSTEM + LEATHER_INTERIOR + COMPUTER_NAVIGATION;
                }
                else
                {
                    cost = 0;
                }
                return cost;
            }
        }

        /// <summary>
        /// this property gets the cost of the chosen exterior finish.
        /// </summary>
        public decimal FinishCost

        {
            get
            {
                //These if statements determine the exterior finish cost based on the exterior finish chosen.
                if (exteriorFinishChosen == ExteriorFinish.Standard)
                {
                    return 202.02M;
                }
                else if (exteriorFinishChosen == ExteriorFinish.Pearlized)
                {
                    return 404.04M;
                }
                else if (exteriorFinishChosen == ExteriorFinish.Custom)
                {
                    return 606.06M;
                }
                else if (exteriorFinishChosen == ExteriorFinish.None)
                {
                    return 0M;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// This property gets sub total cost based on the vehicle's sale price
        /// as well as the Accessory and exterior finish chosen if applicable.
        /// </summary>
        public decimal SubTotal
        {
            get
            {
                //This return returns the vehicle price plus the accessory and exterior finish price.
                return Math.Round(this.vehicleSalePrice + this.AccessoriesCost +
                this.FinishCost, 2);
            }
        }

        /// <summary>
        /// This property calculates and gets the amount of tax to charge
        /// based on the subtotal.
        /// </summary>
        public decimal SalesTax
        {
            get
            {
                return Math.Round(this.salesTaxRate * this.SubTotal, 2);
            }
        }

        /// <summary>
        /// This property calculates the total cost using the sub total 
        /// and the sales tax rate.
        /// </summary>
        public decimal Total
        {
            get
            {
                return Math.Round(this.SubTotal * (1 + this.salesTaxRate), 2);
            }
        }

        /// <summary>
        /// This property calculates the amount due by subtracting the 
        /// trade in amount from the total.
        /// </summary>
        public decimal AmountDue
        {
            get
            {
                return Math.Round(this.Total - this.tradeInAmount, 2);
            }
        }

        /// <summary>
        /// On method for the VehiclePriceChanged event.
        /// </summary>
        protected virtual void OnVehiclePriceChanged()
        {
            if (VehiclePriceChanged != null)
            {
                VehiclePriceChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// On method for the TradeInPriceChanged event.
        /// </summary>
        protected virtual void OnTradeInPriceChanged()
        {
            if (TradeInPriceChanged != null)
            {
                TradeInPriceChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// On method for the AccessoriesChosenChanged event.
        /// </summary>
        protected virtual void OnAccessoriesChosenChanged()
        {
            if (AccessoriesChosenChanged != null)
            {
                AccessoriesChosenChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// On method for the ExteriorFinishChanged event.
        /// </summary>
        protected virtual void OnExteriorFinishChanged()
        {
            if (ExteriorFinishChanged != null)
            {
                ExteriorFinishChanged(this, new EventArgs());
            }
        }
    }
}



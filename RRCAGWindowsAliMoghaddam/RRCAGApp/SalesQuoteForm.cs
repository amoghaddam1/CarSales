using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moghaddam.Ali.Business;

namespace RRCAGApp
{
    public partial class SalesQuoteForm : Form
    {
        //Variable defined so that the update events only happen once.
        int one = 1;

        public SalesQuoteForm()
        {
            InitializeComponent();

            this.btnCalculateQuote.Click += BtnCalculateQuote_Click;
            this.btnReset.Click += BtnReset_Click;

            //clears the labels when the text inputs are changed.
            this.txtVehicleSalePrice.TextChanged += TxtVehicleSalePrice_TextChanged;
            this.txtTradeInValue.TextChanged += TxtTradeInValue_TextChanged;
        }

        /// <summary>
        /// Handles the event of the text box for trade in value being changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTradeInValue_TextChanged(object sender, EventArgs e)
        {
            Cleared();
        }

        /// <summary>
        /// Handles the event of the text box for vehicle sale price being changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtVehicleSalePrice_TextChanged(object sender, EventArgs e)
        {
            Cleared();
        }

        /// <summary>
        /// Handles the click event of the reset button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            DialogResult reset = MessageBox.Show("do you want to reset the form?", "Reset Form", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if(reset == DialogResult.Yes)
            {
                txtVehicleSalePrice.Text = null;
                txtTradeInValue.Text = 0.ToString();
                chkStereoSystem.Checked = false;
                chkLeatherInterior.Checked = false;
                chkComputerNavigation.Checked = false;
                radStandard.Checked = true;
                radPearlized.Checked = false;
                radCustom.Checked = false;
                nudAnnualInterestRate.Value = 5;
                nudNumberOfYears.Value = 1;
                Cleared();
                errorProvider.Clear();
            }
        }

        /// <summary>
        /// Handles the click event of the calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalculateQuote_Click(object sender, EventArgs e)
        {
            //If statement to make the events only happen once.
            if (one <= 1)
            {
                //Updates the form when you change one of the check boxes.
                this.chkLeatherInterior.CheckedChanged += BtnCalculateQuote_Click;
                this.chkComputerNavigation.CheckedChanged += BtnCalculateQuote_Click;
                this.chkStereoSystem.CheckedChanged += BtnCalculateQuote_Click;

                //Updates the form when you change one of the radio buttons.
                this.radStandard.CheckedChanged += BtnCalculateQuote_Click;
                this.radPearlized.CheckedChanged += BtnCalculateQuote_Click;
                this.radCustom.CheckedChanged += BtnCalculateQuote_Click;

                //Updates the form when you change the interest rate or number of years.
                this.nudAnnualInterestRate.ValueChanged += BtnCalculateQuote_Click;
                this.nudNumberOfYears.ValueChanged += BtnCalculateQuote_Click;
                one++;
            }
            //This check verifies that the inputs are valid.
            bool check = verify();

            //If the inputs are valid the event happens.
            if (check)
            {
                decimal vehicleSalesPrice = Decimal.Parse(this.txtVehicleSalePrice.Text);
                decimal tradeInAmount = Decimal.Parse(this.txtTradeInValue.Text);
                Accessories accessories;
                ExteriorFinish exteriorFinish;
                decimal tax = 0.13M;

                //These if statements determine which accessories are selected and set the accessories variable accordingly. 
                if (this.chkStereoSystem.Checked)
                {
                    if (this.chkLeatherInterior.Checked)
                    {
                        if (this.chkComputerNavigation.Checked)
                        {
                            accessories = Accessories.All;
                        }
                        else
                        {
                            accessories = Accessories.StereoAndLeather;
                        }
                    }
                    else if (this.chkComputerNavigation.Checked)
                    {
                        accessories = Accessories.StereoandNavigation;
                    }
                    else
                    {
                        accessories = Accessories.StereoSystem;
                    }
                }
                else if (this.chkLeatherInterior.Checked)
                {
                    if (this.chkComputerNavigation.Checked)
                    {
                        accessories = Accessories.LeatherAndNavigation;
                    }
                    else
                    {
                        accessories = Accessories.LeatherInterior;
                    }
                }
                else if (this.chkComputerNavigation.Checked)
                {
                    accessories = Accessories.CumputerNavigation;
                }
                else
                {
                    accessories = Accessories.None;
                }

                //these if statements determine which exterior finish is chosen and set the exterior finish variable accordingly.

                if (this.radStandard.Checked)
                {
                    exteriorFinish = ExteriorFinish.Standard;
                }
                else if (this.radPearlized.Checked)
                {
                    exteriorFinish = ExteriorFinish.Pearlized;
                }
                else if (this.radCustom.Checked)
                {
                    exteriorFinish = ExteriorFinish.Custom;
                }
                else
                {
                    exteriorFinish = ExteriorFinish.None;
                }

                //This creates instantiates an object of the SalesQuote class with the variables from above.
                SalesQuote salesQuote = new SalesQuote(vehicleSalesPrice, tradeInAmount, tax, accessories, exteriorFinish);

                //Sets the vehicleSalePrice label.
                this.lblVehicleSalePrice.Text = "$" + String.Format("{0:0.00}", vehicleSalesPrice);

                //Sets the options label.
                this.lblOptions.Text = (salesQuote.AccessoriesCost + salesQuote.FinishCost).ToString();

                //Sets the subtotal label.
                this.lblSubtotal.Text = "$" + String.Format("{0:0.00}", salesQuote.SubTotal);

                //Sets the Sales Tax label.
                this.lblSalesTax.Text = salesQuote.SalesTax.ToString();

                //Sets the Total Label
                this.lblTotal.Text = "$" + String.Format("{0:0.00}", salesQuote.Total);

                //Sets the Trade-In Label.
                this.lblTradeIn.Text = "-" + salesQuote.TradeInAmount;

                //Sets the Amount Due label.
                this.lblAmountDue.Text = "$" + String.Format("{0:0.00}", salesQuote.AmountDue);

                //sets the Monthly Payment label.
                this.lblMonthlyPayment.Text = "$" + String.Format("{0:0.00}", Financial.GetPayment(this.nudAnnualInterestRate.Value / 100, Decimal.ToInt32(this.nudNumberOfYears.Value) * 12, salesQuote.AmountDue).ToString());
            }
        }

        /// <summary>
        /// This method verifies the inputs passed in to make sure that they are valid.
        /// </summary>
        /// <returns></returns>
        private bool verify()
        {
            errorProvider.Clear();
            bool vehicleSalePriceCheck = true;
            bool tradeInCheck = true;
            decimal parse;

            //checking the vehicle sale price is valid.
            if(string.IsNullOrEmpty(this.txtVehicleSalePrice.Text))
            {
                errorProvider.SetIconPadding(txtVehicleSalePrice, 3);
                errorProvider.SetError(txtVehicleSalePrice, "Vehicle price is a required field.");
                vehicleSalePriceCheck = false;
            }
            else if (!decimal.TryParse(this.txtVehicleSalePrice.Text, out parse))
            {
                errorProvider.SetIconPadding(txtVehicleSalePrice, 3);
                errorProvider.SetError(txtVehicleSalePrice, "Vehicle price cannot contain letters or special characters.");
                vehicleSalePriceCheck = false;
            }
            else if (Decimal.Parse(this.txtVehicleSalePrice.Text) <= 0)
            {
                errorProvider.SetIconPadding(txtVehicleSalePrice, 3);
                errorProvider.SetError(txtVehicleSalePrice, "Vehicle price cannot be less than or equal to 0.");
                vehicleSalePriceCheck = false;
            }

            //checking the trade in value is valid.
            if (string.IsNullOrEmpty(this.txtTradeInValue.Text))
            {
                errorProvider.SetIconPadding(txtTradeInValue, 3);
                errorProvider.SetError(txtTradeInValue, "Trade-in value is a required field.");
                tradeInCheck = false;
            }
            else if (!decimal.TryParse(this.txtTradeInValue.Text, out parse))
            {
                errorProvider.SetIconPadding(txtTradeInValue, 3);
                errorProvider.SetError(txtTradeInValue, "Trade-in value cannot contain letters or special characters.");
                tradeInCheck = false;
            }
            else if (Decimal.Parse(this.txtTradeInValue.Text) < 0)
            {
                errorProvider.SetIconPadding(txtTradeInValue, 3);
                errorProvider.SetError(txtTradeInValue, "Trade-in value cannot be less than 0.");
                tradeInCheck = false;
            }
            if(vehicleSalePriceCheck && tradeInCheck && Decimal.Parse(this.txtVehicleSalePrice.Text) < Decimal.Parse(this.txtTradeInValue.Text))
            {
                errorProvider.SetIconPadding(txtTradeInValue, 3);
                errorProvider.SetError(txtTradeInValue, "Trade-in value cannot exceed the vehicle sale price.");
                tradeInCheck = false;
            }
            if (vehicleSalePriceCheck && tradeInCheck)
            {
                errorProvider.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method clears the labels in the form.
        /// </summary>
        public void Cleared()
        {
            lblVehicleSalePrice.Text = null;
            lblOptions.Text = null;
            lblSubtotal.Text = null;
            lblSalesTax.Text = null;
            lblTotal.Text = null;
            lblTradeIn.Text = null;
            lblAmountDue.Text = null;
            lblMonthlyPayment.Text = null;
        }
    }
}

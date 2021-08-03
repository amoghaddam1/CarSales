/*
 * Name: Ali Moghaddam
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2019-09-12
 * Updated: 2019-09-16
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Moghaddam.Ali.Business
{
    /// <summary>
    /// This class calculates the payment.
    /// </summary>
    public static class Financial
    {
        /// <summary>
        /// This class calculates payment rates and the number of payment periods 
        /// based on information from the other classes.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified value is outside the allowed range.</exception>
        /// <param name="rate">This variable hold the rate.</param>
        /// <param name="numberOfPaymentPeriods">This variable holds the number of payment periods.</param>
        /// <param name="presentValue">This variable hold the present value.</param>
        /// <returns></returns>
        public static decimal GetPayment(decimal rate, int numberOfPaymentPeriods, decimal presentValue)
        {
            decimal futureValue = 0;
            decimal type = 0;
            decimal payment = 0;

            if (rate < 0)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be less than 0.");
            }
            if (rate > 1)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be greater than 1.");
            }
            if (numberOfPaymentPeriods <= 0)
            {
                throw new ArgumentOutOfRangeException("numberOfPaymentPeriods", "The argument cannot be less than or equal to 0.");
            }
            if (presentValue <= 0)
            {
                throw new ArgumentOutOfRangeException("presentValue", "The argument cannot be less than or equal to 0.");
            }
            if (rate == 0)
                payment = presentValue / numberOfPaymentPeriods;
            else
                payment = rate * (futureValue + presentValue * (decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods)) /
                                (((decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods) - 1) * (1 + rate * type));

            return Math.Round(payment, 2);
        }
    }
}

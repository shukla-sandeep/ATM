using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ATM.Model;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private Account[] accounts;
        /// <summary>
        /// Inject accounts object with account array details
        /// </summary>
        /// <param name="_accounts"></param>
        public SampleDataController(IAccounts _accounts)
        {
            this.accounts = (_accounts as AccountData)._account;
        }
        /// <summary>
        /// Authenticate the user with card number and pin
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public bool Authenticate()
        {
            bool result = false;
            try
            {
                decimal cardNumber = decimal.Parse(Request.Headers["cardNumber"]);
                int pin = int.Parse(Request.HttpContext.Request.Headers["pin"]);
                result = accounts.Any(p => p.cardNumber == cardNumber && p.pin == pin);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                //log exception in logger
            }
            return result;
        }
        /// <summary>
        /// Check the user account balance
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public decimal? Balance()
        {
            decimal? result = null;
            try
            {
                decimal cardNumber = decimal.Parse(Request.Headers["cardNumber"]);
                Account account = accounts.Where(p => p.cardNumber == cardNumber).FirstOrDefault();
                result = accounts.Where(p => p.cardNumber == cardNumber).FirstOrDefault().amount;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                //log exception in logger
            }
            return result;
        }
        /// <summary>
        /// withdraw amount from selected user account
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public decimal? Withdraw([FromBody]dynamic param)
        {
            decimal? result = null;
            try
            {
                decimal cardNumber = decimal.Parse(param.cardNumber.Value);
                int requestedAmount = int.Parse(param.amount.Value);
                Account account = accounts.Where(p => p.cardNumber == cardNumber).FirstOrDefault();
                if (requestedAmount < account.amount)
                {
                    account.amount = account.amount - requestedAmount;
                    result = account.amount;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                //log exception in logger
            }
            return result;
        }
    }
}

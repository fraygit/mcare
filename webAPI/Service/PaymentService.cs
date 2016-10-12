using mcare.API.paymentexpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Service
{
    public class PaymentService
    {
        public TransactionResult2 Rebill(string amount, string creditcardNumber2)
        {
            var pe = new paymentexpress.PaymentExpressWS();
            var response = pe.SubmitTransaction("ParkoDev1149", "test1234", new paymentexpress.TransactionDetails
            {
                amount = amount.ToString(),
                cardNumber2 = creditcardNumber2,
                inputCurrency = "NZD",
                txnType = "Purchase",
            });
            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;


    public class GopayException : Exception
    {
        /// <summary>
        /// Seznam duvodu
        /// </summary>
        public enum Reason
        {
            OTHER,
            NO_PAYMENT_STATUS,
            INVALID_ON,
            INVALID_GOID,
            INVALID_SIGNATURE,
            INVALID_CALL_STATE_STATE,
            INVALID_SESSION_STATE,
            INVALID_PAYMENT_SESSION_ID,
            INVALID_PN,
            INVALID_PRICE,
            INVALID_CURRENCY,
            INVALID_STATUS_SIGNATURE,
            INVALID_COUNTRY_CODE
        }

        private Reason reason = Reason.OTHER;

        /// <summary>
        /// Vraci duvod vyjimky
        /// </summary>
        /// <returns>duvod</returns>
        public Reason GetReason()
        {
            return this.reason;
        }

        public GopayException(string message) : base(message)
        {
        }

        public GopayException(string message, Reason reason) : base(message)
        {
            this.reason = reason;
        }

        public GopayException(Reason reason) : base(reason.ToString())
        {
            this.reason = reason;
        }
    }

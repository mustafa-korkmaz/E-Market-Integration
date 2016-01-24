using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Common
{
    public class Enumerations
    {
    }

    public enum IntegrationType
    {
        Opencart=0,
        Magento,
        NopCommerce
    }

    public enum ResponseCode
    {
        Fail = 0,
        Success,
        Warning,
        Info,
        NoEffect
    }
}
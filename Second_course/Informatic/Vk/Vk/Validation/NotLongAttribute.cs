using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk.Validation
{
    public class NotLongAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value.ToString().Length <= 50)
                return true;
            ErrorMessage = "Слишком много символов в названии";
            return false;
        }
    }
}

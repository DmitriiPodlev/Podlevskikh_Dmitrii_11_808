using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk.Validation
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value.ToString() != "")
                return true;
            ErrorMessage = "Поле должно быть заполнено!";
            return false;
        }
    }
}

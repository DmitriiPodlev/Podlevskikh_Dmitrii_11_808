using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk.Validation
{
    public class Validation
    {
        public static ValidationResult Validate(object obj)
        {
            // здесь пробежать по всем полям модели с использованием рефлексии
            // и если они имеют атрибут потомок ValidationAttribute
            // вызвать соответствующий метод IsValid
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (ValidationAttribute attribute in attributes)
                    if (!attribute.IsValid(property.GetValue(obj)))
                        return new ValidationResult(false, $"{property.Name} нет атрибута!");
            }

            return new ValidationResult(true);
        }
    }
}

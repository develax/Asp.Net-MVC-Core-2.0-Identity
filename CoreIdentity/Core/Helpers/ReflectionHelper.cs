using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreIdentity.Core.Helpers
{
    public static class ReflectionHelper
    {
        public static void SetPublicFieldValue<T>(string fieldName, object fieldValue)
        {
            FieldInfo fieldInfo = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);

            if (fieldInfo == null)
                throw new NullReferenceException(string.Format("Static field '{0}' not found in '{1}' class", fieldName, nameof(T)));

            fieldInfo.SetValue(null, fieldValue);
        }
    }
}

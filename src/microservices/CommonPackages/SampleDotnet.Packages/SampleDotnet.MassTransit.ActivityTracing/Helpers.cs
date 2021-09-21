using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public static class Helpers
    {
       
    }

    internal static class ObjectToDictionaryHelper
    {
        internal static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        internal static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                AddPropertyToDictionary<T>(property, source, dictionary);
            }
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }
    }
}

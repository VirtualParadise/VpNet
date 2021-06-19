using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace VpNet.Extensions
{
    /// <summary>
    /// Serialization Extensions for serializable objects.
    /// </summary>
    public static class SerializableExtensions
    {
        /// <summary>
        /// Copies public properties from another object into your own object. 
        /// Both objects must derive from the same abstact class.
        /// 
        /// Note: Only the extended properties defined in the declaring type T1 are overwritten.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="otherObject">The other object.</param>
        /// <returns></returns>
        //internal static T1 CopyFrom<T1, T2>(this T1 obj, T2 otherObject)
        //    where T1 : class
        //    where T2 : class
        //{
        //    var srcFields = otherObject.GetType().GetProperties(
        //        BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

        //    var destFields = obj.GetType().GetProperties(
        //        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

        //    foreach (var property in srcFields)
        //    {
        //        var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
        //        if (dest != null && dest.DeclaringType == typeof(T1) /* TODO: only copy the properties from the declaring type with proper presedence */ && dest.CanWrite)
        //            dest.SetValue(obj, property.GetValue(otherObject, null), null);
        //    }

        //    return obj;
        //}

        public static T1 CopyFrom<T1, T2>(this T1 obj, T2 otherObject, bool isCopyOnlyBaseProperties)
            where T1 : class, new()
            where T2 : class
        {
            if (obj==null)
                obj = new T1();
            PropertyInfo[] srcFields;

            if (isCopyOnlyBaseProperties)
            {
                srcFields = otherObject.GetType().BaseType.GetProperties(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            }
            else
            {
                srcFields = otherObject.GetType().GetProperties(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            }

            var destFields = obj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.CanWrite)
                    dest.SetValue(obj, property.GetValue(otherObject, null), null);
            }

            return obj;
        }
    }
}

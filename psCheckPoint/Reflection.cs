using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint
{
    internal static class Reflection
    {
        #region Methods

        internal static void Add<T>(this MemberMembershipChangeTracking<T> obj, MembershipActions action, string[] values) where T : IObjectSummary
        {
            switch (action)
            {
                case MembershipActions.Add:
                    foreach (var v in values)
                        obj.Add(v);
                    break;

                case MembershipActions.Remove:
                    foreach (var v in values)
                        obj.Remove(v);
                    break;

                case MembershipActions.Replace:
                    obj.Clear();
                    if (values != null)
                        foreach (var v in values)
                            obj.Add(v);
                    break;
            }
        }

        internal static T GetProperty<T>(this IObjectSummary obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (name == null) throw new ArgumentNullException(nameof(name));

            PropertyInfo prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanRead)
                return (T)prop.GetValue(obj);
            else
                throw new ArgumentException("No property found.");
        }

        internal static void SetProperty(this IObjectSummary obj, string name, object value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (name == null) throw new ArgumentNullException(nameof(name));

            PropertyInfo prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                if (value is SwitchParameter sp) value = sp.IsPresent;

                prop.SetValue(obj, value, null);
            }
            else
            {
                throw new ArgumentException("No writeable property found.");
            }
        }

        #endregion Methods
    }
}
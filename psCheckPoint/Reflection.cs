using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System;
using System.Management.Automation;
using System.Reflection;

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
                    foreach (string v in values)
                        obj.Add(v);
                    break;

                case MembershipActions.Remove:
                    foreach (string v in values)
                        obj.Remove(v);
                    break;

                case MembershipActions.Replace:
                    obj.Clear();
                    if (values != null)
                        foreach (string v in values)
                            obj.Add(v);
                    break;
            }
        }

        internal static void Add<T>(this MembershipChangeTracking<T> obj, MembershipActions action, string[] values)
        {
            switch (action)
            {
                case MembershipActions.Add:
                    foreach (string v in values)
                        obj.Add(v);
                    break;

                case MembershipActions.Remove:
                    foreach (string v in values)
                        obj.Remove(v);
                    break;

                case MembershipActions.Replace:
                    obj.Clear();
                    if (values != null)
                        foreach (string v in values)
                            obj.Add(v);
                    break;
            }
        }

        internal static T GetProperty<T>(this IObjectSummary obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (name == null) throw new ArgumentNullException(nameof(name));

            var prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanRead)
                return (T)prop.GetValue(obj);
            else
                throw new ArgumentException("No property found.");
        }

        internal static void SetProperty(this IObjectSummary obj, string name, object value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (name == null) throw new ArgumentNullException(nameof(name));

            var prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
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
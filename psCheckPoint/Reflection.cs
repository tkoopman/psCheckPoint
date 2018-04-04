using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint
{
    internal static class Reflection
    {
        #region Methods

        private static readonly string[] IgnoredProperties = {
            "Debug",
            "ErrorAction",
            "ErrorVariable",
            "Ignore",
            "InformationAction",
            "InformationVariable",
            "OutBuffer",
            "OutVariable",
            "PassThru",
            "PipelineVariable",
            "Session",
            "Verbose",
            "WarningAction",
            "WarningVariable"
        };

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

        internal static void SetProperty(this IObjectSummary obj, string name, object value)
        {
            // Ignore all standard Powershell property names
            if (IgnoredProperties.Contains(name)) return;

            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (name == null) throw new ArgumentNullException(nameof(name));

            PropertyInfo prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
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
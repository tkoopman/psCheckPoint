using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace psCheckPoint.Extra.Sync
{
    /// <summary>
    /// <para type="synopsis">Result of New-SyncMember.</para>
    /// <para type="description">Details a single source member objects to sync with Check Point group</para>
    /// </summary>
    /// <example>
    /// <code>{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup</code>
    /// </example>
    public class Member
    {
        public Member(string name, string iP, bool nameIsPrefix = true)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IP = iP ?? throw new ArgumentNullException(nameof(iP));

            if (nameIsPrefix)
            {
                Name += ip.ToString();
                if (IsNetwork())
                {
                    Name += $"/{CIDR}";
                }
            }
        }

        public string Name { get; set; }

        private string _IP;
        private IPAddress ip;

        public int CIDR { get; private set; }

        public string IP
        {
            get => _IP;
            set
            {
                string[] ipParts = value.Split('/');
                ip = IPAddress.Parse(ipParts[0]);
                _IP = ip.ToString();

                if (!(IsIPv4() || IsIPv6()))
                {
                    throw new InvalidCastException("Invalid IP address provided. Must be IPv4 or IPv6.");
                }

                if (ipParts.Length == 2)
                {
                    CIDR = int.Parse(ipParts[1]);
                    if (CIDR <= 0 ||
                        (IsIPv4() && CIDR > 32) ||
                        (IsIPv6() && CIDR > 128))
                    {
                        throw new InvalidCastException("Invalid Mask Length provided.");
                    }
                }
                else if (IsIPv4())
                {
                    CIDR = 32;
                }
                else if (IsIPv6())
                {
                    CIDR = 128;
                }
            }
        }

        public bool IsIPv4()
        {
            return (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }

        public bool IsIPv6()
        {
            return (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
        }

        public bool IsHost()
        {
            return CIDR == ((IsIPv4()) ? 32 : 128);
        }

        public bool IsNetwork()
        {
            return !IsHost();
        }
    }
}
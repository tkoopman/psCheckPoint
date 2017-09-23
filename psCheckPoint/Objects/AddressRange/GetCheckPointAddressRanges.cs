﻿using System.Management.Automation;

namespace psCheckPoint.Objects.AddressRange
{
    /// <api cmd="show-address-ranges">Get-CheckPointAddressRanges</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointAddressRanges")]
    [OutputType(typeof(CheckPointObjects<CheckPointAddressRange>))]
    public class GetCheckPointAddressRanges : GetCheckPointObjects
    {
        public override string Command { get { return "show-address-ranges"; } }
    }
}
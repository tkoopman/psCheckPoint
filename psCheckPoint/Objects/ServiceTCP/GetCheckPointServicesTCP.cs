﻿using psCheckPoint.Objects.Service;
using System.Management.Automation;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <api cmd="show-services-tcp">Get-CheckPointServicesTCP</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointServicesTCP")]
    [OutputType(typeof(CheckPointObjects<CheckPointService>))]
    public class GetCheckPointServicesTCP : GetCheckPointObjectsBase<CheckPointObjects<CheckPointService>>
    {
        public override string Command { get { return "show-services-tcp"; } }
    }
}
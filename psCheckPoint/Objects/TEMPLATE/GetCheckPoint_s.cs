﻿using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <api cmd="show-_s">Get-CheckPoint_s</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPoint_s")]
    [OutputType(typeof(CheckPointObjects<CheckPoint_>))]
    public class GetCheckPoint_s : GetCheckPointObjects
    {
        public override string Command { get { return "show-_s"; } }
    }
}
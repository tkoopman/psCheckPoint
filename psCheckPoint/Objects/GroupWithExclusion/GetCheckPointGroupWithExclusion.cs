﻿using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="show-group-with-exclusion">Get-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(CheckPointGroupWithExclusion))]
    public class GetCheckPointGroupWithExclusion : GetCheckPointObject<CheckPointGroupWithExclusion>
    {
        public override string Command { get { return "show-group-with-exclusion"; } }
    }
}
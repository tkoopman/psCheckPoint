﻿using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="show-groups-with-exclusion">Get-CheckPointGroupsWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointGroupsWithExclusion")]
    [OutputType(typeof(CheckPointObjects<CheckPointObject>))]
    public class GetCheckPointGroupsWithExclusion : GetCheckPointObjects
    {
        public override string Command { get { return "show-groups-with-exclusion"; } }
    }
}
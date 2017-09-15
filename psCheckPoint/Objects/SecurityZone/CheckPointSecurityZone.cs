using Newtonsoft.Json;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <summary>
    /// <para type="description">Details of a Check Point Security Zone</para>
    /// </summary>
    public class CheckPointSecurityZone : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointSecurityZone(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
        }
    }
}
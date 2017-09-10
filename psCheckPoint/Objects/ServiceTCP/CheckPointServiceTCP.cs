using psCheckPoint.Objects.Service;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="description">Details of a Check Point TCP Service</para>
    /// </summary>
    public class CheckPointServiceTCP : CheckPointService
    {
        public override string ToString()
        {
            return $"{Name} (tcp/{Port})";
        }
    }
}
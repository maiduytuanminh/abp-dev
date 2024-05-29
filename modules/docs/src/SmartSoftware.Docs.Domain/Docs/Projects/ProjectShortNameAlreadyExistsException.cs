using SmartSoftware;

namespace SmartSoftware.Docs.Projects
{
    public class ProjectShortNameAlreadyExistsException : BusinessException
    {
        public ProjectShortNameAlreadyExistsException(string shortName)
            : base("SmartSoftware.Docs.Domain:010002")
        {
            WithData("ShortName", shortName);
        }
    }
}
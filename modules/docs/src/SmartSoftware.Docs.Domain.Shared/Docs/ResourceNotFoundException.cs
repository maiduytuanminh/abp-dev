using SmartSoftware;

namespace SmartSoftware.Docs
{
    public class ResourceNotFoundException : BusinessException
    {
        public string ResourceName { get; set; }

        public ResourceNotFoundException(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}
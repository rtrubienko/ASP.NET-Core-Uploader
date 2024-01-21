using TestApp.Data.Models;

namespace TestApp.Data.interfaces
{
    public interface IAllJsons
    {
        IEnumerable<UploadJson> JsonNames { get; }
        UploadJson GetUploadJson(int jsonId);
    }
}

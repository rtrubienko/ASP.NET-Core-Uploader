using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using TestApp.Data.interfaces;
using TestApp.Data.Models;

namespace TestApp.Data.Repository
{
    public class JsonRepository : IAllJsons
    {

        private readonly AppDbContent appDbContent;

        public JsonRepository(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }

        public IEnumerable<UploadJson> JsonNames => appDbContent.UploadJson;

        public UploadJson GetUploadJson(int jsonId)
        {
            return appDbContent.UploadJson.FirstOrDefault(j => j.id == jsonId);
        }
    }
}

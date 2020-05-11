using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microservices.Helper
{
    public static class JsonConvertHelper
    {
        public static async Task<string> GetIgnoreLooping(object obj)
        {
            return await JsonConvert.SerializeObjectAsync(obj, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }).ConfigureAwait(false);
        }
    }
}

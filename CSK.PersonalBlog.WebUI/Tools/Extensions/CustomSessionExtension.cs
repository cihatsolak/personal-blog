using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace CSK.PersonalBlog.WebUI.Tools.Extensions
{
    public static class CustomSessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonData);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            var data = session.GetString(key);

            if (data == null)
                return Activator.CreateInstance<T>();

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}

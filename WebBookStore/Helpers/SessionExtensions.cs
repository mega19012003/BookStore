using System.Text.Json;

namespace WebBookStore.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
            => session.SetString(key, JsonSerializer.Serialize(value));

        public static T? GetObjectFromJson<T>(this ISession session, string key)
            => session.GetString(key) is { } str
               ? JsonSerializer.Deserialize<T>(str)
               : default;
    }
}

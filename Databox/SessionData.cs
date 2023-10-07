using Newtonsoft.Json;
using System;

namespace Databox
{
    [Serializable]
    public class SessionData
    {
        public int UserId { get; set; }
        public string AccountType { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public DateTime ExpirationTime { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static SessionData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<SessionData>(json);
        }
    }
}

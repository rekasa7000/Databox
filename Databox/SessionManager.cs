using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Databox
{
    public static class SessionManager
    {
        private static string sessionFilePath = "session.json";

        public static void SaveSession(SessionData sessionData)
        {
            try
            {
                string json = sessionData.ToJson();
                File.WriteAllText(sessionFilePath, json);
                Console.WriteLine("Session data saved: " + json);
                Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
                Console.WriteLine("Session file exists: " + File.Exists(sessionFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving session data: " + ex.Message);
            }
        }

        public static SessionData LoadSession()
        {
            SessionData sessionData = null;
            try
            {
                Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
                Console.WriteLine("Session file exists: " + File.Exists(sessionFilePath));

                if (File.Exists(sessionFilePath))
                {
                    string json = File.ReadAllText(sessionFilePath);
                    sessionData = SessionData.FromJson(json);
                    Console.WriteLine("Session data loaded: " + json);
                }
                else
                {
                    Console.WriteLine("No session data file found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading session data: " + ex.Message);
            }
            return sessionData;
        }

        public static void ClearSession()
        {
            try
            {
                if (File.Exists(sessionFilePath))
                {
                    File.Delete(sessionFilePath);
                    Console.WriteLine("Logout Successful.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error clearing session data: " + ex.Message);
            }
        }
    }
}

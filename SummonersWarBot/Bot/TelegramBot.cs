using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SummonersWarBot.Bot
{
    public class TelegramBot
    {

        private static readonly string token = "1024728048:AAEr5e1UCiyNjyiWLwrENulDKeYfug7qxVs"; // bot token for auth with telegram api
        private static readonly string apiUri = string.Format("https://api.telegram.org/bot{0}/", token);
        private static readonly HttpClient http = new HttpClient();
        private static int lastUpdate;
        private static string user = null;
        private static string nextMessage;

        public static void ReadUser()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/link.dat"))
                return;
            user = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/link.dat");
        }

        public static async Task Update()
        {
            JObject json = await ApiGetRequest("getUpdates?offset=" + (lastUpdate + 1));

            if (nextMessage != null)
            {
                if (user != null && nextMessage != null && nextMessage != "" && user != "")
                {
                    await SendMessage(nextMessage, user);
                    nextMessage = null;
                }
            }

            foreach (JToken update in json["result"])
            {
                if (update["message"] == null || update["message"]["text"] == null)
                {
                    if (update["update_id"] != null)
                        lastUpdate = Convert.ToInt32(update["update_id"].ToString());
                    continue;
                }
                string chat_id = update["message"]["chat"]["id"].ToString();
                string text = update["message"]["text"].ToString();
                string answer;
                if ((answer = HandleInput(text, chat_id)) != "")
                    await SendMessage(answer, chat_id);
                lastUpdate = Convert.ToInt32(update["update_id"].ToString());
            }
        }

        private static string HandleInput(string input, string chatId)
        {
            if (input.ToLower().StartsWith("/link"))
            {
                string[] linkId = Regex.Split(input, " ");
                if (linkId.Length != 2)
                    return "/link 'linkId' - to link your ingame session with your account";
                if (!linkId[1].ToUpper().Equals(SWBot.GetHwid()))
                    return "";
                user = chatId;
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/link.dat", chatId);
                return "Successfully linked.";
            }
            
            switch (input.ToLower())
            {
                case "/help":
                    return "/link 'linkId' - to link your ingame session with your account";
                case "/start":
                    return "/help - for help";

            }
            return "";
        }

        public static async Task<JObject> ApiGetRequest(string func)
        {
            string responseJsonRaw = "";
            responseJsonRaw = await http.GetStringAsync(apiUri + func);
            return JObject.Parse(responseJsonRaw);
        }

        public static async Task SendMessage(string text, string recipient)
        {
            await ApiGetRequest(string.Format("sendMessage?chat_id={0}&text={1}&parse_mode=Markdown", recipient, text));
        }

        public static void SendMessage(string text)
        {
            nextMessage = text;
        }

    }
}

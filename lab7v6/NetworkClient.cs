using System;
using System.Net.Http;

namespace Lab7
{
    public class NetworkClient
    {
        private int downloadAttempts = 0;

        // Метод імітує HttpRequestException перші 3 рази
        public string DownloadData(string url)
        {
            downloadAttempts++;
            if (downloadAttempts <= 3)
            {
                throw new HttpRequestException($"Помилка мережі при завантаженні: {url}");
            }
            return $"Дані з мережі: {url}";
        }
    }
}

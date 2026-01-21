using System;
using System.Net.Http;

namespace Lab7
{
    class NetworkClient
    {
        private int _attempts = 0;

        public string DownloadData(string url)
        {
            _attempts++;
            if (_attempts <= 3)
            {
                throw new HttpRequestException($"Помилка мережі при завантаженні: {url}");
            }
            return $"Дані успішно завантажено з {url}";
        }
    }
}

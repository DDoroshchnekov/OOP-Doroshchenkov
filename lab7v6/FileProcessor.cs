using System;
using System.IO;

namespace Lab7
{
    class FileProcessor
    {
        private int _attempts = 0;

        public string ReadFile(string path)
        {
            _attempts++;
            if (_attempts <= 2)
            {
                throw new FileNotFoundException($"Файл не знайдено: {path}");
            }
            return $"Дані з файлу: {path}";
        }
    }
}

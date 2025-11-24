using System;
using System.Collections.Generic;
using System.IO;

namespace Lab7
{
    public class FileProcessor
    {
        private int readAttempts = 0;

        // Метод імітує читання файлу з FileNotFoundException перші 2 рази
        public string ReadFile(string path)
        {
            readAttempts++;
            if (readAttempts <= 2)
            {
                throw new FileNotFoundException($"Файл не знайдено: {path}");
            }
            return $"Дані з файлу: {path}";
        }
    }
}

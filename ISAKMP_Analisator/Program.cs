using System;
using Microsoft.Extensions.Configuration;

namespace program

{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание конфигурации | Не знаю насколько это правильно, но нашел только это решение
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Установка базового пути
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Добавление файла конфигурации
                .Build();

            // Считываем настройки
            string inputDirectory = configuration["inputDirectory"];
            Console.WriteLine($"Input Directory: {inputDirectory}");
            var outputDirectory = configuration["outputDirectory"];
            Console.WriteLine($"Output Directory: {outputDirectory}");



        }
    }
}
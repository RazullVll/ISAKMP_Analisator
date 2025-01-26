// НЕОБХОДИМО УСТАНОВИТЬ WinPcap_4_1_3.exe из 3rdParty
// pcap на вход в папке PcapExamples

//Название проекта принято формировать от названия решения (Solution - *.sln) и
//в целом придерживаться нотации CamelCase в названиях.
//Поэтому при создании правильней было бы назвать решение IsakmpAnalisator
//а проект - IsakmpAnalisator.Processing
//тогда namespace был бы IsakmpAnalisator
namespace ISAKMP_Analisator
{
    using ISAKMP_Analisator.Handling;
    using ISAKMP_Analisator.Settings;

    public class Program
    {
        private static readonly SettingsProvider SettingsProvider = new();

        private static readonly HandlingProvider HandlingProvider = new();

        public static void Main(string[] args)
        {
            // Создание конфигурации | Не знаю насколько это правильно, но нашел только это решение
            // Всё верно, однако лучше выносить инициализацию в отдельные методы и классы
            // Например:
            if (!SettingsProvider.Init())
            {
                Console.ReadKey();

                return;
            }

            //Вообще в методе Main консольного приложения должно быть минимум кода,
            //а основная логика раскидана по другим методам и классам. Рекомендую почитать это:
            //https://habr.com/ru/articles/764898/
            //https://habr.com/ru/companies/ruvds/articles/426413/
            //И, собственно работа с SarpPcap
            //https://habr.com/ru/sandbox/30450/

            // Считываем настройки 
            SettingsProvider.ShowSettingsInConsole();

            while (true)
            {
                ShowActionsList();

                switch (Console.ReadLine())
                {
                    case "1":
                    {
                        // Пример считывания входной директории
                        HandlingProvider.ReadPcapInput(SettingsProvider.GetInputPath());

                        break;
                    }
                    case "2":
                    {
                        return;
                    }
                }
            }
        }

        private static void ShowActionsList()
        {
            Console.WriteLine("\r\nВыберите цифру действия:\r\n\t" +
                              "1 - Обработать pcap-файлы\r\n\t" +
                              "2 - Выход\r\n" +
                              "и нажмите Enter\r\n");
        }
    }
}
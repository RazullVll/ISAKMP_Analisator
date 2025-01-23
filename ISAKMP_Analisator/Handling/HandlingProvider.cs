namespace ISAKMP_Analisator.Handling
{
    public class HandlingProvider
    {
        private PcapReader pcapReader;

        public HandlingProvider()
        {
            pcapReader = new PcapReader();
        }

        public void ReadPcapInput(string? inputDirectory)
        {
            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("ОШИБКА: не удалось получить доступ к входной директории!");

                return;
            }

            // Пример: извлекаем пакеты из каждого pcap на входе
            foreach (var file in Directory.EnumerateFiles(inputDirectory, "*", SearchOption.AllDirectories))
            {
                var packets = pcapReader.ReadPcap(file);

                Console.WriteLine($"Из {file} извлечено {packets.Count()} ethernet пакетов");
            }
        }
    }
}

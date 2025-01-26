namespace ISAKMP_Analisator.Handling
{
    using PacketDotNet;
    using SharpPcap;
    using SharpPcap.LibPcap;

    // Для примера
    public class PcapReader
    {
        private List<EthernetPacket> packets = new();

        private static int packetIndex = 0;

        public IEnumerable<EthernetPacket> ReadPcap(string pcapFilePath)
        {
            ICaptureDevice device;

            try
            {
                // Get an offline device
                device = new CaptureFileReaderDevice(pcapFilePath);

                // Open the device
                device.Open();

                // Подписываемся на событие о новом найденном пакете и по нему выполняем метод device_OnPacketArrival
                device.OnPacketArrival +=
                    new PacketArrivalEventHandler(device_OnPacketArrival);

                // Запускает чтение дампа pcap. Здесь начинают срабатывать события OnPacketArrival
                device.Capture();

                device.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА: {e} при открытии файла {pcapFilePath}");
            }

            return this.packets;
        }

        private void device_OnPacketArrival(object sender, PacketCapture e)
        {
            var rawPacket = e.GetPacket();

            var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var ethernetPacket = packet.Extract<EthernetPacket>();

            if (ethernetPacket != null)
            {
                this.packets.Add(ethernetPacket);

                //Пример инфы, которую можно дёргать из пакетов
                //Console.WriteLine("{0} At: {1}:{2}: MAC:{3} -> MAC:{4}",
                //    packetIndex,
                //    e.Header.Timeval.Date.ToString(),
                //    e.Header.Timeval.Date.Millisecond,
                //    ethernetPacket.SourceHardwareAddress,
                //    ethernetPacket.DestinationHardwareAddress);

                packetIndex++;
            }
        }
    }
}

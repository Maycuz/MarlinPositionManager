using System.IO.Ports;
using System.Numerics;

namespace PrinterPositionManager
{
    internal class MarlinPrinter
    {
        private bool _isConnected = false;
        private readonly SerialPort _port;
        private StoredPosition? _currentPos;

        public static List<int> AvailableBaudrates = new List<int>
        {
            1200,
            2400,
            4800,
            9600,
            14400,
            19200,
            38400,
            57600,
            115200,
            128000,
            192000
        };

        public MarlinPrinter(string serialPort, int baudrate)
        {
            _port = new SerialPort(serialPort, baudrate);
        }

        public StoredPosition? CurrentPos
        {
            get { return _currentPos; }
        }

        public bool Connect()
        {
            try
            {
                _port.Open();
                _isConnected = true;

                _port.DataReceived += DataReceived;
            }
            catch(UnauthorizedAccessException e)
            {
                // TODO: Better exception handling & recovery
                Console.WriteLine("Could not connect: {}", e.Message);
            }

            return IsConnected;
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = _port.ReadExisting();
            
            var parsedData = GcodeParser.SerialDataToPosition(data);

            if (parsedData != null)
                _currentPos = new StoredPosition((Vector3)parsedData);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _port.Close();
            }

            _isConnected = false;
            _port.Dispose();
        }

        public bool IsConnected
        {
            get { return _port.IsOpen && _isConnected; }
        }

        public void RequestPosition()
        {
            if (IsConnected)
            {
                _port.WriteLine(GcodeParser.PositionRequest());
            }
        }

        public void SetPosition(StoredPosition pos)
        {
            if (IsConnected)
            {
                _port.WriteLine(GcodeParser.PositionToSerialData(pos.Position));
            }
        }
    }
}

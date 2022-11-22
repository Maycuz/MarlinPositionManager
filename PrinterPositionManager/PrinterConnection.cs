using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PrinterPositionManager
{
    internal class PrinterConnection
    {
        private bool _isConnected = false;
        private SerialPort _port;
        private GcodeParser _parser;
        private StoredPosition? _currentPos;

        public struct NewPositionReceivedEventArgs
        {
            public NewPositionReceivedEventArgs(StoredPosition storedPos) { storedPosition = storedPos; }
            public StoredPosition storedPosition { get; }
        }

        public delegate void OnNewPositionReceivedEventHandler(object sender, NewPositionReceivedEventArgs e);

        public static List<string> AvailableBaudrates = new List<string>
        {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000",
            "192000"
        };

        public PrinterConnection(string serialPort, int baudrate)
        {
            _port = new SerialPort(serialPort, baudrate);
            _parser = new GcodeParser();
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
            
            // TODO: for debug purposes
            // Console.Write(data);

            var parsedData = _parser.ParseData(data);

            if (parsedData != null)
                _currentPos = parsedData;
                // newPositionEvent?.Invoke(this, new NewPositionReceivedEventArgs(parsedData));
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
                _port.WriteLine("M114");
            }
        }

        public void SetPosition(StoredPosition pos)
        {
            if (IsConnected)
            {
                var command = String.Format("G1 X{0} Y{1} Z{2}", pos.Position.X, pos.Position.Y, pos.Position.Z);
                _port.WriteLine(command);
            }
        }
    }
}

using System;
using System.Globalization;
using System.IO.Ports;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Serialization;
using System.Xml;

namespace PrinterPositionManager
{
    public partial class Form1 : Form
    {
        private StoredPosition? currentPosition;
        private string storedPositionsFile = Directory.GetCurrentDirectory() + "\\stored_positions.xml";

        private enum UIState
        {
            CONNECTED,
            DISCONNECTED,
            ERROR
        }

        private PrinterConnection? _printerConnection;

        public Form1()
        {
            InitializeComponent();

            // Attempt...
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            var availablePorts = SerialPort.GetPortNames();

            cbxComPort.Items.AddRange(availablePorts);
            cbxBaudrate.Items.AddRange(PrinterConnection.AvailableBaudrates.ToArray());
            
            if(availablePorts.Length > 0)
                cbxComPort.SelectedIndex = 0;

            cbxBaudrate.SelectedIndex = 
                PrinterConnection.AvailableBaudrates.FindIndex(a => a == ("115200"));

            if (File.Exists(storedPositionsFile))
                LoadFromXML(storedPositionsFile);

            SetUIState(UIState.DISCONNECTED);
        }

        private void btPrinterConn_Click(object sender, EventArgs e)
        {
            // TODO: Must be a safer way to do this...
            _printerConnection = new PrinterConnection((string)cbxComPort.SelectedItem,
                int.Parse((string)cbxBaudrate.SelectedItem));
            _printerConnection.Connect();

            // Should be connected, so consider it an error
            SetPrinterConnectionUI(_printerConnection.IsConnected, true);           
        }
        private void SetPrinterConnectionUI(bool connected, bool isError)
        {
            if (connected)
            {
                tbPrinterStatus.Text = "Connected";
                SetUIState(UIState.CONNECTED);
                posRequest.Start();
            }
            else if (!isError)
            {
                tbPrinterStatus.Text = "Disconnected";
                SetUIState(UIState.DISCONNECTED);
                posRequest.Stop();
            }
            else
            {
                tbPrinterStatus.Text = "Error";
                SetUIState(UIState.ERROR);
                posRequest.Stop();
            }
        }

        private void SetModifyGroup(bool enabled)
        {
            btRemove.Enabled = enabled;
            gbEditPos.Enabled = enabled;
        }

        private void SetUIState(UIState state)
        {
            switch(state)
            {
                case UIState.CONNECTED:
                    btPrinterConn.Enabled = false;
                    btPrinterDisconn.Enabled = true;
                    btStore.Enabled = true;
                    btRecallPos.Enabled = true;
                    tbPrinterStatus.ForeColor = Color.DarkGreen;
                    tbPrinterStatus.BackColor = Color.MediumSeaGreen;
                    tbCurrentPosVal.Enabled = true;
                    comCheck.Stop();
                    lbPrinterState.Focus();
                    break;
                case UIState.ERROR:
                case UIState.DISCONNECTED:
                    btPrinterConn.Enabled = true;
                    btPrinterDisconn.Enabled = false;
                    btStore.Enabled = false;
                    btRecallPos.Enabled = false;
                    tbPrinterStatus.ForeColor = Color.Maroon;
                    tbPrinterStatus.BackColor = Color.IndianRed;
                    tbCurrentPosVal.Enabled = false;
                    comCheck.Start();
                    lbPrinterState.Focus();
                    break;
            }
        }

        private void btPrinterDisconn_Click(object sender, EventArgs e)
        {
            if (_printerConnection == null)
            {
                MessageBox.Show("Error", "Check printer connection");
                return;
            }

            _printerConnection.Disconnect();

            // Should be disconnected, so do not consider it an error
            SetPrinterConnectionUI(_printerConnection.IsConnected, false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void posRequest_Tick(object sender, EventArgs e)
        {
            if (_printerConnection == null)
            {
                MessageBox.Show("Error", "Check printer connection");
                posRequest.Stop();
                return;
            }

            _printerConnection.RequestPosition();

            var currentPos = _printerConnection.CurrentPos;

            if (currentPos != null)
            {
                tbCurrentPosVal.Text = currentPos.ToString();
                currentPosition = currentPos;
            }
        }

        private void btStore_Click(object sender, EventArgs e)
        {
            if (currentPosition != null)
            {
                var currentPos = new StoredPosition(currentPosition.Position);
                lbxStored.Items.Add(currentPos);
                SaveStoredToXml(storedPositionsFile);
            }
        }

        private void lbxStored_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxStored.SelectedIndex == -1)
            {
                SetModifyGroup(false);
                return;
            }           

            var storedPos = (StoredPosition)lbxStored.SelectedItem;

            if (storedPos == null)
                return;

            tbModPosX.Text = String.Format("{0:0.00}", storedPos.Position.X.ToString());
            tbModPosY.Text = String.Format("{0:0.00}", storedPos.Position.Y.ToString());
            tbModPosZ.Text = String.Format("{0:0.00}", storedPos.Position.Z.ToString());
            tbModPosAlias.Text = storedPos.ToString();

            SetModifyGroup(true);
        }

        private void btPosModStore_Click(object sender, EventArgs e)
        {
            var storedPos = (StoredPosition)lbxStored.Items[lbxStored.SelectedIndex];
            var newPos = new Vector3(float.Parse(tbModPosX.Text), float.Parse(tbModPosY.Text), float.Parse(tbModPosZ.Text));

            if (tbModPosAlias.Text != storedPos.ToString())
                storedPos.Alias = tbModPosAlias.Text;

            storedPos.Position = newPos;

            lbxStored.Items[lbxStored.SelectedIndex] = storedPos;
            SaveStoredToXml(storedPositionsFile);
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            lbxStored.Items.RemoveAt(lbxStored.SelectedIndex);
            SaveStoredToXml(storedPositionsFile);
        }

        private void btRecallPos_Click(object sender, EventArgs e)
        {
            if (_printerConnection == null)
            {
                posRequest.Stop();
                MessageBox.Show("Error", "Check printer connection");
                return;
            }

            posRequest.Stop();

            var storedPos = (StoredPosition)lbxStored.SelectedItem;
            _printerConnection.SetPosition(storedPos);

            posRequest.Start();
        }

        private void EnsureInputIsNumeric(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, decimal and -
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 45))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal or - is allowed
            if (e.KeyChar == 46 || e.KeyChar == 45)
            {
                if ((sender as System.Windows.Forms.TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void comCheck_Tick(object sender, EventArgs e)
        {
            var availablePorts = SerialPort.GetPortNames();

            foreach (var availablePort in availablePorts)
            {
                if (!cbxComPort.Items.Contains(availablePort))
                    cbxComPort.Items.Add(availablePort);
            }
        }

        public void SaveStoredToXml(string path)
        {
            var serializer = new XmlSerializer(typeof(List<StoredPosition>));

            using (FileStream stream = File.OpenWrite(path))
            {
                stream.SetLength(0);
                stream.Flush();
                serializer.Serialize(stream, lbxStored.Items.Cast<StoredPosition>().ToList());
            }
        }

        public void LoadFromXML(string path)
        { 
            if (File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<StoredPosition>));
                    
                using (FileStream stream = File.OpenRead(path))
                {
                    if (stream.Length == 0)
                        return;

                    try
                    {
                        var retrievedList = (List<StoredPosition>)serializer.Deserialize(stream);

                        if (retrievedList != null)
                            lbxStored.Items.AddRange(retrievedList.ToArray());
                    }
                    catch(System.InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message, "Error loading saved state");
                    }
                }
            }
        }
    }
}
using System;
using System.Globalization;
using System.IO.Ports;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Serialization;
using System.Xml;

namespace MarlinPositionManager
{
    public partial class Form1 : Form
    {
        private enum UIState
        {
            CONNECTED,
            DISCONNECTED,
            ERROR
        }

        private Dictionary<UIState, string> UIStateString = new Dictionary<UIState, string>()
        {
            { UIState.CONNECTED, "Connected" },
            { UIState.DISCONNECTED, "Disconnected" },
            { UIState.ERROR, "Error" },
        };

        private StoredPosition? _currentPosition;
        private MarlinPrinter? _printerConnection;
        private readonly string _storedPositionsFile = Directory.GetCurrentDirectory() + "\\stored_positions.xml";

        public Form1()
        {
            InitializeComponent();

            // Ensure decimal notations are compliant with Marlin (dot rather than comma).
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            var availablePorts = SerialPort.GetPortNames();

            cbxComPort.Items.AddRange(availablePorts);
            
            if(availablePorts.Length > 0)
                cbxComPort.SelectedIndex = 0;

            cbxBaudrate.Items.AddRange(MarlinPrinter.AvailableBaudrates.Cast<object>().ToArray());

            // Default: 115200bps
            cbxBaudrate.SelectedIndex = 
                MarlinPrinter.AvailableBaudrates.FindIndex(a => a == 115200);

            if (File.Exists(_storedPositionsFile))
                lbxStored.Items.AddRange(PositionSerializer.LoadStoredPositionsFromXML(_storedPositionsFile));

            SetUIState(UIState.DISCONNECTED);
        }

        private void SetPrinterConnectionUI(bool connected, bool isDisconnectUnexpected)
        {
            if (connected)
            {
                SetUIState(UIState.CONNECTED);
            }
            else if (!isDisconnectUnexpected)
            {
                SetUIState(UIState.DISCONNECTED);
            }
            else
            {
                SetUIState(UIState.ERROR);
            }
        }

        private void SetModifyGroupEnabled(bool enabled)
        {
            btRemove.Enabled = enabled;
            gbEditPos.Enabled = enabled;
        }

        private void SetUIState(UIState state)
        {
            tbPrinterStatus.Text = UIStateString[state];

            switch (state)
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
                    posRequest.Start();
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
                    posRequest.Stop();
                    break;
            }

            lbPrinterState.Focus();
        }

        private void EnsureInputIsNumeric(object sender, KeyPressEventArgs e)
        {
            // Based on: https://itecnote.com/tecnote/c-validation-textboxes-allowing-only-decimals/

            // allows 0-9, backspace, decimal and -
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 45))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal or - is allowed
            if (e.KeyChar == 46 || e.KeyChar == 45)
            {
                if (((System.Windows.Forms.TextBox)sender).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
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

        private void lbxStored_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxStored.SelectedIndex < 0)
            {
                SetModifyGroupEnabled(false);
                return;
            }           

            var storedPos = (StoredPosition)lbxStored.SelectedItem;

            if (storedPos == null)
                return;

            tbModPosX.Text = String.Format("{0:0.00}", storedPos.Position.X.ToString());
            tbModPosY.Text = String.Format("{0:0.00}", storedPos.Position.Y.ToString());
            tbModPosZ.Text = String.Format("{0:0.00}", storedPos.Position.Z.ToString());
            tbModPosAlias.Text = storedPos.ToString();

            SetModifyGroupEnabled(true);
        }

        private void btStore_Click(object sender, EventArgs e)
        {
            // Save current pos & write to file
            if (_currentPosition != null)
            {
                var currentPos = new StoredPosition(_currentPosition.Position);
                lbxStored.Items.Add(currentPos);
                PositionSerializer.SaveStoredPositionsToXml(_storedPositionsFile,
                    lbxStored.Items.Cast<StoredPosition>().ToList());
            }
        }

        private void btPosModStore_Click(object sender, EventArgs e)
        {
            // Modify entry & write to file
            var storedPos = (StoredPosition)lbxStored.Items[lbxStored.SelectedIndex];
            var newPos = new Vector3(float.Parse(tbModPosX.Text), float.Parse(tbModPosY.Text), float.Parse(tbModPosZ.Text));

            if (tbModPosAlias.Text != storedPos.ToString())
                storedPos.Alias = tbModPosAlias.Text;

            storedPos.Position = newPos;

            lbxStored.Items[lbxStored.SelectedIndex] = storedPos;
            PositionSerializer.SaveStoredPositionsToXml(_storedPositionsFile,
                lbxStored.Items.Cast<StoredPosition>().ToList());
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            // Remove from list & write to file
            lbxStored.Items.RemoveAt(lbxStored.SelectedIndex);
            PositionSerializer.SaveStoredPositionsToXml(_storedPositionsFile,
                lbxStored.Items.Cast<StoredPosition>().ToList());
        }

        private void btRecallPos_Click(object sender, EventArgs e)
        {
            if (_printerConnection == null)
            {
                MessageBox.Show("Error", "Check printer connection");
                SetPrinterConnectionUI(false, false);
                return;
            }

            // Stop request timer when sending move command to printer
            posRequest.Stop();

            var storedPos = (StoredPosition)lbxStored.SelectedItem;
            _printerConnection.SetPosition(storedPos);

            posRequest.Start();
        }

        private void btPrinterConn_Click(object sender, EventArgs e)
        {
            _printerConnection = new MarlinPrinter((string)cbxComPort.SelectedItem,
                (int)cbxBaudrate.SelectedItem);

            if (!_printerConnection.Connect())
            {
                MessageBox.Show("Error connecting to printer", "Connection error");
            }

            // Should be connected, so consider it an error
            SetPrinterConnectionUI(_printerConnection.IsConnected, true);
        }

        private void comCheck_Tick(object sender, EventArgs e)
        {
            // Tick: Check available ports, add to cbxComPort box

            var availablePorts = SerialPort.GetPortNames();

            foreach (var availablePort in availablePorts)
            {
                if (!cbxComPort.Items.Contains(availablePort))
                    cbxComPort.Items.Add(availablePort);
            }
        }

        private void posRequest_Tick(object sender, EventArgs e)
        {
            // Tick: Request position from printer, show on UI

            if (_printerConnection == null)
            {
                MessageBox.Show("Error", "Check printer connection");
                SetPrinterConnectionUI(false, false);
                return;
            }

            _printerConnection.RequestPosition();

            var currentPos = _printerConnection.CurrentPos;

            if (currentPos != null)
            {
                tbCurrentPosVal.Text = currentPos.ToString();
                _currentPosition = currentPos;
            }
        }
    }
}
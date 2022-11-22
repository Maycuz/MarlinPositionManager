using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterPositionManager
{
    internal class GcodeParser
    {
        public StoredPosition? ParseData(string serialData)
        {
            // TODO: Verify received message is OK
            StoredPosition? storedPos = null;

            try
            {
                // TODO: Better/safer way
                var splitData = serialData.Split(' ');
                var x = splitData[0].Substring(2);
                var y = splitData[1].Substring(2);
                var z = splitData[2].Substring(2);

                storedPos = new StoredPosition(new System.Numerics.Vector3(float.Parse(x), 
                    float.Parse(y), float.Parse(z)));
            }
            catch (System.FormatException)
            {
                // Ignore
            }
            catch (System.ArgumentOutOfRangeException)
            {
                // Ignore
            }
            catch (System.IndexOutOfRangeException)
            {
                // Ignore
            }

            return storedPos;
        }
    }
}

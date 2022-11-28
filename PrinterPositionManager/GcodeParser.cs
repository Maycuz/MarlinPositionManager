using System.Numerics;

namespace PrinterPositionManager
{
    internal static class GcodeParser
    {
        public static Vector3? SerialDataToPosition(string serialData)
        {
            Vector3? pos = null;

            try
            {
                var splitData = serialData.Split(' ');
                var x = splitData[0].Substring(2);
                var y = splitData[1].Substring(2);
                var z = splitData[2].Substring(2);

                pos = new Vector3(float.Parse(x), float.Parse(y), float.Parse(z));
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

            return pos;
        }

        public static string PositionToSerialData(Vector3 position)
        {
            var command = String.Format("G1 X{0} Y{1} Z{2}", position.X, position.Y, position.Z);
            return command;
        }

        public static string PositionRequest()
        {
            return "M114";
        }
    }
}

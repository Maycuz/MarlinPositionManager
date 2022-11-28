using System.Xml.Serialization;

namespace MarlinPositionManager
{
    internal static class PositionSerializer
    {
        public static void SaveStoredPositionsToXml(string path, List<StoredPosition> storedPositions)
        {
            var serializer = new XmlSerializer(typeof(List<StoredPosition>));

            using (FileStream stream = File.OpenWrite(path))
            {
                stream.SetLength(0);
                stream.Flush();
                serializer.Serialize(stream, storedPositions);
            }
        }

        public static StoredPosition[]? LoadStoredPositionsFromXML(string path)
        {
            if (File.Exists(path))
            {
                var serializer = new XmlSerializer(typeof(List<StoredPosition>));

                using (FileStream stream = File.OpenRead(path))
                {
                    if (stream.Length == 0)
                        return null;

                    try
                    {
                        var retrievedList = (List<StoredPosition>)serializer.Deserialize(stream);

                        if (retrievedList != null)
                            return retrievedList.ToArray();
                    }
                    catch (System.InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message, "Error loading saved state");
                    }
                }
            }

            return null;
        }
    }
}

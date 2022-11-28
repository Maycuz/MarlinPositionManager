using System.Numerics;

namespace MarlinPositionManager
{
    [Serializable]
    public class StoredPosition
    {
        // Required for XML serialization
        public StoredPosition() { }

        public StoredPosition(Vector3 position, string? alias = null)
        {
            Position = position;

            if (!string.IsNullOrEmpty(alias))
                Alias = alias;
        }

        public Vector3 Position { set; get; }

        public string? Alias { set; get; }

        public override string ToString()
        {
            if (Alias != null && !string.IsNullOrEmpty(Alias))
                return Alias;

            return string.Format("X: {0:0.00}, Y: {1:0.00}, Z: {2:0.00}", Position.X, Position.Y, Position.Z);
        }
    }
}

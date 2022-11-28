using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarlinPositionManager
{
    [Serializable]
    public class StoredPosition
    {
        private Vector3 _position;
        private string? _alias;

        // Required for XML serialization
        public StoredPosition() { }

        public StoredPosition(Vector3 position, string? alias = null)
        {
            _position = position;

            if (!string.IsNullOrEmpty(alias))
                _alias = alias;
        }

        public Vector3 Position
        {
            set { _position = value; }
            get { return _position; }
        }

        public string? Alias
        {
            set { _alias = value; }
            get { return _alias; }
        }

        public override string ToString()
        {
            if (_alias != null && !string.IsNullOrEmpty(_alias))
                return _alias;

            return string.Format("X: {0:0.00}, Y: {1:0.00}, Z: {2:0.00}", _position.X, _position.Y, _position.Z);
        }
    }
}

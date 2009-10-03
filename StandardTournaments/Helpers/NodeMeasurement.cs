using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournaments.Standard
{
    public class NodeMeasurement
    {
        public NodeMeasurement(float width, float height, float centerLine)
        {
            this.Width = width;
            this.Height = height;
            this.CenterLine = centerLine;
        }

        public float Width
        {
            get;
            private set;
        }

        public float Height
        {
            get;
            private set;
        }

        public float CenterLine
        {
            get;
            private set;
        }
    }
}

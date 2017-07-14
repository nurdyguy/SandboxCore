using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathService.Models.EulerModels
{

    public class Problem566
    {
        public int[] _sliceSizes;
        public int[] _prods;
        public int _denom;
        public int _edgeCounter;

        public Problem566(int x, int y , int z)
        {
            Slices = new List<Slice>(50000);            
            _sliceSizes = new int[3] { x, y, z };
            _prods = new int[3] { 360 * y * z, 360 * x * z, 360 * x * y };
            Edge._sliceSizes = new int[3] { x, y, z };            
            Edge._denom = _denom = x * y * z;
            
            LastEdge = new Edge(0, 0);
            Slices.Add(new Slice(LastEdge, new Edge(356400, 0), true));

            _edgeCounter = 1;
        }
        
        public List<Slice> Slices { get; set; }
        public int[] sliceSizes { get { return _sliceSizes; } }

        public Edge LastEdge { get; set; }
        public Edge NextEdge { get; set; }

        public void MakeNextCut()
        {
            int cutNum = (_edgeCounter - 1) % 3;
            var size = _sliceSizes[cutNum];
            switch(cutNum)
            {
                case 0:
                    NextEdge = new Edge(LastEdge.rationalCoef + _prods[0], LastEdge.irrationalCoef);
                    break;
                case 1:
                    NextEdge = new Edge(LastEdge.rationalCoef + _prods[1], LastEdge.irrationalCoef);
                    break;
                case 2:
                    NextEdge = new Edge(LastEdge.rationalCoef, LastEdge.irrationalCoef + _prods[2]);
                    break;
            }

            // find current slice based on lastEdge
            Slice currSlice;
            int currSliceId = 0;
            bool found = false;
            for(var i = 0; !found && i < Slices.Count; i++)
            {
                if (LastEdge.Angle == Slices[i].StartEdge.Angle)// && LastEdge.Angle < Slices[i].EndEdge.Angle)
                {
                    currSlice = Slices[i];
                    currSliceId = i;
                    found = true;
                }
            }
            
            // new cut lies completely inside current slice
            if(NextEdge.Angle <= Slices[currSliceId].EndEdge.Angle)
            {
                var newSlice = new Slice(NextEdge, Slices[currSliceId].EndEdge, Slices[currSliceId].IsFrostingUp);
                Slices[currSliceId].EndEdge = new Edge(NextEdge);
                Slices[currSliceId].IsFrostingUp = !Slices[currSliceId].IsFrostingUp;
                Slices.Insert(currSliceId + 1, newSlice);
            }
            //else if(// new cut goes past end of current slice
            

            

            LastEdge = NextEdge;
            _edgeCounter++;
        }


        public class Slice
        {
            public Slice(Edge start, Edge end, bool up)
            {
                StartEdge = new Edge(start.rationalCoef, start.irrationalCoef);
                EndEdge = new Edge(end.rationalCoef, end.irrationalCoef);
                IsFrostingUp = up;
            }
            public Edge StartEdge { get; set; }
            public Edge EndEdge { get; set; }
            public bool IsFrostingUp { get; set; }
        }

        public class Edge
        {
            public static int _denom;
            public static int[] _sliceSizes;
            public static double CalcEdgeAngle(int r, int i)
            {
                var angle = (r + i * System.Math.Sqrt(_sliceSizes[2])) / _denom;
                while (angle > 360)
                    angle -= 360;
                return angle;
            }

            public Edge(int r, int i)
            {
                rationalCoef = r;
                irrationalCoef = i;
                Angle = CalcEdgeAngle(r, i);
            }

            public Edge(Edge e)
            {
                rationalCoef = e.rationalCoef;
                irrationalCoef = e.irrationalCoef;
                Angle = CalcEdgeAngle(e.rationalCoef, e.irrationalCoef);
            }

            public int rationalCoef { get; set; }
            public int irrationalCoef { get; set; }
            public double Angle { get; set; }            
        }
    }

    

    

}

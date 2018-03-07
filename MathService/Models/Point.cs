using System;
using System.Collections.Generic;
using System.Text;

namespace MathService.Models
{
    public struct Point<T> where T : IComparable
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }
    }
}

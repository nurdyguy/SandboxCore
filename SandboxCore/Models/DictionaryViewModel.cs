using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Models
{
    public class DictionaryViewModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Dictionary<int, int> Dictionary {get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandboxCore.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ResultMessage ResultMessage { get; set; }
        public List<string> Strings { get; set; }
    }
}

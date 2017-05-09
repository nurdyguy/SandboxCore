using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SandboxCore.Services.Contracts
{
    public interface IEulerService
    {
        double RunProblem566(int x, int y, int z);

        BigInteger RunProblem590(int max);
    }
}

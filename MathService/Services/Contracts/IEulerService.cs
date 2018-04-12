using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace MathService.Services.Contracts
{
    public interface IEulerService
    {
        object RunProblem207(long max);
        object RunProblem401(int exp);
        object RunProblem461(int max);
        object RunProblem467(int num);
        BigInteger RunProblem482(int max);        
        object RunProblem483(int max);        
        object RunProblem500(int num);
        object RunProblem501(int num);
        object RunProblem504(int max);
        BigInteger RunProblem566(int x, int y, int z);
        object RunProblem569(int x);
        object RunProblem574(int max);
        BigInteger RunProblem590(int max);
    }
}

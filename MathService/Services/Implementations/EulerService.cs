using System;
using System.Collections.Generic;
using System.Text;
using MathService.Services.Contracts;
using MathService.Calculators;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService
    {
        private readonly ICalculator _calc;
        public EulerService(ICalculator calc)
        {
            _calc = calc;
            //Calculator.InitCalculator();  //------------------------------------------------
        }
    }
}

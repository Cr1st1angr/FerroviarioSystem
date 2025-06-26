using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Design_Patterns.Estrategy
{
    public class ClaculadoraPrecioAdulto : ICalculadoraPrecioStrategy
    {
        public double CalcularPrecio()
        {
            var IVA = 10 * 0.15;
            var precioIVA = 10 + IVA;

            return precioIVA;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Design_Patterns.Estrategy
{
    public class ClaculadoraPrecioKid: ICalculadoraPrecioStrategy
    {
        public double CalcularPrecio()
        {
            var IVA = 10 * 0.15;
            var precioIVA = 10 + IVA;
            var descuento = precioIVA * 0.5; // 50% de descuento
            var precioFinal = precioIVA - descuento;

            return precioFinal;
        }
    }
}

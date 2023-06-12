using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe GreenJewel é uma especialização da classe Jewel, esta classe representa joias verdes contidas no mapa.
    /// </summary>
    public class GreenJewel : Jewel
    {   /// <summary>
        /// Este método cria uma nova GreenJewel.
        /// </summary>
        public GreenJewel(int x, int y)
        {
            this.y = y;
            this.x = x;
            this.valor = 50;
        }
        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Uma GreenJewel é representada no mapa pelos caracteres JG.</returns>
        public override string ToString()
        {
            return "JG ";
        }
    }
}
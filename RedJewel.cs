using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe RedJewel é uma especialização da classe Jewel, esta classe representa joias vermelhas contidas no mapa.
    /// </summary>
    public class RedJewel : Jewel
    {
        /// <summary>
        /// Este método cria uma nova RedJewel.
        /// </summary>
        public RedJewel(int x, int y) 
        { 
            this.y = y;
            this.x = x;
            this.valor = 100;
        }
        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Uma RedJewel é representada no mapa pelos caracteres JR.</returns>
        public override string ToString()
        {
            return "JR ";
        }
    }
}
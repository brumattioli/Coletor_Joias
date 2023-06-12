using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe Water trata-se de uma especialização de Obstacle, esta classe representa objetos do tipo água no mapa.
    /// </summary>
    public class Water : Obstacle
    {
        public Water(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Uma Water é representada no mapa pelos caracteres ##.</returns>
        public override string ToString()
        {
            return "## ";
        }
    }
}
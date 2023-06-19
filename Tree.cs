using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe Tree trata-se de uma especialização de Obstacle, esta classe representa objetos do tipo árvore no mapa. O jogador pode coletar energia dos obstáculos que são do tipo Tree.
    /// </summary>
    public class Tree : Obstacle
    {
        public Tree(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Uma Tree é representada no mapa pelos caracteres $$.</returns>
        public override string ToString()
        {
            return "$$ ";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
        /// <summary>
        /// A classe Radioactive trata-se de uma especialização de Obstacle, esta classe representa objetos radioativos no mapa.
        /// </summary>
    public class Radioactive : Obstacle
    {
        /// <value>
        /// O jogador perde parte de sua energia quando passa em alguma das posições adjacentes a um objeto radioativo.
        /// </value>
        int energia { get; set; }
        public Radioactive(int x, int y)
        {
            this.y = y;
            this.x = x;
            energia = -10;
        }

        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Um Radioactive é representado no mapa pelos caracteres !!.</returns>
        public override string ToString()
        {
            return "!! ";
        }
    }
}
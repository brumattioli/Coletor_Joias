using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe joia é utilizada com a finalidade de representar objetos do tipo joia que serão coletadas no jogo.
    /// A classe joia é uma especialização da classe Cell.
    /// </summary>
    public class Jewel : Cell
    {
        /// <value>
        /// Cada joia possui um valor diferente, e quando a joia é coletada o valor é somado à sacola do jogador.
        /// </value>
        public int valor { get; set; }
    }
}

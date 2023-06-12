using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
        /// <summary>
        /// A classe Cell trata-se de uma superclasse utilizada como base para construção de objetos neste jogo,
        /// sendo assim, será herdada por todos os objetos inseridos no mapa do jogo: Jewel, Obstacle, Radioactive e Robot.
        /// </summary>
    public class Cell
    {
        /// <summary>
        /// O mapa do jogo trata-se de uma matriz com duas dimensões, a variável x representa as linhas desta matriz.
        /// </summary>
        public int x { get; set; }
        /// </summary>
        /// <summary>
        /// O mapa do jogo trata-se de uma matriz com duas dimensões, a variável y representa as colunas desta matriz.
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Ele imprime a string -- no mapa.</returns>
        public override string ToString()
        {
            return "-- ";
        }
    }
}
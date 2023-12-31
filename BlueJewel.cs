﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe BlueJewel é uma especialização da classe Jewel, esta classe representa joias azuis contidas no mapa. Uma joia do tipo BlueJewel fornece energia ao jogador quando coletada por ele.
    /// </summary>
    public class BlueJewel : Jewel
    {
        /// <summary>
        /// Este método cria uma nova BlueJewel.
        /// </summary>
        public BlueJewel(int x, int y)
        {
            this.y = y;
            this.x = x;
            this.valor = 10;
        }
        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Uma BlueJewel é representada no mapa pelos caracteres JB.</returns>
        public override string ToString()
        {
            return "JB ";
        }
    }
}
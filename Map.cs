using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe Map representa o tabuleiro do jogo, este é representado por uma matriz de duas dimensões, e representa o espaço no qual o robô se desloca para coletar joias e energia.
    /// Esta classe armazena todas as informações dos elementos do mapa.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// O tabuleiro representa uma matriz de duas dimensões. O tabuleiro armazena elementos do tipo Cell, como todos os elementos herdam da superClasse Cell, o mapa acomoda os elementos do tipo Jewel, Robot e Obstacle. 
        /// </summary>
        /// <value> O tabuleiro retorna uma matriz do tipo Cell. </value>
        public Cell[,] tabuleiro { get; set; }
        /// <summary>
        /// O mapa mantém uma contagem de quantas joias ainda não foram coletadas, quando o contador é igual a zero, se inicia a próxima fase.
        /// </summary>
        /// <value> Retorna a quantidade de joias que ainda não foram coletadas pelo robô.</value>
        public int qtdJewel { get; set; }

        /// <summary>
        /// O mapa mantém um controle de qual fase o jogo está, e este atributo é inicializado em 1.
        /// </summary>
        public int faseAtual = 1;

        /// <summary>
        /// Este método atribui objetos do tipo Cell a todas as posições do mapa.
        /// </summary>
        /// <returns> Este método retorna o mapa que da início ao jogo. A matriz que da início ao jogo tem tamanho [10,10].</returns>
        public Map inicioJogo(Map m)
        {
            m.tabuleiro = new Cell[10,10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Cell celula = new Cell();
                    m.tabuleiro[i, j] = celula;
                }
            }
            return m;
        }
        /// <summary>
        /// Este método imprime todas as posições do mapa, a partir dos métodos sobrescritos ToString de cada elemento que faz parte do mapa.
        /// </summary>
        /// <param name="m"></param>

        public void mostraMapa(Map m)
        {
            for (int i = 0; i < m.tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < m.tabuleiro.GetLength(0); j++)
                {
                    switch(m.tabuleiro[i, j].ToString().Trim())
                    {
                        case "JR":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case "JB":
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case "JG":
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                        case "##": //agua
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case "$$": //arvore
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case "!!": //radioativo
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                            ;            }
                    Console.Write(m.tabuleiro[i, j].ToString());
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Este método posiciona todos os elementos da fase 1 no mapa, as joias, obstáculos e o robô.
        /// </summary>
        /// <param name="m"></param>
        public void tabuleiroFase1(Map m)
        {
            RedJewel joiaVermelha = new RedJewel(1, 9);
            m.tabuleiro[joiaVermelha.x, joiaVermelha.y] = joiaVermelha;
            m.qtdJewel++;
            joiaVermelha = new RedJewel(8, 8);
            m.tabuleiro[joiaVermelha.x, joiaVermelha.y] = joiaVermelha;
            m.qtdJewel++;

            GreenJewel joiaVerde = new GreenJewel(9, 1);
            m.tabuleiro[joiaVerde.x, joiaVerde.y] = joiaVerde;
            m.qtdJewel++;
            joiaVerde = new GreenJewel(7, 6);
            m.tabuleiro[joiaVerde.x, joiaVerde.y] = joiaVerde;
            m.qtdJewel++;

            BlueJewel joiaAzul = new BlueJewel(3, 4);
            m.tabuleiro[joiaAzul.x, joiaAzul.y] = joiaAzul;
            m.qtdJewel++;
            joiaAzul = new BlueJewel(2, 1);
            m.tabuleiro[joiaAzul.x, joiaAzul.y] = joiaAzul;
            m.qtdJewel++;

            Tree arvore = new Tree(5, 9);
            m.tabuleiro[arvore.x, arvore.y] = arvore;
            arvore = new Tree(3, 9);
            m.tabuleiro[arvore.x, arvore.y] = arvore;
            arvore = new Tree(8, 3);
            m.tabuleiro[arvore.x, arvore.y] = arvore;
            arvore = new Tree(2, 5);
            m.tabuleiro[arvore.x, arvore.y] = arvore;
            arvore = new Tree(1, 4);
            m.tabuleiro[arvore.x, arvore.y] = arvore;

            Water agua = new Water(5, 0);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 1);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 2);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 3);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 4);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 5);
            m.tabuleiro[agua.x, agua.y] = agua;
            agua = new Water(5, 6);
            m.tabuleiro[agua.x, agua.y] = agua;

            Robot robo = new Robot(0, 0);
            m.tabuleiro[robo.x, robo.y] = robo;
        }
        /// <summary>
        /// Este método posiciona todos os elementos das fases 2 em diante no mapa, as joias, obstáculos e o robô. 
        /// A partir da segunda fase, os elementos são posicionados de maneira aleatória no mapa, portanto foi 
        /// analisada a proporção de cada elemento na fase inicial e esta mesma proporção de quantidade foi replicada 
        /// para os elementos de cada tipo nas fases seguintes.
        /// Este método aumenta as dimensões do mapa em 1 unidade a cada nova fase do jogo até o limite de 30x30 de tamanho.
        /// Este método também imprime na tela uma mensagem quando o jogador passa de fase.
        /// </summary>
        /// <param name="m"></param>
        
        public void tabuleiroProximaFase(Map m)
        {
            Console.WriteLine();
            Console.WriteLine("Parabéns! Você passou para a fase " + ++faseAtual + "!");  ///Incrementa o contador da fase em 1 unidade e imprime na tela uma mensagem quando o jogador passa de fase.

            if (m.faseAtual == 2)
            {
                instrucoesFase2();
            }

            m.tabuleiro = new Cell[m.tabuleiro.GetLength(0) + 1, m.tabuleiro.GetLength(0) + 1]; ///Aumenta as dimensões do mapa em 1 unidade a cada nova fase do jogo.

            for (int i = 0; i < m.tabuleiro.GetLength(0); i++)     ///Coloca um objeto do tipo Cell em cada posição do mapa.
            {
                for (int j = 0; j < m.tabuleiro.GetLength(0); j++)
                {
                    Cell celula = new Cell();
                    m.tabuleiro[i,j] = celula;
                }
            }

            Robot robo = new Robot(0, 0);                         ///Inicializa o robô no mapa na posição [0,0].
            m.tabuleiro[robo.x, robo.y] = robo;

            Random rand = new Random();

            for (int i = 0; i <= Math.Round((0.07 * m.tabuleiro.Length)); i++) ///Inicializa os elementos do tipo Water no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. A partir do cálculo da porporção de elementos do tipo Water dispostos na primeira fase do jogo, a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                Water agua;
                do 
                {
                    agua = new Water(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[agua.x, agua.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[agua.x, agua.y] = agua;
            }
            for (int i = 0; i <= Math.Round((0.05 * m.tabuleiro.Length)); i++) ///Inicializa os elementos do tipo Tree no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. A partir do cálculo da porporção de elementos do tipo Tree dispostos na primeira fase do jogo, a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                Tree arvore;
                do
                {
                    arvore = new Tree(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[arvore.x, arvore.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[arvore.x, arvore.y] = arvore;
            }
            for (int i = 0; i <= Math.Round(0.02 * m.tabuleiro.Length); i++) ///Inicializa os elementos do tipo RedJewel no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. A partir do cálculo da porporção de elementos do tipo RedJewel dispostos na primeira fase do jogo, a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                RedJewel joiaVermelha;
                do
                {
                    joiaVermelha = new RedJewel(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[joiaVermelha.x, joiaVermelha.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[joiaVermelha.x, joiaVermelha.y] = joiaVermelha;
                m.qtdJewel++;
            }
            for (int i = 0; i <= Math.Round(0.02 * m.tabuleiro.Length); i++) ///Inicializa os elementos do tipo GreenJewel no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. A partir do cálculo da porporção de elementos do tipo GreenJewel dispostos na primeira fase do jogo, a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                GreenJewel joiaVerde;
                do
                {
                    joiaVerde = new GreenJewel(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[joiaVerde.x, joiaVerde.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[joiaVerde.x, joiaVerde.y] = joiaVerde;
                m.qtdJewel++;
            }
            for (int i = 0; i <= Math.Round(0.02 * m.tabuleiro.Length); i++) ///Inicializa os elementos do tipo BlueJewel no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. A partir do cálculo da porporção de elementos do tipo BlueJewel dispostos na primeira fase do jogo, a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                BlueJewel joiaAzul;
                do
                {
                    joiaAzul = new BlueJewel(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[joiaAzul.x, joiaAzul.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[joiaAzul.x, joiaAzul.y] = joiaAzul;
                m.qtdJewel++;
            }
            for (int i = 0; i <= Math.Round((0.02 * m.tabuleiro.Length)); i++) ///Inicializa os elementos do tipo Radioactive no mapa a partir da geração de números aleatórios para a posição [x,y] na matriz. Este elemento é inserido no mapa a partir da fase 2. São inseridos 2 elementos deste tipo no mapa na fase 2, e a mesma proporção é repetida para todas as outras fases, levando em conta o tamanho total da matriz de cada fase.
            {
                Radioactive radioativo;
                do
                {
                    radioativo = new Radioactive(rand.Next(0, m.tabuleiro.GetLength(0)), rand.Next(0, m.tabuleiro.GetLength(0)));
                }
                while (m.tabuleiro[radioativo.x, radioativo.y].GetType() != typeof(Cell)); ///Verifica se existe algum elemento já colocado na posição aleatória que foi criada.
                m.tabuleiro[radioativo.x, radioativo.y] = radioativo;
            }
        }
        /// <summary>
        /// O método imprime na tela as instruções para a fase 2 do jogo.
        /// </summary>
        public void instrucoesFase2()
        {
            Console.WriteLine();
            Console.WriteLine("A partir da fase 2, há um novo elemento no mapa: O elemento Radioativo. Este elemento é representado pelo símbolo !!");
            Console.WriteLine("Este elemento tira 10 pontos de energia quando o robô passa por alguma das posições adjacentes a ele e tira 30 pontos de energia caso o robô se mova para a mesma posição que ele.");
            Console.WriteLine();
        }
    }
}
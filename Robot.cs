using Coletor_Joias;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coletor_Joias
{
    /// <summary>
    /// A classe Robot é uma especialização da classe Cell. Ela representa o robô que se movimenta pelo mapa coletando joias e energia e desviando dos obstáculos.
    /// </summary>
    public class Robot : Cell
    {
        /// <summary>
        /// O atributo Bag representa uma lista de joias que armazena as informações relacionas as joias que foram coletadas pelo robô, sendo essas informações o valor total e a quantidade de joias.
        /// </summary>
        /// <value> Retorna uma lista do tipo Jewel.</value>
        public List<Jewel> Bag { get; set; }

        /// <summary>
        /// O robô pode coletar energia dos obstáculos que são do tipo Tree, das joias que são do tipo BlueJewel e perde parte de sua energia ao passar em posições adjacentes ou ao transpor objetos do tipo Radioactive.
        /// </summary>
        /// <value> Retorna quanta energia armazenada o robô tem.</value>
        public int energia { get; set; }

        /// <summary>
        /// Refere-se ao construtor do robô que recebe a posição [x,y] do robô no mapa.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Robot(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        /// <summary>
        /// Refere-se ao construtor do robô que não recebe parâmetros.
        /// </summary>
        public Robot() { }
            
        /// <summary>
        /// Este método sobrescreve o ToString().
        /// </summary>
        /// <returns>Um Robot é representado no mapa pelos caracteres ME.</returns>
        public override string ToString()
        {
            return "ME ";
        }

        /// <summary>
        /// O método verifica em qual posição o robô se encontra no mapa para que seja possível realizar as movimentações.
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Retorna a posição [x,y] do robô no mapa.</returns>
        public Cell posicaoAtualRobo(Map m)
        {
            Cell c = new Cell();
            for (int i = 0; i < m.tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < m.tabuleiro.GetLength(0); j++)
                {
                    c = m.tabuleiro[i, j];
                    if (c.ToString() == "ME ")
                    {
                        return c;
                    }
                }
            }
            return c;
        }

        /// <summary>
        /// O método sobrescreve a posição dada com um elemento do tipo Cell, com a string "--" na posição anterior do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void movimentar(Map m, int x, int y)
        {
            Cell celula = new Cell();
            celula.x = x;
            celula.y = y;
            m.tabuleiro[x, y] = celula;
        }

        /// <summary>
        /// O método tira 1 ponto de energia do robô a cada movimentação realizada.
        /// </summary>
        /// <param name="r"></param>
        public void tiraEnergia(Robot r)
        {
            if (r.energia > 0)
            {
                r.energia = energia - 1;
            }
        }

        /// <summary>
        /// O método verifica a posição atual do robô, verifica se há algum objeto instransponível abaixo dele e se não houver um objeto instransponível, ele realiza a movimentação para baixo. 
        /// Este método também verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para baixo, se houver são tirados 10 pontos de energia do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
        public void baixo(Map m, Robot r)
        {
            Robot robo = (Robot)r.posicaoAtualRobo(m); ///Verifico a posição atual do robô no mapa
            try
            {
                if (m.tabuleiro[robo.x + 1, robo.y].GetType() != typeof(BlueJewel) && ///Verifico se não há nenhum elemento intransponível no local para qual o robô irá se movimentar.
                    m.tabuleiro[robo.x + 1, robo.y].GetType() != typeof(GreenJewel) &&
                    m.tabuleiro[robo.x + 1, robo.y].GetType() != typeof(RedJewel) &&
                    m.tabuleiro[robo.x + 1, robo.y].GetType() != typeof(Tree) &&
                    m.tabuleiro[robo.x + 1, robo.y].GetType() != typeof(Water))
                {
                    if (m.tabuleiro[robo.x + 1, robo.y].GetType() == typeof(Radioactive)) r.energia = r.energia - 30; ///Se a posição que o robô irá sobrepor for um elemento radioativo, o robô perde 30 pontos de energia.
                    robo.movimentar(m, robo.x, robo.y); ///Insiro "--" na posição atual do robô.
                    robo.x = robo.x + 1;                ///Para onde o robô vai, nova posição do robô no tabuleiro.
                    m.tabuleiro[robo.x, robo.y] = robo; ///Inclusão do robô em sua nova posição no tabuleiro.
                    r.tiraEnergia(r);                   ///Tira energia caso seja realizada a movimentação.
                }
                else
                {
                    throw new Exception();               ///Quando ele tentar ultrapassar os limites do mapa ou tentar se movimentar para a posição que tem uma joia ou obstáculo, será lançada uma exceção informando que o movimento não é permitido.
                }
            }
            catch
            {
                Console.WriteLine("Este movimento não é permitido");
                Console.WriteLine();
            }
            try ///Verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para baixo, se houver são tirados 10 pontos de energia do robô.
            {
                if (m.tabuleiro[robo.x + 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10; 
                if (m.tabuleiro[robo.x - 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y - 1].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y + 1].GetType() == typeof(Radioactive)) r.energia -= 10;
            }
            catch
            { }
        }

        /// <summary>
        /// O método verifica a posição atual do robô, verifica se há algum objeto instransponível acima dele e se não houver um objeto instransponível, ele realiza a movimentação para cima. 
        /// Este método também verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para cima, se houver são tirados 10 pontos de energia do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
        public void cima(Map m, Robot r)
        {
            Robot robo = (Robot)r.posicaoAtualRobo(m); ///Verifico a posição atual do robô no mapa
            try
            {
                if (m.tabuleiro[robo.x - 1, robo.y].GetType() != typeof(BlueJewel) && ///Verifico se não há nenhum elemento intransponível no local para qual o robô irá se movimentar.
                    m.tabuleiro[robo.x - 1, robo.y].GetType() != typeof(GreenJewel) &&
                    m.tabuleiro[robo.x - 1, robo.y].GetType() != typeof(RedJewel) &&
                    m.tabuleiro[robo.x - 1, robo.y].GetType() != typeof(Tree) &&
                    m.tabuleiro[robo.x - 1, robo.y].GetType() != typeof(Water))
                {
                    if (m.tabuleiro[robo.x - 1, robo.y].GetType() == typeof(Radioactive)) r.energia = r.energia - 30; ///Se a posição que o robô irá sobrepor for um elemento radioativo, o robô perde 30 pontos de energia.
                    robo.movimentar(m, robo.x, robo.y); ///Insiro "--" na posição atual do robô.
                    robo.x = robo.x - 1;                ///Para onde o robô vai, nova posição do robô no tabuleiro.
                    m.tabuleiro[robo.x, robo.y] = robo; ///Inclusão do robô em sua nova posição no tabuleiro.
                    r.tiraEnergia(r);                   ///Tira energia caso seja realizada a movimentação.
                }
                else
                {
                    throw new Exception();               ///Quando ele tentar ultrapassar os limites do mapa ou tentar se movimentar para a posição que tem uma joia ou obstáculo, será lançada uma exceção informando que o movimento não é permitido.
                }
            }
            catch
            {
                Console.WriteLine("Este movimento não é permitido");
                Console.WriteLine();
            }
            //    try     ///Verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para cima, se houver são tirados 10 pontos de energia do robô.
            //{
                if (m.tabuleiro[robo.x + 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10; 
                if (m.tabuleiro[robo.x - 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y - 1].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y + 1].GetType() == typeof(Radioactive)) r.energia -= 10;
            //}
            //catch
            //{ }
        }
        /// <summary>
        /// O método verifica a posição atual do robô, verifica se há algum objeto instransponível a esquerda dele e se não houver um objeto instransponível, ele realiza a movimentação para a esquerda. 
        /// Este método também verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para a esquerda, se houver são tirados 10 pontos de energia do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
        public void esquerda(Map m, Robot r)
        {
            Robot robo = (Robot)r.posicaoAtualRobo(m); ///Verifico a posição atual do robô no mapa
            try 
            { 
                if (m.tabuleiro[robo.x, robo.y - 1].GetType() != typeof(BlueJewel) && ///Verifico se não há nenhum elemento intransponível no local para qual o robô irá se movimentar.
                    m.tabuleiro[robo.x, robo.y - 1].GetType() != typeof(GreenJewel) &&
                    m.tabuleiro[robo.x, robo.y - 1].GetType() != typeof(RedJewel) &&
                    m.tabuleiro[robo.x, robo.y - 1].GetType() != typeof(Tree) &&
                    m.tabuleiro[robo.x, robo.y - 1].GetType() != typeof(Water))
                {
                    if (m.tabuleiro[robo.x, robo.y - 1].GetType() == typeof(Radioactive)) r.energia = r.energia - 30; ///Se a posição que o robô irá sobrepor for um elemento radioativo, o robô perde 30 pontos de energia.
                    robo.movimentar(m, robo.x, robo.y); ///Insiro "--" na posição atual do robô.
                    robo.y = robo.y - 1;                ///Para onde o robô vai, nova posição do robô no tabuleiro.
                    m.tabuleiro[robo.x, robo.y] = robo; ///Inclusão do robô em sua nova posição no tabuleiro.
                    r.tiraEnergia(r);                   ///Tira energia caso seja realizada a movimentação.
                }
                else
                {
                    throw new Exception();               ///Quando ele tentar ultrapassar os limites do mapa ou tentar se movimentar para a posição que tem uma joia ou obstáculo, será lançada uma exceção informando que o movimento não é permitido.
                }
            }
            catch
            {
                Console.WriteLine("Este movimento não é permitido");
                Console.WriteLine();
            }
            try ///Verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para a esquerda, se houver são tirados 10 pontos de energia do robô.
            {
                if (m.tabuleiro[robo.x + 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x - 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y + 1].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y - 1].GetType() == typeof(Radioactive)) r.energia -= 10;
            }
            catch
            { }
        }

        /// <summary>
        /// O método verifica a posição atual do robô, verifica se há algum objeto instransponível a direita dele e se não houver um objeto instransponível, ele realiza a movimentação para a direita. 
        /// Este método também verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para a direita, se houver são tirados 10 pontos de energia do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
        public void direita(Map m, Robot r)
        {
            Robot robo = (Robot)r.posicaoAtualRobo(m); ///Verifico a posição atual do robô no mapa
            try
            {
                if (m.tabuleiro[robo.x, robo.y + 1].GetType() != typeof(BlueJewel) && ///Verifico se não há nenhum elemento intransponível no local para qual o robô irá se movimentar.
                    m.tabuleiro[robo.x, robo.y + 1].GetType() != typeof(GreenJewel) &&
                    m.tabuleiro[robo.x, robo.y + 1].GetType() != typeof(RedJewel) &&
                    m.tabuleiro[robo.x, robo.y + 1].GetType() != typeof(Tree) &&
                    m.tabuleiro[robo.x, robo.y + 1].GetType() != typeof(Water))
                {
                    if (m.tabuleiro[robo.x, robo.y + 1].GetType() == typeof(Radioactive)) r.energia = r.energia - 30; ///Se a posição que o robô irá sobrepor for um elemento radioativo, o robô perde 30 pontos de energia.
                    robo.movimentar(m, robo.x, robo.y); ///Insire "--" na posição atual do robô.
                    robo.y = robo.y + 1;                ///Para onde o robô vai, nova posição do robô no tabuleiro.
                    m.tabuleiro[robo.x, robo.y] = robo; ///Inclusão do robô em sua nova posição no tabuleiro.
                    r.tiraEnergia(r);                   ///Tira energia caso seja realizada a movimentação.
                }
                else
                {
                    throw new Exception();               ///Quando ele tentar ultrapassar os limites do mapa ou tentar se movimentar para a posição que tem uma joia ou obstáculo, será lançada uma exceção informando que o movimento não é permitido.
                }
            }
            catch
            {
                Console.WriteLine("Este movimento não é permitido");
                Console.WriteLine();
            }
            try ///Verifica se há algum objeto do tipo Radioactive nas posições adjacentes ao robô quando este se movimenta para a direita, se houver são tirados 10 pontos de energia do robô.
            {
                if (m.tabuleiro[robo.x + 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x - 1, robo.y].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y + 1].GetType() == typeof(Radioactive)) r.energia -= 10;
                if (m.tabuleiro[robo.x, robo.y - 1].GetType() == typeof(Radioactive)) r.energia -= 10;
            }
            catch
            { }
        }

        /// <summary>
        /// O método é utilizado para que o robô possa coletar joias e energia. Este verifica a posição do robô no mapa, verifica se tem algo para ser coletado nas posições adjacentes a ele e coleta os objetos, adicionando energia à variável energia e as joias a Bag (Lista de joias) do robô.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="r"></param>
        public void coletar(Map m, Robot r)
        {
            Robot roboAux = new Robot(r.x,r.y); ///Cria uma variável auxiliar do tipo robô para armazenar a posição do robô no mapa.

            roboAux = (Robot)roboAux.posicaoAtualRobo(m); ///Verifica a posição do robô no mapa e armazena na variável auxiliar do tipo robô.
            if ((roboAux.x - 1) >= 0)  ///Verifica se acima está dentro dos limites do tabuleiro.
                if (m.tabuleiro[roboAux.x - 1, roboAux.y].GetType() == typeof(BlueJewel) || ///Verifica se tem algo acima para ser coletado.
                    m.tabuleiro[roboAux.x - 1, roboAux.y].GetType() == typeof(GreenJewel) ||
                    m.tabuleiro[roboAux.x - 1, roboAux.y].GetType() == typeof(RedJewel))
                {
                    if (m.tabuleiro[roboAux.x - 1, roboAux.y].GetType() == typeof(BlueJewel)) energia += 5; ///Se for um objeto do tipo BlueJewel, incrementa a energia em 5 pontos.
                    r.Bag.Add((Jewel)m.tabuleiro[roboAux.x - 1, roboAux.y]); ///Coleta a joia e adiciona ela a Bag do robô.
                    m.qtdJewel--;                                            ///Diminui 1 na quantidade de joias que falta o robô coletar no mapa.
                    m.tabuleiro[roboAux.x - 1, roboAux.y] = new Cell();      ///Insere "--" na posição da qual a joia foi coletada.
                    if (m.qtdJewel == 0) m.tabuleiroProximaFase(m);          ///Verifica se a quantidade de joias restantes é igual a zero, e se for, passa para a próxima fase do jogo.
                }
                else if (m.tabuleiro[roboAux.x - 1, roboAux.y].GetType() == typeof(Tree)) energia += 3; ///Se for um objeto do tipo Tree, incrementa a energia em 3 pontos.
            if ((roboAux.x + 1) <= m.tabuleiro.GetLength(0)-1) ///Verifica se abaixo está dentro dos limites do tabuleiro.
                if (m.tabuleiro[roboAux.x + 1, roboAux.y].GetType() == typeof(BlueJewel) || ///Verifica se tem algo abaixo para ser coletado.
                    m.tabuleiro[roboAux.x + 1, roboAux.y].GetType() == typeof(GreenJewel) ||
                    m.tabuleiro[roboAux.x + 1, roboAux.y].GetType() == typeof(RedJewel))
                {
                    if (m.tabuleiro[roboAux.x + 1, roboAux.y].GetType() == typeof(BlueJewel)) energia += 5; ///Se for um objeto do tipo BlueJewel, incrementa a energia em 5 pontos.
                    r.Bag.Add((Jewel)m.tabuleiro[roboAux.x + 1, roboAux.y]); ///Coleta a joia e adiciona ela a Bag do robô.
                    m.qtdJewel--;                                            ///Diminui 1 na quantidade de joias que falta o robô coletar no mapa.
                    m.tabuleiro[roboAux.x + 1, roboAux.y] = new Cell();      ///Insere "--" na posição da qual a joia foi coletada.
                    if (m.qtdJewel == 0) m.tabuleiroProximaFase(m);          ///Verifica se a quantidade de joias restantes é igual a zero, e se for, passa para a próxima fase do jogo.
                }
                else if (m.tabuleiro[roboAux.x + 1, roboAux.y].GetType() == typeof(Tree)) energia += 3; ///Se for um objeto do tipo Tree, incrementa a energia em 3 pontos.

            if ((roboAux.y + 1) <= m.tabuleiro.GetLength(0) - 1) ///Verifica se a direita está dentro dos limites do tabuleiro.
                if (m.tabuleiro[roboAux.x, roboAux.y + 1].GetType() == typeof(BlueJewel) || ///Verifica se tem algo a direita para ser coletado.
                    m.tabuleiro[roboAux.x, roboAux.y + 1].GetType() == typeof(GreenJewel) ||
                    m.tabuleiro[roboAux.x, roboAux.y + 1].GetType() == typeof(RedJewel))
                {
                    if (m.tabuleiro[roboAux.x, roboAux.y + 1].GetType() == typeof(BlueJewel)) energia += 5; ///Se for um objeto do tipo BlueJewel, incrementa a energia em 5 pontos.
                    r.Bag.Add((Jewel)m.tabuleiro[roboAux.x, roboAux.y + 1]); ///Coleta a joia e adiciona ela a Bag do robô.
                    m.qtdJewel--;                                            ///Diminui 1 na quantidade de joias que falta o robô coletar no mapa.
                    m.tabuleiro[roboAux.x, roboAux.y + 1] = new Cell();      ///Insere "--" na posição da qual a joia foi coletada.
                    if (m.qtdJewel == 0) m.tabuleiroProximaFase(m);          ///Verifica se a quantidade de joias restantes é igual a zero, e se for, passa para a próxima fase do jogo.
                }
                else if (m.tabuleiro[roboAux.x, roboAux.y + 1].GetType() == typeof(Tree)) energia += 3; ///Se for um objeto do tipo Tree, incrementa a energia em 3 pontos.

            if ((roboAux.y - 1) >= 0) ///Verifica se a esquerda está dentro dos limites do tabuleiro.
                if (m.tabuleiro[roboAux.x, roboAux.y - 1].GetType() == typeof(BlueJewel) || ///Verifica se tem algo a esquerda para ser coletado.
                    m.tabuleiro[roboAux.x, roboAux.y - 1].GetType() == typeof(GreenJewel) ||
                    m.tabuleiro[roboAux.x, roboAux.y - 1].GetType() == typeof(RedJewel))
                {
                    if (m.tabuleiro[roboAux.x, roboAux.y - 1].GetType() == typeof(BlueJewel)) energia += 5; ///Se for um objeto do tipo BlueJewel, incrementa a energia em 5 pontos.
                    r.Bag.Add((Jewel)m.tabuleiro[roboAux.x, roboAux.y - 1]); ///Coleta a joia e adiciona ela a Bag do robô.
                    m.qtdJewel--;                                            ///Diminui 1 na quantidade de joias que falta o robô coletar no mapa.
                    m.tabuleiro[roboAux.x, roboAux.y - 1] = new Cell();      ///Insere "--" na posição da qual a joia foi coletada.
                    if (m.qtdJewel == 0) m.tabuleiroProximaFase(m);          ///Verifica se a quantidade de joias restantes é igual a zero, e se for, passa para a próxima fase do jogo.
                }
                else if (m.tabuleiro[roboAux.x, roboAux.y - 1].GetType() == typeof(Tree)) energia += 3; ///Se for um objeto do tipo Tree, incrementa a energia em 3 pontos.
        }

        /// <summary>
        /// O método realiza a soma de todas as joias armazenadas na Bag do robô e imprime o valor total das joias coletadas.
        /// </summary>
        /// <param name="r"></param>
        public void valorTotal(Robot r)
        {
            int total = 0;
            for (int i = 0; i < Bag.Count; i++)
            {
                total += Bag[i].valor;
            }
            Console.Write("Valor das joias coletadas: " + total + " | ");
        }

        /// <summary>
        /// O método realiza a contagem de todas as joias armazenadas na Bag do robô e imprime a quantidade total das joias coletadas.
        /// </summary>
        /// <param name="r"></param>
        public void qtdTotal(Robot r)
        {
            int contagem = r.Bag.Count();
            Console.WriteLine("Quantidade de joias coletadas:" + contagem);
        }

        /// <summary>
        /// O método imprime a quantidade de energia armazenada pelo robô.
        /// </summary>
        /// <param name="r"></param>
        public void qtdEnergia(Robot r)
        {
            Console.WriteLine("Quantidade de energia: " + r.energia);
        }
    }
}
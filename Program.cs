using Coletor_Joias;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography.X509Certificates;
/// <summary>
/// A classe Coletor é a classe base que contém os métodos Main e as instruções para a primeira fase.
/// </summary>
public class Coletor
{
    delegate void baixo(Map m, Robot r); ///Criação do delegate que realizará o movimento para baixo.
    static event baixo OnBaixo; /// Criação do evento referente ao delegate para baixo.

    delegate void cima(Map m, Robot r); /// Criação do delegate que realizará o movimento para cima.
    static event cima OnCima; /// Criação do evento referente ao delegate para cima.

    delegate void esquerda(Map m, Robot r); /// Criação do delegate que realizará o movimento para a esquerda.
    static event esquerda OnEsquerda; /// Criação do evento referente ao delegate para a esquerda.

    delegate void direita(Map m, Robot r); /// Criação do delegate que realizará o movimento para a direita.
    static event direita OnDireita; /// Criação do evento referente ao delegate para a direita.

    delegate void coletar(Map m, Robot r); /// Criação do delegate que realizará a coleta de joias e energia.
    static event coletar OnColetar; /// Criação do evento referente ao delegate para coletar.


    /// <summary>
    /// O método main é responsável por criar o mapa no início do jogo e por realizar a leitura dos eventos do teclado.
    /// </summary>
    public static void Main() 
    {
        ///Início do jogo
        Map m = new Map(); ///Inicialização de um novo mapa.
        m.qtdJewel = 0;    ///Inicialização da quantidade de joias no mapa.
        Robot r = new Robot ///Inicialização do robô.
        {
            Bag = new List<Jewel>(), ///Inicialização da Bag do robô.
            energia = 5              ///Inicialização da energia em 5 pontos.
        };
        m = m.inicioJogo(m);  ///Atribui objetos do tipo Cell a todas as posições do mapa.
        instrucoes();       ///Imprime na tela as instruções do jogo.
        m.tabuleiroFase1(m); ///Posiciona os elementos da fase 1 no mapa.
        m.mostraMapa(m);     ///Imprime as posições do mapa no console.

        bool running = true; ///Criação da variável running para definir quando o programa irá rodar e parar de rodar.
        do
        {
            r.valorTotal(r); ///Imprime o valor total de joias coletadas pelo robô.
            r.qtdTotal(r);   ///Imprime a quantidade total de joias coletadas pelo robô.
            r.qtdEnergia(r); ///Imprime a quantidade de energia coletada pelo robô.
            Console.WriteLine("Enter the command: ");
            Console.WriteLine();
            OnBaixo = r.baixo; ///Atribui o método baixo do robô ao evento.
            OnCima = r.cima; ///Atribui o método cima do robô ao evento.
            OnDireita = r.direita; ///Atribui o método direita do robô ao evento.
            OnEsquerda = r.esquerda; ///Atribui o método esquerda do robô ao evento.
            OnColetar = r.coletar; ///Atribui o método coletar do robô ao evento.

            if (r.energia <= 0) ///Caso a energia seja menor ou igual a zero, o jogo termina e imprime uma mensagem de que a energia acabou.
            {
                running = false;
                Console.WriteLine();
                Console.WriteLine("A energia do robô acabou! :( ");
                Console.WriteLine("Fim do jogo");
            }
            else if (m.faseAtual > 21) ///Caso o jogador passe da fase 21, o jogo terminará para que o tabuleiro não exceda o tamanho de 30x30.
            {
                running = false;
                Console.WriteLine();
                Console.WriteLine("Parabéns! Você chegou ao final do jogo");
                Console.WriteLine();
            }
            else
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case 's': ///Move o robô para baixo quando o usuário apertar a tecla s.
                        OnBaixo?.Invoke(m, r);
                        break;
                    case 'a': ///Move o robô para a esquerda quando o usuário apertar a tecla a.
                        OnEsquerda?.Invoke(m, r);
                        break;
                    case 'd': ///Move o robô para direita quando o usuário apertar a tecla d.
                        OnDireita?.Invoke(m, r);
                        break;
                    case 'w': ///Move o robô para cima quando o usuário apertar a tecla w.
                        OnCima?.Invoke(m, r);
                        break;
                    case 'g': ///Coleta joias e energia nas posições adjcentes ao robô quando o usuário apertar a tecla g.
                        OnColetar?.Invoke(m, r);
                        break;
                    case 'q': ///Para a execução quando o usuário apertar a tecla q.
                        running = false;
                        break;
                }
                Console.WriteLine();
                m.mostraMapa(m);
            }
        }
        while (running);
    }

    /// <summary>
    /// O método imprime na tela as instruções do início do jogo.
    /// </summary>
    public static void instrucoes()
    {
        Console.WriteLine();
        Console.WriteLine("Seja bem-vindo ao Jewel Colector!"); ///Instruções do jogo que serão mostradas na tela ao início do jogo
        Console.WriteLine();
        Console.WriteLine("Objetivo: Colete todas as joias antes que a sua energia acabe.");
        Console.WriteLine("Como jogar: O robô (ME) se inicia na posição 0,0. Utilize as teclas w, a, s e d para movimentar o robô e a tecla g para coletar joias e recarregar suas energias.");
        Console.WriteLine("As joias azuis (JB) e as árvores ($$) fornecem energia, quando estiver em posições adjacentes a elas, utilize a tecla g para coletar.");
        Console.WriteLine("Utilize também a tecla g para coletar as joias do mapa. Ao coletar todas as joias de uma fase, você passará para a fase seguinte.");
        Console.WriteLine("O jogo conta com obstáculos, como a água (##) e as árvores ($$), que você precisará desviar para coletar as joias.");
        Console.WriteLine("Cuidado para não acabar sua energia ao desviar dos obstáculos!");
        Console.WriteLine("Regras do jogo: A cada movimento, você perde 1 ponto de energia. Quando sua energia acabar, o jogo chegará ao fim.");
        Console.WriteLine();
        Console.WriteLine("Boa sorte e divirta-se! :) ");
        Console.WriteLine();
        Console.WriteLine("Trabalho realizado por Bruna Mattioli de Oliveira.");
        Console.WriteLine();
    }
}
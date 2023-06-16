using Coletor_Joias;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
/// <summary>
/// O método main é responsável por criar o mapa no início do jogo e por realizar a leitura dos eventos do teclado.
/// </summary>
void Main()
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
        char command = Console.ReadKey().KeyChar; ///Lê os comandos do teclado que o usuário irá inserir para jogar.
        Console.WriteLine();

        if (command.Equals('q'))
        {
            running = false; ///Para a execução quando o usuário apertar a tecla q.
        }
        else if (command.Equals('w'))
        {
            r.cima(m, r);   ///Move o robô para cima quando o usuário apertar a tecla w.
            m.mostraMapa(m);
        }
        else if (command.Equals('s'))
        {
            r.baixo(m, r); ///Move o robô para baixo quando o usuário apertar a tecla s.
            m.mostraMapa(m);
        }
        else if (command.Equals('a'))
        {
            r.esquerda(m, r); ///Move o robô para a esquerda quando o usuário apertar a tecla a.
            m.mostraMapa(m);
        }
        else if (command.Equals('d'))
        {
            r.direita(m, r); ///Move o robô para direita quando o usuário apertar a tecla d.
            m.mostraMapa(m);
        }
        else if (command.Equals('g'))
        {
            r.coletar(m, r); ///Coleta joias e energia nas posições adjcentes ao robô quando o usuário apertar a tecla g.
            m.mostraMapa(m);
        }
        if (r.energia <= 0) ///Caso a energia seja menor ou igual a zero, o jogo termina e imprime uma mensagem de que a energia acabou.
        {
            running = false;
            Console.WriteLine("A energia do robô acabou! :( ");
            Console.WriteLine("Fim do jogo");
        }
    }
    while (running);
}
Main();

void instrucoes()
{
    Console.WriteLine();
    Console.WriteLine("Seja bem-vindo ao Jewel Colector"); ///Instruções do jogo que serão mostradas na tela ao início do jogo
    Console.WriteLine();
    Console.WriteLine("Objetivo: Colete todas as joias antes que a sua energia acabe.");
    Console.WriteLine();
    Console.WriteLine("Como jogar: O robô (ME) se inicia na posição 0,0. Utilize as teclas w, a, s e d para movimentar o robô e a tecla g para coletar joias e recarregar suas energias.");
    Console.WriteLine("As joias azuis (JB) e as árvores ($$) fornecem energia, quando estiver em posições adjacentes a elas, utilize a tecla g para coletar.");
    Console.WriteLine("Ao coletar todas as joias de uma fase, você passará para a fase seguinte.");
    Console.WriteLine("O jogo conta com obstáculos, como a água (##) e as árvores ($$), que você precisará desviar para coletar as joias.");
    Console.WriteLine("Cuidado para não acabar sua energia ao desviar dos obstáculos!");
    Console.WriteLine();
    Console.WriteLine("Regras do jogo: A cada movimento, você perde 1 ponto de energia. Quando sua energia acabar, o jogo chegará ao fim.");
    Console.WriteLine();
}

///Eventos

class publisher ///classe que publica o evento
{
    public delegate void TeclaEventHandler(object sender, EventArgs args);
    public event TeclaEventHandler Tecla;

    protected virtual async void OnTecla()
    {
        switch (Console.ReadKey().KeyChar)
        {
            case 'a':
                break;
            case 's':
                break;
            case 'd':
                break;
            case 'w':
                break;
            case 'g':
                break;
        }
    }
}

class subscriber ///classe que subscreve para receber o evento
{
    public void OnTeclaEventHandler(object sender, EventArgs args)
    {
        ///https://www.macoratti.net/18/01/c_event1.htm
        ///https://www.macoratti.net/12/06/c_event1.htm
    }
}
using Coletor_Joias;
using System.Diagnostics.CodeAnalysis;
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
    m = m.inicioJogo();  ///Atribui objetos do tipo Cell a todas as posições do mapa.  
    m.tabuleiroFase1(m); ///Posiciona os elementos da fase 1 no mapa.
    m.mostraMapa(m);     ///Imprime as posições do mapa no console.

    bool running = true; ///Criação da variável running para definir quando o programa irá rodar e parar de rodar.
    do
    {   r.valorTotal(r); ///Imprime o valor total de joias coletadas pelo robô.
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
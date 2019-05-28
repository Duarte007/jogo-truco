using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9{
    class Program{
        static int[] copas;
        static int[] espadas;
        static int[] ouros;
        static int[] paus;
        static string[] jogador1;
        static string[] jogador2;
        static string cartaJogador1 = "";
        static string cartaJogador2 = "";
        static int rodadaJogador1 = 0;
        static int rodadaJogador2 = 0;
        static int pontoJogador1 = 0;
        static int pontoJogador2 = 0;
        static int tento = 0;
        static int[] escolha = new int[3];


            private static void construtor(){
                //test for commiting
                copas = new int[12];
                espadas = new int[12];
                ouros = new int[12];
                paus = new int[12];
                jogador1 = new string[3];
                jogador2 = new string[3];
                for (int pos = 0; pos < copas.Length; pos++) {
                    copas[pos] = 0;
                    espadas[pos] = 0;
                    ouros[pos] = 0;
                    paus[pos] = 0;
                }
            }
            static void Main(string[] args) {
                construtor();
                distribuircarta(jogador1);
                distribuircarta(jogador2);
                
                for (int pos = 0; pos < jogador1.Length; pos++){
                    Console.WriteLine("Carta " + (pos + 1) + " do jogador 1 = "+ jogador1[pos]);
                    Console.WriteLine("Carta " + (pos + 1) + " do jogador 2 = "+ jogador2[pos]);
                }

                Console.ReadKey();
            }
            static void distribuircarta(string[] jogador) {
                for (int pos = 0; pos < jogador.Length; pos++) {
                    jogador[pos] = getCarta();
                    System.Threading.Thread.Sleep(1000);
                }

            }
            static string getCarta(){
                string retorno = "";
                Random valorAleatorio = new Random();

                switch (valorAleatorio.Next() % 4) {
                    case 0:
                        retorno = sorteio(copas, 'C');
                        break;
                    case 1:
                        retorno = sorteio(ouros, 'O');
                        break;
                    case 2:
                        retorno = sorteio(espadas, 'E');
                        break;
                    case 3:
                        retorno = sorteio(paus, 'P');
                        break;
                    default:
                        break;
                }
                return retorno;
            }
            static string sorteio(int[] pilha, char naipe) {
                string retorno = "";
                Random valorAleatorio = new Random();
                int pos;
                do {
                    pos = valorAleatorio.Next() % 12;

                } while (pos == 0 || pos == 1 || pilha[pos] == -1);

                pilha[pos] = -1;
                switch (pos) {
                    case 8:
                        retorno = "A" + naipe;
                        break;
                    case 9:
                        retorno = "J" + naipe;
                        break;
                    case 10:
                        retorno = "Q" + naipe;
                        break;
                    case 11:
                        retorno = "K" + naipe;
                        break;
                    default:
                        retorno = "" + pos + naipe;
                        break;

                }
                return retorno;
            }

            static void reiniciaJogo() {
                for (int pos = 0; pos < copas.Length; pos++) {
                    copas[pos] = 0;
                    espadas[pos] = 0;
                    ouros[pos] = 0;
                    paus[pos] = 0;
                }
            }

            static int getPeso(string carta)    {   
                string naipe = ""+carta[1];
                string valor = ""+carta[0];
                string[] pesoCartas = new string[10] {"4","5","6","7","Q","J","K","A","2","3"};
                string[] pesoManilhas = new string[4] {"7O","AE","7C","4P"};
                for(int i=1;i<=pesoManilhas.Length;i++){
                    if(carta==pesoManilhas[i-1]){
                        return 10+i;
                    } 
                }

                for(int i=1;i<=pesoCartas.Length;i++){
                    if(valor==pesoCartas[i-1]){
                        return i;
                    } 
                }
                
                return 0;
            }
        
            static bool pedeTruco(){
                int opcao = 0;
                Console.WriteLine("Voce deseja pedir truco?\n\t1)Sim\n\t2)Nao");
                opcao = int.Parse(Console.ReadLine());
                if (opcao == 1){
                    int a;
                    Random frase = new Random();
                    a = frase.Next(1, 4);
                    switch (a){
                        case 1:
                            Console.WriteLine("E´ truco nessa porra");
                            break;

                        case 2:
                            Console.WriteLine("Truco na cabeca!");
                            break;
                        case 3:
                            Console.WriteLine("So´ jogo no truco!");
                            break;
                        case 4:
                            Console.WriteLine("Truco! Pede seis!!");
                            break;
                    }
                    return true;
                }
                else{
                    return false;
                }
            }
            static void placar(){

            if (rodadaJogador1 > rodadaJogador2) {
                pontoJogador1 += tento;
            } else if (rodadaJogador1 < rodadaJogador2) {
                pontoJogador2 += tento;
            } 

            Console.WriteLine("Jogador1: " + pontoJogador1 + "a Jogador2: " + pontoJogador2);
        }

            static void jogadaJogador()
                {
                    int jogadas = 0;
                    int numcarta = 0;
                    while (jogadas < 3)
                    {
                        int i = 0;
                        for (int pos = 0; pos < jogador1.Length; pos++)
                        {
                            if (escolha[pos] == 0)
                            {
                                Console.WriteLine("Carta" + (pos + 1) + " do jogador 1 = " + jogador1[pos]);
                            }
                        }
                        while (i == 0)
                        {
                            Console.WriteLine("Digite o número da carta que você quer jogar");
                            numcarta = int.Parse(Console.ReadLine());
                            if(numcarta <= escolha.Length)
                            {
                                if (escolha[numcarta - 1] == 0)
                                {
                                    Console.WriteLine("Você jogou a carta " + jogador1[numcarta - 1]);
                                    i = 1;
                                    escolha[numcarta - 1] = numcarta;
                                    jogadas++;
                                }
                                else
                                {
                                    Console.WriteLine("Carta inválida, digite novamente");
                                    for (int pos = 0; pos < jogador1.Length; pos++)
                                    {
                                        if (escolha[pos] == 0)
                                        {
                                            Console.WriteLine("Carta" + (pos + 1) + " do jogador 1 = " + jogador1[pos]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Número de carta inválido, digite novamente");
                            }                    
                        }                
                    }
                    for (int pos = 0; pos < escolha.Length; pos++)
                    {
                        escolha[pos] = 0;
                    }
                }

            static void jogadaBot(string[] jogador2){
                int pos;
                Random valorAleatorio = new Random();
                if(cartaJogador1 != ""){
                    // to do...
                } else {
                    pos = valorAleatorio.Next() % 4;
                    cartaJogador2 = jogador2[pos];
                }
                pos = int.Parse(Console.ReadLine());
                cartaJogador2 = jogador2[pos];
            }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9 {
    class Program {
        static int[] copas;
        static int[] espadas;
        static int[] ouros;
        static int[] paus;
        static string[] jogador1;
        static string[] jogador2;
        static string cartaJogador1 = "";
        static string cartaJogador2 = "";
        static int pontoJogador1 = 0;
        static int pontoJogador2 = 0;
        static int tento = 2;
        static int[] escolha = new int[3];
        static bool vezJogador1 = true;
        static bool vezJogador2 = false;
        static bool correr = false;
        static int rodada = 1;
        static int tentoJogador = 0;
        static int tentoBot = 0;
        static bool trucoJog = false;


        private static void construtor() {
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

            controleJogada();

        }

        static void distribuircarta(string[] jogador) {
            for (int pos = 0; pos < jogador.Length; pos++) {
                jogador[pos] = getCarta();
                System.Threading.Thread.Sleep(200);
            }

        }

        static string getCarta() {
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

        static void controleJogada(){
             while (pontoJogador1 < 12 && pontoJogador2 < 12) {
                if(correr && pontoJogador1 < 12 && pontoJogador2 < 12){
                    correr = false;
                    vezJogador1 = false;
                    vezJogador2 = true;
                    proximaRodada();
                } else if(tentoJogador == 2 || tentoBot == 2){
                    System.Threading.Thread.Sleep(2000);
                    proximaRodada();
                } else if (vezJogador1 && rodada <= 6) {
                    Console.WriteLine("\n\n>>>>>>> Vez do jogador 1:\n\n ");
                    menu();
                    if((rodada-1) % 2 == 0){ 
                        getPlacarDaRodada();
                    }
                } else if (vezJogador2 && rodada <= 6) {
                    Console.WriteLine("\n\n>>>>>>> Vez do BOT:\n\n ");
                    System.Threading.Thread.Sleep(2000);
                    jogadaBot();
                    if((rodada-1) % 2 == 0){
                        getPlacarDaRodada();
                    }
                } else {
                    proximaRodada();
                }

            }

            if (pontoJogador1 > pontoJogador2) {
                Console.WriteLine("Jogador1 ganhou.");
                 Console.WriteLine("\tPlacar:\nJogador 1: " + pontoJogador1 + "\nBot: " + pontoJogador2+"\n");
            } else {
                Console.WriteLine("BOT ganhou.");
                 Console.WriteLine("\tPlacar:\nJogador 1: " + pontoJogador1 + "\nBot: " + pontoJogador2+"\n");
            }

            Console.ReadKey();

        }

        static void proximaRodada(){
            System.Threading.Thread.Sleep(2000);
            carregando(1000);
            setTento();
            cartaJogador1 = "";
            cartaJogador2 = "";
            rodada = 1;
            reiniciaJogo();
            distribuircarta(jogador1);
            distribuircarta(jogador2);
            zeraEscolhaJogador1();
            tento = 2;
            tentoJogador = 0;
            tentoBot = 0;
            trucoJog = false;
        }

        static void zeraEscolhaJogador1() {
            for (int i = 0; i < escolha.Length; i++) { 
                escolha[i] = 0;
            }
        }

        static void menu() {
            int opc = 0;

            mostraCartasJogador();

            Console.WriteLine("\t1 - Jogar\n\t2 - Pedir Truco/Pedir mais\n\t3 - Correr\n");
            opc = int.Parse(Console.ReadLine());

            switch (opc) {

                case 1:
                    jogadaJogador();
                    vezJogador1 = false;
                    vezJogador2 = true;
                    break;

                case 2:
                    truco(false);
                    break;

                case 3: 
                    correr = true;
                    pontoJogador2 += tento;
                    Console.WriteLine("Jogador 1 correu!");
                    break;

                default:
                    Console.WriteLine("Opcao invalida!");
                    break;
            }
        }

        static int getPeso(string carta) {

            if(carta == "") return 15;

            string naipe = "" + carta[1];
            string valor = "" + carta[0];
            string[] pesoCartas = new string[10] { "4", "5", "6", "7", "Q", "J", "K", "A", "2", "3" };
            string[] pesoManilhas = new string[4] { "7O", "AE", "7C", "4P" };
            for (int i = 1; i <= pesoManilhas.Length; i++) {
                if (carta == pesoManilhas[i - 1]) {
                    return 10 + i;
                }
            }

            for (int i = 1; i <= pesoCartas.Length; i++) {
                if (valor == pesoCartas[i - 1]) {
                    return i;
                }
            }

            return 0;
        }

        static void truco(bool trucoBot) {
            if(!trucoJog || trucoBot){
                trucoJog = true;
                if (tento == 2) {
                    pedeTruco(new string[] {"E´ truco nessa porra", "Truco na cabeca!", "So´ jogo no truco!", "Truco! Pede seis!!"}, 4, trucoBot);
                } else if (tento == 4) {
                    pedeTruco(new string[] {"Seis rato!", "SEEEEEEEIIIIISS", "Seis!!", "AHHHHHHHHHHHH SEEEEEEEEEEEEEEEEEEIIIIIIIIISSSS"}, 6, trucoBot);
                } else if (tento == 6) {
                    pedeTruco(new string[] {"Nove rato!", "NOOOOVEEEEEE", "Nove!!", "AHHHHHHHHHHHH NOOOVEEEEEE"}, 9, trucoBot);
                } else if (tento == 9) {
                    pedeTruco(new string[] {"Queda rato!", "QUEEEEDAAAAAA", "Doze!!", "AHHHHHHHHHHHH DOOOOOOZEEEEEEE"}, 12, trucoBot);
                }
            } else {
                vezJogador1 = true;
                vezJogador2 = false;
                System.Console.WriteLine("Voce ja pediu truco!");
            }
        }

        static void pedeTruco(string[] grito, int pontuacao, bool trucoBot){
            int pos;
            int escolhaBot;
            Random frase = new Random();
            pos = frase.Next(0, 3);
            Console.WriteLine(grito[pos]);

            vezJogador1 = true;
            vezJogador2 = false;

            if(!trucoBot){
                Random simOuNao = new Random();
                escolhaBot = simOuNao.Next(0, 2);
                respostaBot(escolhaBot, pontuacao);
            } else {
                tento = pontuacao;
                trucoJog = false;
            }
        }

        static void respostaBot(int escolhaBot, int pontuacao){
            int flag;
            if(escolhaBot == 0){
                Console.WriteLine("O BOT correu!");
                pontoJogador1 += tento;
                proximaRodada();
            } else if(escolhaBot == 1){
                Random pedeMais = new Random();
                flag = pedeMais.Next(0, 2);
                tento = pontuacao;
                if(flag == 0){
                    System.Console.WriteLine("\nBOT:\nCAI RATO!");
                } else {
                    System.Console.WriteLine("\nBOT:");
                    truco(true);
                }
            }
        }

        static void getPlacarDaRodada() {
            if(cartaJogador1 != "" && cartaJogador2 != ""){
                if (getPeso(cartaJogador1) > getPeso(cartaJogador2)) {
                    tentoJogador++;
                    vezJogador1 = true;
                    vezJogador2 = false;
                }
                else if (getPeso(cartaJogador1) < getPeso(cartaJogador2)) {
                    tentoBot++;
                    vezJogador1 = false;
                    vezJogador2 = true;
                } else if(getPeso(cartaJogador1) == getPeso(cartaJogador2)){
                    tentoBot++;
                    tentoJogador++;
                }

                cartaJogador1 = "";
                cartaJogador2 = "";
            }

        }

        static void setTento(){
            if(tentoJogador == 2){
                pontoJogador1+=tento;
            } else if(tentoBot == 2) {
                pontoJogador2+=tento;
            }
            Console.WriteLine("\tPlacar:\nJogador 1: " + pontoJogador1 + "\nBot: " + pontoJogador2+"\n");
        }

        static void mostraCartasJogador() {
            for (int pos = 0; pos < jogador1.Length; pos++) {
                if (escolha[pos] == 0) {
                    Console.WriteLine("Carta (" + (pos + 1) + ") do jogador 1 = " + jogador1[pos]);
                }
            }
            Console.WriteLine("\n");
        }

        static void jogadaJogador() {
            int jogadas = 0;
            int numcarta = 0;
            int i = 0;

            rodada++;
            while (i == 0) {
                Console.WriteLine("Digite o número da carta que você quer jogar:");
                numcarta = int.Parse(Console.ReadLine());
                if (numcarta <= escolha.Length) {
                    if (escolha[numcarta - 1] == 0) {
                        Console.WriteLine("Jogador 1 jogou a carta: " + jogador1[numcarta - 1]);
                        cartaJogador1 = jogador1[numcarta - 1];
                        i = 1;
                        escolha[numcarta - 1] = numcarta;
                        jogadas++;
                    }
                    else {
                        Console.WriteLine("Carta inválida, digite novamente");
                        for (int pos = 0; pos < jogador1.Length; pos++) {
                            if (escolha[pos] == 0) {
                                Console.WriteLine("Carta" + (pos + 1) + " do jogador 1 = " + jogador1[pos]);
                            }
                        }
                    }
                }
                else {
                    Console.WriteLine("Número de carta inválido, digite novamente");
                }
            }
        }

        static void jogadaBot() {
            int pos;
            rodada++;
            Random valorAleatorio = new Random();
            if (cartaJogador1 != "") {
                cartaJogador2 = testaCartaMaior();
            } else {
                do{
                    pos = valorAleatorio.Next(0, 3);
                } while (jogador2[pos] == "");
                
                cartaJogador2 = jogador2[pos];
                jogador2[pos] = "";
            }
            vezJogador1 = true;
            vezJogador2 = false;
            Console.WriteLine("O BOT jogou a carta: " + cartaJogador2+"\n");
        }

        static string testaCartaMaior() {
            string aux;
            string jogada;
            string[] cartasAux = new string[3];

            for (int i = 0; i < jogador2.Length; i++) {
                if(jogador2[i] != "") {
                    if (getPeso(jogador2[i]) > getPeso(cartaJogador1)) {
                        jogada = jogador2[i];
                        jogador2[i] = "";
                        return jogada;
                    }
                }
            }

            cartasAux = jogador2;

            for (int j = 0; j < jogador2.Length; j++) {
                for (int k = j+1; k < jogador2.Length; k++) {
                    if (getPeso(cartasAux[j]) > getPeso(cartasAux[k])) {
                        aux = cartasAux[j];
                        cartasAux[j] = cartasAux[k];
                        cartasAux[k] = aux;
                    }
                }
            }

            jogada = cartasAux[0];
            limpaValorBot(jogada);
            return jogada;
        }

        static void limpaValorBot(string cartaJogada){
            for(int i = 0 ; i< jogador2.Length ; i++){
                if(jogador2[i] == cartaJogada){
                    jogador2[i] = "";
                }
            }
        }

        static void carregando(int time) {
            int slepTime = 100;
            int vezes = time / slepTime;
            int porcentagem = 100 / vezes;
            int carregado = 0;

            Console.Clear();

            string vazio = "                                                                                                    ";
            Console.WriteLine("[" + vazio + "] 0%");
            System.Threading.Thread.Sleep(slepTime);

            for(int i = 0; i < vezes; i++) {
                string tracos = "";
                carregado += porcentagem;
                if(carregado > 100) carregado = 100;
                for(int j = 0; j < carregado; j++) {
                    tracos += "-";
                }
                tracos += vazio.Substring(0, 100 - carregado);
                
                Console.Clear();
                Console.WriteLine("[" + tracos + "]" + carregado.ToString() + "%");

                System.Threading.Thread.Sleep(slepTime);
                
            } 
           
        }

    }
}
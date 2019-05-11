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
                Console.WriteLine("Carta" + (pos + 1) + "Do jogador 1 = "+ jogador1[pos]);
                Console.WriteLine("Carta" + (pos + 1) + "Do jogador 2 = "+ jogador2[pos]);
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

    }
}
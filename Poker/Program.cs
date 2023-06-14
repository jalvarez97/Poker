using System;
using System.Collections.Generic;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool bJugando = true;                       

            Console.WriteLine("Introduce el numero de jugadores:");
            while (VariablesGlobales.NumeroJugadores == 1 || VariablesGlobales.NumeroJugadores > 5)
            {
                while (!int.TryParse(Console.ReadLine(), out VariablesGlobales.NumeroJugadores))
                    Console.WriteLine(" Debes introducir un número.");

                if (VariablesGlobales.NumeroJugadores == 1 || VariablesGlobales.NumeroJugadores > 5)
                {
                    Console.WriteLine(" Numero de jugadores invalido se esperan entre 2 y 5 jugadores");
                }
            }          

            while (bJugando)
            {
                Console.Clear();
                Baraja baraja = new Baraja();
                baraja.Barajar();

                //Repartirmos la mano de cada jugador
                Mano oMano = new Mano(baraja.Mazo, 0);
                List<Mano> lstJugadores = oMano.RepartirManosJugadores(baraja);

                oMano.MostrarManos(lstJugadores,"");               

                oMano.MostrarManoGanadora(lstJugadores);               
                
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    bJugando = false;
                    Console.Clear();                    
                }
            }                
        }
    }
}

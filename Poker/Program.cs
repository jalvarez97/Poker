using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool bJugando = true;
            int nJugadoresSinCarta = 0;

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

            Baraja baraja = new Baraja();
            baraja.Barajar();
            
            List<Mano> lstJugadores = new List<Mano>();
            for (int i = 1; i <= VariablesGlobales.NumeroJugadores; i++)
            {
                lstJugadores.Add(baraja.PedirMano(i));
            }

            while (bJugando)
            {
                Console.Clear();
                foreach (Mano i in lstJugadores) {
                    Console.WriteLine("___________________________________________");
                    Console.WriteLine(" Jugador " + i.Cartas[0].Jugador);
                    Console.WriteLine("     Cartas en mano: ");
                    i.Mostrar();
                    Console.WriteLine(" ");
                }
                Console.WriteLine("___________________________________________");
                Console.ReadKey();
            }
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Poker
{
    internal class Mano
    {
        public List<Carta> Cartas = new List<Carta>();
        public int Jugador { get; set; }

        public Mano(List<Carta> cartas, int nJugador)
        {
            Cartas = cartas;
            Jugador = nJugador; 
        }

        public void Mostrar()
        {            
            Console.Write("        ");
            foreach (Carta carta in Cartas)
            {
                Thread.Sleep(125);
                Console.Write("|" + carta + "| ");
                Thread.Sleep(125);
            }           
        }

        public static bool operator >(Mano a, Mano b)
        {
            return true;
        }

        public static bool operator <(Mano a, Mano b)
        {
            return true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Poker.ManoJugada;
using static Poker.VariablesGlobales;

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
                List<Mano> lstJugadores = new List<Mano>();
                for (int i = 1; i <= VariablesGlobales.NumeroJugadores; i++)
                {
                    lstJugadores.Add(baraja.PedirMano(i));
                }
                MostrarManos(lstJugadores,"");

                Thread.Sleep(500);
                Console.WriteLine("");
                Console.WriteLine("Pulse cualquier tecla para ver la mano ganadora. . .");
                Console.ReadKey();

                MostrarManoGanadora(lstJugadores);
                Thread.Sleep(500);
                Console.WriteLine("");
                Console.WriteLine("Pulsa cualquier tecla para volver a jugar. . .");
                Console.WriteLine("Pulsa ESC para salir. . .");                
                
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    bJugando = false;
                    Console.Clear();                    
                }
            }                
        }
        static void MostrarManos(List <Mano> lstJugadores, String sGanador)
        {            
            foreach (Mano i in lstJugadores)
            {
                Console.WriteLine("___________________________________________");
                Console.WriteLine(sGanador +" Jugador " + i.Jugador);
                Console.WriteLine("     Cartas en mano: ");
                i.Mostrar();               
                Console.WriteLine(" ");
                Console.WriteLine("     " + ObtenerJugada(i));
            }
            Console.WriteLine("___________________________________________");
        }

        static Jugadas ObtenerJugada(Mano oMano) 
        {
            ManoJugada oManoJugada = new ManoJugada(oMano.Cartas);
            Jugadas oJugada = oManoJugada.EvaluarMano();            

            return oJugada;
        }

        static Mano ObtenerManoGanadora(Mano oMano1, Mano oMano2)
        {
            Mano oDevuelveMano = null;
            ManoJugada oManoJugada = new ManoJugada(oMano1.Cartas);
            ManoJugada oManoJugada2 = new ManoJugada(oMano2.Cartas);
            
            Jugadas oJugada = oManoJugada.EvaluarMano();
            Jugadas oJugada2 = oManoJugada2.EvaluarMano();

            //Evaluamos jugadas
            if (oJugada > oJugada2)
            {
                oDevuelveMano = oMano1;
            }
            else if (oJugada < oJugada2)
            {
                oDevuelveMano = oMano2;
            }
            else 
            {
                //Si la jugada de las manos es la misma, comparamos los valores
                //Primero comprobamos quien tiene el valor mas alto en mano
                if (oManoJugada.valorMano.Total > oManoJugada2.valorMano.Total)
                {
                    oDevuelveMano = oMano1;
                } 
                else if (oManoJugada.valorMano.Total < oManoJugada2.valorMano.Total)
                {
                    oDevuelveMano = oMano2;
                }
                //si las dos manos son iguales (por ejemplos ambos tienen 2 reinas)
                //entonces el jugador con la siguiente carta mas grande gana
                else if (oManoJugada.valorMano.CartaMasAlta > oManoJugada2.valorMano.CartaMasAlta)
                {
                    oDevuelveMano = oMano1;
                }
                else if (oManoJugada.valorMano.CartaMasAlta < oManoJugada2.valorMano.CartaMasAlta)
                {
                    oDevuelveMano = oMano2;
                }
                else 
                {
                    //Es empate devolvemos cualquiera de las dos manos
                    oDevuelveMano = oMano1;
                }
            }           

            return oDevuelveMano;
        }

        static void MostrarManoGanadora(List<Mano> lstJugadores)
        {
            Mano oManoGanadora = lstJugadores[0];
            for (int i = 0; i < VariablesGlobales.NumeroJugadores; i++)
            {
                oManoGanadora = ObtenerManoGanadora(oManoGanadora, lstJugadores[i]);
            }
            List<Mano> lstManoGanadora = new List<Mano>();
            lstManoGanadora.Add(oManoGanadora);
            MostrarManos(lstManoGanadora, "Ganador");            
        }

    }
}

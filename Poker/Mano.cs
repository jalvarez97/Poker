using System;
using System.Collections.Generic;
using System.Threading;
using static Poker.ManoJugada;

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

        public List<Mano> RepartirManosJugadores(Baraja baraja)
        {
            List<Mano> lstJugadores = new List<Mano>();
            for (int i = 1; i <= VariablesGlobales.NumeroJugadores; i++)
            {
                lstJugadores.Add(baraja.PedirMano(i));
            }
            return lstJugadores;
        }

        public void MostrarManos(List<Mano> lstJugadores, String sGanador)
        {
            foreach (Mano i in lstJugadores)
            {
                Console.WriteLine("___________________________________________");
                Console.WriteLine(sGanador + " Jugador " + i.Jugador);
                Console.WriteLine("     Cartas en mano: ");
                i.Mostrar();
                Console.WriteLine(" ");
                Console.WriteLine("     " + ObtenerJugada(i));
            }
            Console.WriteLine("___________________________________________");
            if (sGanador.Equals(""))
            {
                Thread.Sleep(500);
                Console.WriteLine("");
                Console.WriteLine("Pulse cualquier tecla para ver la mano ganadora. . .");
                Console.ReadKey();
            }            
        }

        public Jugadas ObtenerJugada(Mano oMano)
        {
            ManoJugada oManoJugada = new ManoJugada(oMano.Cartas);
            Jugadas oJugada = oManoJugada.EvaluarMano();

            return oJugada;
        }

        public void MostrarManoGanadora(List<Mano> lstJugadores)
        {
            Mano oManoGanadora = lstJugadores[0];
            for (int i = 0; i < VariablesGlobales.NumeroJugadores; i++)
            {
                oManoGanadora = ObtenerManoGanadora(oManoGanadora, lstJugadores[i]);
            }
            List<Mano> lstManoGanadora = new List<Mano>();
            lstManoGanadora.Add(oManoGanadora);
            oManoGanadora.MostrarManos(lstManoGanadora, "Ganador");
            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Pulsa cualquier tecla para volver a jugar. . .");
            Console.WriteLine("Pulsa ESC para salir. . .");
        }

        public Mano ObtenerManoGanadora(Mano oMano1, Mano oMano2)
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
       
    }
}

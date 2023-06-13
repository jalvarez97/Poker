using System;
using System.Collections.Generic;
using System.Linq;
using static Poker.VariablesGlobales;

namespace Poker
{
    internal class Baraja
    {

        private List<Carta> Mazo = new List<Carta>();
        private int nContador = 0;

        public Baraja()
        {           
            for (int nFigura = 0; nFigura <= 3; nFigura++)
            {
                for (int Rango = 0; Rango <= 12; Rango++)
                {
                    Mazo.Add(new Carta(Carta.NumerosPoker(Rango), (Figura)nFigura, Rango + 1));
                }
            }
        }

        public void Mostrar()
        {
            foreach (Carta oCarta in Mazo)
            {
                Console.WriteLine(oCarta);
            }
        }

        public void Barajar()
        {
            Random rnd = new Random();
            for (int nCartaActual = 0; nCartaActual < Mazo.Count; nCartaActual++)
            {
                int nPosicion = rnd.Next(0, Mazo.Count - 1);
                //Guardamos la carta a reemplazar
                Carta oOtraCarta = Mazo[nPosicion];
                //Insertamos la carta actual en una posicion aleatoria
                Mazo[nPosicion] = Mazo[nCartaActual];
                //Ahora insertamos la carta movida a la posicion de la carta actual
                Mazo[nCartaActual] = oOtraCarta;
            }
        }

        public Carta PedirCarta()
        {
            if (nContador >= Mazo.Count)
                return null;

            Carta oCarta = Mazo[nContador];
            nContador++;
            return oCarta;
        }

        public Mano PedirMano(int nJugador)
        {
            List<Carta> lstMano = new List<Carta>();
           

            for (int i = 0; i < 5; i++)
            {
                Carta oCartaActual = PedirCarta();
                if (oCartaActual != null)
                    lstMano.Add(oCartaActual);
            }
            //Ordenamos las cartas
            List<Carta> lstManoOrdenada = lstMano.OrderBy(x => x.Rango).ToList();

            return new Mano(lstManoOrdenada, nJugador);
        }       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    internal class VariablesGlobales
    {
        public enum Figura { Diamantes, Corazones, Treboles, Picas }

        public enum Jugadas { Pareja
                            , DoblePareja
                            , Trio
                            , Escalera
                            , Color
                            , FullHouse 
                            , Poker
                            , EscaleraColor
                            , EscaleraReal}

        public struct ValorMano
        {
            public int Total { get; set; }
            public int CartaMasAlta { get; set; }   

        }

        public static int NumeroJugadores = 1;

    }
}

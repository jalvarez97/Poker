using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Poker.VariablesGlobales;

namespace Poker
{
    public enum Jugadas
    {
        Nada
      , Pareja
      , DoblePareja
      , Trio
      , Escalera
      , Color
      , FullHouse
      , Poker
      , EscaleraColor
      , EscaleraReal
    }

    public struct ValorMano
    {
        public int Total { get; set; }
        public int CartaMasAlta { get; set; }

    }

    internal class ManoJugada 
    {
        private int nSumaDiamantes;
        private int nSumaCorazones;
        private int nSumaTreboles;
        private int nSumaPicas;
        private List<Carta> lstCartas;
        private ValorMano valorMano;

        public ManoJugada(List<Carta> manoJugador) 
        {
            nSumaDiamantes = 0;
            nSumaCorazones = 0;
            nSumaTreboles = 0;
            nSumaPicas = 0;
            lstCartas = manoJugador;
            valorMano = new ValorMano();
        }

        public Jugadas EvaluarMano()
        {
            //obtenemos el número de cada palo
            ObtenerNumeroPalo();
            if (EsPoker())
                return Jugadas.Poker;
            else if (EsFullHouse())            
                return Jugadas.FullHouse;            
            else if (EsColor())            
                return Jugadas.Color;            
            else if (EsEscalera())            
                return Jugadas.Escalera;            
            else if (EsTrio())            
                return Jugadas.Trio;            
            else if (EsDoblePareja())            
                return Jugadas.DoblePareja;            
            else if (EsPareja())            
                return Jugadas.Pareja;

            //Si la mano no devuelve nada, el jugador con la carta mas alta gana.
            valorMano.CartaMasAlta = lstCartas[4].Rango;
            return Jugadas.Nada;
            
        }

        private void ObtenerNumeroPalo()
        {
            foreach (Carta oCarta in lstCartas)
            {
                switch (oCarta.Palo)
                {
                    case Figura.Diamantes:
                        nSumaDiamantes++;
                        break;
                    case Figura.Corazones:
                        nSumaCorazones++;
                        break;
                    case Figura.Treboles:
                        nSumaTreboles++;
                        break;
                    case Figura.Picas:
                        nSumaPicas++;
                        break;
                    default:
                        break;
                }                   
            }
        }

        private bool EsPoker()
        {
            if (lstCartas[0].Rango == lstCartas[1].Rango
                && lstCartas[0].Rango == lstCartas[2].Rango
                && lstCartas[0].Rango == lstCartas[3].Rango)
            {
                valorMano.Total = lstCartas[1].Rango * 4;
                valorMano.Total = lstCartas[4].Rango;
                return true;
            }
            else if(lstCartas[1].Rango == lstCartas[2].Rango
                    && lstCartas[1].Rango == lstCartas[3].Rango
                    && lstCartas[1].Rango == lstCartas[4].Rango)
            {
                valorMano.Total = lstCartas[1].Rango * 4;
                valorMano.Total = lstCartas[0].Rango;
                return true;
            }

            return false;
        }

        private bool EsFullHouse()
        {
            //Miramos si las tres primeras cartas son iguales
            //y comprobamos que las dos restantes sean iguales
            //Tambien Comprobamos a la inversa
            if(lstCartas[0].Rango == lstCartas[1].Rango
                && lstCartas[0].Rango == lstCartas[2].Rango
                && lstCartas[3].Rango == lstCartas[4].Rango
                && lstCartas[0].Rango == lstCartas[1].Rango
                && lstCartas[2].Rango == lstCartas[3].Rango
                && lstCartas[2].Rango == lstCartas[4].Rango)
            {
                valorMano.Total = (lstCartas[0].Rango) + (lstCartas[1].Rango) + (lstCartas[2].Rango)
                                + (lstCartas[3].Rango) + (lstCartas[4].Rango);
                return true;
            }
            return false;
        }

        private bool EsColor() 
        {
            //Si todos los palos de la mano son iguales devolvemos verdadero
            if(nSumaCorazones == 5 || nSumaDiamantes == 5
                || nSumaTreboles == 5 || nSumaPicas == 5)
            {
                //El jugador con la carta mas alta gana
                valorMano.Total = lstCartas[4].Rango;
                return true;
            }
            return false;
        }

        private bool EsEscalera()
        {
            if (lstCartas[0].Rango + 1 == lstCartas[1].Rango
                && lstCartas[1].Rango + 1 == lstCartas[2].Rango
                && lstCartas[2].Rango + 1 == lstCartas[3].Rango
                && lstCartas[3].Rango + 1 == lstCartas[4].Rango)
            {
                valorMano.Total = lstCartas[4].Rango;
                return true;
            }
            return false;
        }

        private bool EsTrio() 
        {
            //Si las cartas 1,2,3 son iguales o las cartas 2,3,4 son iguales
            //la 3 carta siempre formara parte del trio
            if ((lstCartas[0].Rango == lstCartas[1].Rango && lstCartas[0].Rango == lstCartas[2].Rango)
                || (lstCartas[1].Rango == lstCartas[2].Rango && lstCartas[1].Rango == lstCartas[3].Rango)) 
            {
                valorMano.Total = lstCartas[2].Rango * 3;
                valorMano.CartaMasAlta = lstCartas[4].Rango;
                return true;
            }
            else if ((lstCartas[2].Rango == lstCartas[3].Rango && lstCartas[2].Rango == lstCartas[4].Rango))
            {
                valorMano.Total = lstCartas[2].Rango * 3;
                valorMano.CartaMasAlta = lstCartas[1].Rango;
                return true;
            }
            return false;
        }

        private bool EsDoblePareja()
        {
            //Si 1,2 son iguales entre ellas y 3,4 son iguales entre ellas
            //Si 1,2 son iguales entre ellas y 4,5 son iguales entre ellas
            //Si 2,3 son iguales entre ellas y 4,5 son iguales entre ellas
            //Con doble pareja la segunda carta siempre forma parte de una pareja
            //y la cuarta carta siempre forma parte de otra pareja
            if (lstCartas[0].Rango == lstCartas[1].Rango
                && lstCartas[2].Rango == lstCartas[3].Rango)
            {
                valorMano.Total = ((lstCartas[1].Rango * 2) + (lstCartas[3].Rango * 2));
                valorMano.CartaMasAlta = lstCartas[4].Rango;
                return true;
            }
            else if (lstCartas[0].Rango == lstCartas[1].Rango
                     && lstCartas[3].Rango == lstCartas[4].Rango) 
            {
                valorMano.Total = ((lstCartas[1].Rango * 2) + (lstCartas[3].Rango * 2));
                valorMano.CartaMasAlta = lstCartas[2].Rango;
                return true;
            }
            else if (lstCartas[1].Rango == lstCartas[2].Rango
                     && lstCartas[3].Rango == lstCartas[4].Rango)
            {
                valorMano.Total = ((lstCartas[1].Rango * 2) + (lstCartas[3].Rango * 2));
                valorMano.CartaMasAlta = lstCartas[0].Rango;
                return true;
            }
            return false;
        }

        private bool EsPareja()
        {
            //Si las cartas 1,2 son iguales -> la ultima carta mas alta gana
            //Si las cartas 2,3 son iguales -> la ultima carta mas alta gana
            //Si las cartas 3,4 son iguales -> la ultima carta mas alta gana
            //Si las cartas 4,5 son iguales -> la 3 carta con el valor mas alto gana.

            if (lstCartas[0].Rango == lstCartas[1].Rango)
            {
                valorMano.Total = lstCartas[0].Rango * 2;
                valorMano.CartaMasAlta = lstCartas[4].Rango;
                return true;
            }
            else if (lstCartas[1].Rango == lstCartas[2].Rango)
            {
                valorMano.Total = lstCartas[1].Rango * 2;
                valorMano.CartaMasAlta = lstCartas[4].Rango;
                return true;
            }
            else if (lstCartas[2].Rango == lstCartas[3].Rango)
            {
                valorMano.Total = lstCartas[2].Rango * 2;
                valorMano.CartaMasAlta = lstCartas[4].Rango;
                return true;
            }
            else if (lstCartas[3].Rango == lstCartas[4].Rango)
            {
                valorMano.Total = lstCartas[3].Rango * 2;
                valorMano.CartaMasAlta = lstCartas[2].Rango;
                return true;
            }
            return false;
        }
    }
}

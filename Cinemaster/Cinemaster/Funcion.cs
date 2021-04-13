using System;
using System.Collections.Generic;


namespace Cinemaster
{

    public class Funcion
    {
        public Pelicula Pelicula;
        public Sala Sala;
        public DateTime FechaHora;
        public Dictionary<Asiento, EstadoAsiento> EstadoAsientos;

        public Funcion(Pelicula peli, Sala sala, DateTime fechahora)
        {
            this.Pelicula = peli;
            this.Sala = sala;
            this.FechaHora = fechahora;
            Dictionary<Asiento, EstadoAsiento> estadoAsientos = new Dictionary<Asiento, EstadoAsiento>();
            this.EstadoAsientos = estadoAsientos;

            for(int f = 0; f < sala.Asientos.GetLength(0); f++)
            {
                for (int c = 0; c < sala.Asientos.GetLength(1); c++)
                {
                    estadoAsientos.Add(sala.Asientos[f, c], EstadoAsiento.Libre);
                }
            }             
        }

        public bool IntentarOcuparAsiento(Asiento input)
        {
            if (EstadoAsientos.ContainsKey(input) && EstadoAsientos[input] == EstadoAsiento.Libre)
            {
                EstadoAsientos[input] = EstadoAsiento.Ocupado;
                return true;
            }
            else if (EstadoAsientos[input] == EstadoAsiento.Ocupado)
            {
                return false;
            }            
            return false;
        }
    }
}
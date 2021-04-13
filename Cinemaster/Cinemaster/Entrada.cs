using System;

namespace Cinemaster
{
    public class Entrada
    {
        public Asiento Asiento;
        public Funcion Funcion;
        public int Precio;
        public DateTime FechaEmision;

        public Entrada(Funcion func, Asiento asiento)
        {
            this.FechaEmision = DateTime.Now;
            this.Funcion = func;
            this.Asiento = asiento;
        }
    }
}
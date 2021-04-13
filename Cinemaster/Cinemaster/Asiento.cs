using System;

namespace Cinemaster
{
    public class Asiento
    {
        public int Fila;
        public int Columna;
        public bool EsVip;

        public Asiento(int fila, int col)
        {
            this.Fila = fila;
            this.Columna = col;
        }
    }
}



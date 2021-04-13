using System;

namespace Cinemaster
{
    public class Sala
    {
     
        public int Numero;
        public Asiento[,] Asientos;

        public Sala(int num, int fila, int columna)
        {
            this.Numero = num;
            this.Asientos = new Asiento[fila, columna];

            for (int f = 0; f < Asientos.GetLength(0); f++)
            {
                for (int c = 0; c < Asientos.GetLength(1); c++)
                {
                    Asientos[f, c] = new Asiento(f, c);
                }
            }
        }
    }

}


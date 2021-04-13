using System;
using System.Collections.Generic;

namespace Cinemaster
{
    public class Cine
    {
        public string Nombre;
        public List<Pelicula> Peliculas;
        public List<Sala> Salas;
        public List<Funcion> Funciones;
        public List<Entrada> Entradas;
        public int PrecioEntrada;
        public int PrecioEntradaVIP;

        public Cine(string nombre, int precioE, int precioVIP, List<Pelicula> pelis, List<Sala> salas, List<Funcion> func, List<Entrada> entradas)
        {
            this.Nombre = nombre;
            this.PrecioEntrada = precioE;
            this.PrecioEntradaVIP = precioVIP;
            this.Peliculas = pelis;
            this.Salas = salas;
            this.Funciones = func;
            this.Entradas = entradas;
        }
        public List<Funcion> BuscarFuncion(Pelicula peli)
        {
            if (peli == null)
            {
                throw new ArgumentException("La película no puede ser null.");
            }

            List<Funcion> funcionesDisponibles = new List<Funcion>();

            foreach (Funcion funcion in this.Funciones)
            {                                
                if (funcion.EstadoAsientos.ContainsValue(EstadoAsiento.Libre))
                {
                    if (funcion.Pelicula.Titulo == peli.Titulo)
                    {
                        funcionesDisponibles.Add(funcion);
                    }
                }
                else
                {
                    funcionesDisponibles.Clear();
                }
            }
            return funcionesDisponibles;
        }
    }
}
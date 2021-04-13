using System;
using System.Collections.Generic;

namespace Cinemaster
{

    public class Pelicula
    {
        public string Titulo;
        public string TituloOriginal;
        public Persona Director;
        public Dictionary<Persona, string> Reparto;
        public TimeSpan Duracion;
        public string Sinopsis;
    
        public Pelicula(string titulo, string titOriginal, Persona dir, Dictionary<Persona, string> rep, TimeSpan largo, string sinopsis)
        {
            this.Titulo = titulo;
            this.TituloOriginal = titOriginal;
            this.Director = dir;
            this.Reparto = rep;
            this.Duracion = largo;
            this.Sinopsis = sinopsis;
        }
    }

}

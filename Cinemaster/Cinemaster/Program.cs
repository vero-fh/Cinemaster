using System;
using System.Collections.Generic;


namespace Cinemaster
{
    public enum EstadoAsiento
    {
        Libre,
        Ocupado,
    }

    public class Program
    {
        public static int[] PedirCoordenadas(string input, Funcion funcElegida)
        {
            if (input == null)
            {
                throw new ArgumentNullException("La coordenada ingresada no puede ser nula.");
            }
            string[] Arr = input.Split(',');
            int[] asiento = new int[Arr.Length];

            if (input.Contains(",") && input.Length == 3)
            {

                for (int i = 0; i < Arr.Length; i++)
                {
                    asiento[i] = int.Parse(Arr[i]);
                }
            }
            if (asiento[0] < 0 || asiento[1] < 0 || asiento[0] > funcElegida. Sala.Asientos.GetLength(0) || asiento[1] > funcElegida.Sala.Asientos.GetLength(1))
            {
                throw new ArgumentOutOfRangeException("El asiento no fue encontrado en la sala seleccionada.");
            }
            return asiento;
        }
        public static void MostrarEntrada(Cine CineMaster)
        {
            Console.Clear();

            foreach (Entrada entrada in CineMaster.Entradas)
            {
                entrada.FechaEmision = DateTime.Now;

                string saludo = "| Gracias por elegirnos, disfrute su película!";
                int cantChars = saludo.Length * 2;
                string bordes = new string('_', cantChars);

                int largoSala = entrada.Funcion.Sala.Asientos.GetLength(0) - 1;


                if (entrada.Asiento.Fila == largoSala)
                {
                    entrada.Asiento.EsVip = true;

                    Console.WriteLine($" {bordes}");
                    Console.WriteLine($"| Entrada # {CineMaster.Entradas.IndexOf(entrada) + 1}");
                    Console.WriteLine($"| Película: {entrada.Funcion.Pelicula.Titulo} ({entrada.Funcion.Pelicula.TituloOriginal}).");
                    Console.WriteLine($"| Horario: {entrada.Funcion.FechaHora}.  Duración: {entrada.Funcion.Pelicula.Duracion}.");
                    Console.WriteLine($"| Sala: {entrada.Funcion.Sala.Numero}.  Asiento(s): {entrada.Asiento.Fila}, {entrada.Asiento.Columna}.  VIP: Si.");
                    Console.WriteLine($"| Valor Entrada: {CineMaster.PrecioEntradaVIP}");
                    Console.WriteLine($"| Fecha de emisión: {entrada.FechaEmision}");
                    Console.WriteLine(saludo);
                    Console.WriteLine($"|{bordes}");
                }
                else
                {
                    Console.WriteLine($" {bordes}");
                    Console.WriteLine($"| Entrada # {CineMaster.Entradas.IndexOf(entrada)}");
                    Console.WriteLine($"| Película: {entrada.Funcion.Pelicula.Titulo} ({entrada.Funcion.Pelicula.TituloOriginal}).");
                    Console.WriteLine($"| Horario: {entrada.Funcion.FechaHora}.  Duración: {entrada.Funcion.Pelicula.Duracion}.");
                    Console.WriteLine($"| Sala: {entrada.Funcion.Sala.Numero}.  Asiento(s): {entrada.Asiento.Fila}, {entrada.Asiento.Columna}.  VIP: No.");
                    Console.WriteLine($"| Valor Entrada: {CineMaster.PrecioEntrada}");
                    Console.WriteLine($"| Fecha de emisión: {entrada.FechaEmision}");
                    Console.WriteLine("| Gracias por elegirnos, disfrute su película!");
                    Console.WriteLine($"|{bordes}");
                }
            }
            return;
        }
        public static void ConfirmarCompra(Funcion funcElegida, Cine CineMaster)
        {
            Console.Clear();
            ImprimirAsientos(funcElegida);

            Console.WriteLine($"Película: {funcElegida.Pelicula.Titulo}");
            Console.WriteLine($"Función: {funcElegida.FechaHora}");

            CineMaster.PrecioEntrada = 200;
            CineMaster.PrecioEntradaVIP = 350;

            int cantEntradas = CineMaster.Entradas.Count;
            int precioFinal = 0;

            foreach (Entrada item in CineMaster.Entradas)
            {
                if (item.Asiento.Fila == item.Funcion.Sala.Asientos.GetLength(0) - 1)
                {
                    precioFinal += CineMaster.PrecioEntradaVIP;
                }
                else
                {
                    precioFinal += CineMaster.PrecioEntrada;
                }
            }

            Console.WriteLine($"Usted seleccionó {cantEntradas} entradas. Total a pagar: {precioFinal}");
            
            Console.WriteLine("1- Finalizar compra.\n2- Cancelar compra.");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Compra confirmada. Presione enter para ver el detalle de su entrada:");
                    Console.ResetColor();
                    Console.ReadLine();
                    MostrarEntrada(CineMaster); 
                    break;
                case 2:
                    return;
            }
        }
        public static void ImprimirAsientos(Funcion funcElegida)
        {
            int cantChars = ((funcElegida.Sala.Asientos.GetLength(1) * 3) - 8) / 2;
            string bordes = new string('=', cantChars);
            string pantalla;
            if (cantChars % 2 != 0)
            {
                pantalla = bordes + " PANTALLA " + bordes + "=";
            }
            else
            {
                pantalla = bordes + " PANTALLA " + bordes;
            }

            Console.WriteLine(pantalla);

            Console.Write("  ");
            for (int i = 0; i < funcElegida.Sala.Asientos.GetLength(1); i++)
            {
                Console.Write($" {i } ");
            }
            Console.WriteLine();
            for (int f = 0; f < funcElegida.Sala.Asientos.GetLength(0); f++)
            {
                Console.Write($"{f } ");
                for (int c = 0; c < funcElegida.Sala.Asientos.GetLength(1); c++)
                {
                    EstadoAsiento estado;
                    funcElegida.EstadoAsientos.TryGetValue(funcElegida.Sala.Asientos[f, c], out estado);
                    if (f == funcElegida.Sala.Asientos.GetLength(0) - 1 && estado == EstadoAsiento.Libre)
                    {
                        Console.Write("[V]");
                    }
                    else if (estado == EstadoAsiento.Libre)
                    {
                        Console.Write("[ ]");
                    }
                    else if (estado == EstadoAsiento.Ocupado)
                    {
                        Console.Write("[X]");
                    }
                }
                Console.WriteLine();
            }
        }        
        public static void SeleccionAsiento(Funcion funcElegida, Cine CineMaster)
        {
            Console.Clear();
            Console.WriteLine($"Película: {funcElegida.Pelicula.Titulo}");
            Console.WriteLine($"Función: {funcElegida.FechaHora}");
            
            ImprimirAsientos(funcElegida);

            Console.WriteLine("\nReferencias:\n[ ] = Libre $200(precio promo)\n[V] = VIP Libre $350(precio promo)\n[O] = Ocupado");

            Console.WriteLine("Cuántas entradas desea comprar? (Máximo 4 por compra)");

            short nroEntradas = 0;
            bool esValido = short.TryParse(Console.ReadLine(), out nroEntradas);

            while (esValido == false || nroEntradas < 0 || nroEntradas > 4)
            {
                Console.Write("Cantidad inválida. Intente de nuevo: ");
                esValido = short.TryParse(Console.ReadLine(), out nroEntradas);
            }

            List<Entrada> entradasCompradas = new List<Entrada>();

            for (int i = 0; i < nroEntradas; i++)
            {
                Console.WriteLine($"Su elección para entrada #{i + 1} [ingrese fila y columna en formato (x,y), ingrese 'salir' para cancelar la compra]:");

                string input = Console.ReadLine();

                if (input.ToLower() == "salir")
                {
                    return;
                }

                int[] coordenadaAsiento = PedirCoordenadas(input, funcElegida);

                Asiento asientoElegido = funcElegida.Sala.Asientos[coordenadaAsiento[0], coordenadaAsiento[1]];

                while (funcElegida.EstadoAsientos[asientoElegido] == EstadoAsiento.Ocupado)
                {
                    Console.WriteLine("El asiento seleccionado ya está ocupado. Presione enter para volver a intentar.");

                    Console.WriteLine($"Su elección para entrada #{i + 1} [ingrese fila y columna en formato (x,y), ingrese 'salir' para cancelar la compra]:");

                    input = Console.ReadLine();

                    if (input.ToLower() == "salir")
                    {
                        return;
                    }

                    coordenadaAsiento = PedirCoordenadas(input, funcElegida);

                    asientoElegido = funcElegida.Sala.Asientos[coordenadaAsiento[0], coordenadaAsiento[1]];
                }

                entradasCompradas.Add(new Entrada(funcElegida, asientoElegido));
                
            }

            foreach (Entrada ent in entradasCompradas)
            {
                funcElegida.IntentarOcuparAsiento(ent.Asiento);
                CineMaster.Entradas.Add(ent);
            }

            ConfirmarCompra(funcElegida ,CineMaster);
        }
        public static void SeleccionFuncion(Pelicula peliElegida, Cine CineMaster)
        {
            Console.Clear();
            
            int index = 0;

            List<Funcion> funcionesDisponibles = CineMaster.BuscarFuncion(peliElegida);

            Console.WriteLine($"Título: {peliElegida.Titulo}\n");
            Console.WriteLine($"Título Original: {peliElegida.TituloOriginal}\n");
            Console.WriteLine($"Dirigida por: {peliElegida.Director.Nombre} {peliElegida.Director.Nombre}\n");
            Console.WriteLine("Reparto: ");
            foreach (KeyValuePair<Persona, string> kvp in peliElegida.Reparto)
            {
                Console.WriteLine($"{kvp.Key.Nombre} {kvp.Key.Apellido}, {kvp.Value}");
            }
            Console.WriteLine($"\nDuración: {peliElegida.Duracion}\n");
            Console.WriteLine($"Sinopsis: {peliElegida.Sinopsis}\n\n");

            Console.WriteLine("Funciones Disponibles:\n");

            if (funcionesDisponibles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("No hay funciones disponibles para esta película. Presione enter para volver al menú principal.");
                Console.ResetColor();
                Console.ReadLine();
                MenuPrincipal(CineMaster);
            }
            else
            {
                foreach (Funcion item in funcionesDisponibles)
                {
                    Console.Write($"{index} - ");
                    Console.WriteLine(item.FechaHora);
                    index++;
                }
                Console.WriteLine($"{index} - Cancelar");

                Console.Write("\nSu elección: ");

                short opElegida = 0;
                bool esValido = short.TryParse(Console.ReadLine(), out opElegida);

                while (esValido == false || opElegida < 0 || opElegida > funcionesDisponibles.Count)
                {
                    Console.Write("Opción inválida. Intente de nuevo: ");
                    esValido = short.TryParse(Console.ReadLine(), out opElegida);
                }

                if (opElegida == funcionesDisponibles.Count)
                {
                    return;
                }

                Funcion funcion = funcionesDisponibles[opElegida];
                SeleccionAsiento(funcion, CineMaster);
                Console.ReadLine();
            }            
        }
        public static void MenuPrincipal(Cine CineMaster)
        {
            Console.Clear();
            
            int index = 0;

            Console.WriteLine("Bienvenidx a CineMaster. Qué película desea ver?");
            foreach (Pelicula item in CineMaster.Peliculas)
            {
                Console.Write($"{index} - ");
                Console.Write($"{item.Titulo} ");
                Console.WriteLine($"({item.TituloOriginal}).");
                index++;
            }
            Console.Write("Su Elección: ");

            short opElegida = 0;
            bool esValido = short.TryParse(Console.ReadLine(), out opElegida);

            while (esValido == false || opElegida < 0 || opElegida > CineMaster.Peliculas.Count - 1)
            {
                Console.Write("Opción inválida. Intente de nuevo: ");
                esValido = short.TryParse(Console.ReadLine(), out opElegida);
            }

            Pelicula peliElegida = CineMaster.Peliculas[opElegida];
            SeleccionFuncion(peliElegida, CineMaster);
        
        }
        static void Main(string[] args)
        {
            
            Dictionary<Persona, string> reparto1 = new Dictionary<Persona, string>();
            reparto1.Add(new Persona("Lily", "Collins"), "como Lauren Monroe.");
            reparto1.Add(new Persona("Simon", "Pegg"), "como Morgan Warner.");
            reparto1.Add(new Persona("Connie", "Nielsen"), "como Catherine Monroe.");

            Dictionary<Persona, string> reparto2 = new Dictionary<Persona, string>();
            reparto2.Add(new Persona("Seth", "Rogen"), "como Herschel Greenbaum / Ben Greenbaum.");
            reparto2.Add(new Persona("Sarah", "Snook"), "como Sarah Greenbaum.");

            Dictionary<Persona, string> reparto3 = new Dictionary<Persona, string>();
            reparto3.Add(new Persona("Lili", "Reinhart"), "como Grace Town.");
            reparto3.Add(new Persona("Austin", "Abrahams"), "como Henry Page.");

            Dictionary<Persona, string> reparto4 = new Dictionary<Persona, string>();
            reparto4.Add(new Persona("Sacha", "Baron Cohen"), "como Borat Sagdiyev.");
            reparto4.Add(new Persona("Maria", "Bakalova"), "como Tutar Sagdijev.");

            List<Pelicula> pelis = new List<Pelicula>();

            pelis.Add(new Pelicula("La Herencia", "Inheritance", new Persona("Vaughn", "Stein"), reparto1, new TimeSpan (1, 51, 0), "A patriarch of a wealthy and powerful family suddenly passes away, leaving his daughter with a shocking secret inheritance that threatens to unravel and destroy the family."));

            pelis.Add(new Pelicula("Encurtido en el Tiempo", "An American Pickle", new Persona("Brandon", "Trost"), reparto2, new TimeSpan(1, 28, 0), "An immigrant worker at a pickle factory is accidentally preserved for 100 years and wakes up in modern-day Brooklyn."));

            pelis.Add(new Pelicula("Efectos Colaterales del Amor", "Chemical Hearts", new Persona("Richard", "Tanne"), reparto3, new TimeSpan(1, 33, 0), "A high school transfer student finds a new passion when she begins to work on the school's newspaper."));

            pelis.Add(new Pelicula("Borat - Siguiente Película Documental", "Borat Subsequent Moviefilm", new Persona("Jason", "Woliner"), reparto4, new TimeSpan(1, 35, 0), "Follow-up film to the 2006 comedy centering on the real-life adventures of a fictional Kazakh television journalist named Borat."));

            Random rnd = new Random();

            List<Sala> salas = new List<Sala>();
            salas.Add(new Sala(rnd.Next(1, 7), 3, 3));
            salas.Add(new Sala(rnd.Next(1, 7), 6, 6));
            salas.Add(new Sala(rnd.Next(1, 7), 8, 5));
            salas.Add(new Sala(rnd.Next(1, 7), 5, 5));
            salas.Add(new Sala(rnd.Next(1, 7), 7, 4));
            salas.Add(new Sala(rnd.Next(1, 7), 8, 4));
            salas.Add(new Sala(rnd.Next(1, 7), 5, 4));

            List<Funcion> funciones = new List<Funcion>();
            
            DateTime func1 = new DateTime(2020, 11, 20, rnd.Next(10, 23), 50, 00);
            DateTime func2 = new DateTime(2020, 11, 21, rnd.Next(10, 23), 30, 00);
            DateTime func3 = new DateTime(2020, 11, 22, rnd.Next(10, 23), 40, 00);

            funciones.Add(new Funcion(pelis[0], salas[0], func1));
            funciones.Add(new Funcion(pelis[0], salas[1], func2));
            funciones.Add(new Funcion(pelis[1], salas[2], func1));
            funciones.Add(new Funcion(pelis[1], salas[3], func3));
            funciones.Add(new Funcion(pelis[2], salas[4], func1));
            funciones.Add(new Funcion(pelis[2], salas[5], func3));
            funciones.Add(new Funcion(pelis[2], salas[6], func2));
            funciones.Add(new Funcion(pelis[3], salas[3], func3));
            funciones.Add(new Funcion(pelis[3], salas[2], func2));
            funciones.Add(new Funcion(pelis[3], salas[4], func1));

            List<Entrada> entradas = new List<Entrada>();

            Cine CineMaster = new Cine("CineMaster", 200, 350, pelis, salas, funciones, entradas);

            while (true)
            {
                MenuPrincipal(CineMaster);
            }
        }
    }
}

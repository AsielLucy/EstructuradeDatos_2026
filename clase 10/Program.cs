using System;

// FASE 3 - Módulo A: El Struct CoordenadaGPS
readonly struct CoordenadaGPS
{
    public double Latitud { get; }
    public double Longitud { get; }

    public CoordenadaGPS(double lat, double lon)
    {
        // Módulo C: Validación en el Constructor
        if (lat < -90 || lat > 90)
            throw new ArgumentOutOfRangeException(nameof(lat), "Latitud fuera de rango [-90, 90]");

        if (lon < -180 || lon > 180)
            throw new ArgumentOutOfRangeException(nameof(lon), "Longitud fuera de rango [-180, 180]");

        Latitud = lat;
        Longitud = lon;
    }

    public void ImprimirUbicacion()
    {
        Console.WriteLine($"Latitud: {Latitud}, Longitud: {Longitud}");
    }
}

class Program
{
    static void Main()
    {
        // Módulo C: Captura en el Main
        try
        {
            Console.WriteLine("--- Ingreso de Nuevas Coordenadas ---");
            Console.Write("Latitud: ");
            double lat = double.Parse(Console.ReadLine()!);

            Console.Write("Longitud: ");
            double lon = double.Parse(Console.ReadLine()!);
            var coord = new CoordenadaGPS(lat, lon);
            coord.ImprimirUbicacion();

            Console.WriteLine("\n--- Módulo B: Prueba de Copia por Valor ---");
            // Ciudad de México
            CoordenadaGPS c1 = new CoordenadaGPS(19.4326, -99.1332);
            
            // Copia por valor en el Stack
            CoordenadaGPS c2 = c1;
            
            // Reasignamos c2 -> Berlín
            c2 = new CoordenadaGPS(52.5200, 13.4050);

            // Imprimimos ambas para demostrar la independencia en memoria
            Console.WriteLine("--- c1 ---");
            c1.ImprimirUbicacion();
            Console.WriteLine("--- c2 ---");
            c2.ImprimirUbicacion();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Captura de errores de rango geográfico
            Console.WriteLine($"Error de Validación: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Por favor, introduce un número válido.");
        }
    }
}
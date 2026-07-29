using System;

class Program
{
    static void Main(string[] args)
    {
        // Módulo 3: Bloque try-catch como banco de pruebas
        try
        {
            // Módulo 1: Inicialización aleatoria
            int[] calificaciones = new int[100];
            Random rng = new Random();

            for (int i = 0; i < calificaciones.Length; i++)
            {
                // Se usa 101 para que el 100 sea inclusivo
                calificaciones[i] = rng.Next(0, 101); 
            }

            Console.WriteLine("=== Estado inicial: calificaciones desordenadas ===");
            ImprimirArreglo(calificaciones);

            // Módulo 2: Llamada al algoritmo de ordenamiento
            OrdenarPorBurbuja(calificaciones);

            Console.WriteLine("\n=== Estado final: calificaciones ordenadas (menor a mayor) ===");
            ImprimirArreglo(calificaciones);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"\n[ERROR] Índice fuera de rango detectado: {ex.Message}");
            Console.WriteLine("Revisa los límites de tus ciclos for anidados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR inesperado]: {ex.Message}");
        }
    }

    // Método auxiliar para imprimir el arreglo
    static void ImprimirArreglo(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    // Módulo 2: Bubble Sort tradicional con contador
    static void OrdenarPorBurbuja(int[] arr)
    {
        int n = arr.Length;
        int contadorIntercambios = 0;

        for (int i = 0; i < n - 1; i++)
        {
            // El límite es n-i-1 para evitar errores de IndexOutOfRangeException
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Intercambio con sintaxis de tuplas moderna de C#
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    contadorIntercambios++;
                }
            }
        }
        
        Console.WriteLine($"\nTotal de intercambios realizados: {contadorIntercambios}");
    }
}
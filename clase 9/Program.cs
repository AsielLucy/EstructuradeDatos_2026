using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Ingresa un número (35-43): ");
        string input = Console.ReadLine();

        // Módulo C: Validación de entrada segura
        if (!int.TryParse(input, out int n) || n < 0)
        {
            Console.WriteLine("Error: ingresa un número positivo.");
            return;
        }

        Stopwatch sw = new Stopwatch();

        // -- Método Inseguro (Fuerza Bruta) --
        sw.Restart();
        long r1 = FibonacciInseguro(n);
        sw.Stop();
        Console.WriteLine($"Inseguro: F({n}) = {r1}");
        Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds} ms");

        // -- Método Pro (Memoization) --
        long[] cache = new long[n + 1];
        
        // Inicializar todo en -1 ("no calculado aún")
        for (int i = 0; i <= n; i++)
        {
            cache[i] = -1; 
        }

        sw.Restart();
        long r2 = FibonacciPro(n, cache);
        sw.Stop();
        Console.WriteLine($"Pro: F({n}) = {r2}");
        Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds} ms");
    }

    // Módulo A: Fibonacci Recursivo Tradicional (Fuerza Bruta)
    public static long FibonacciInseguro(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        
        return FibonacciInseguro(n - 1) + FibonacciInseguro(n - 2);
    }

    // Módulo B: Fibonacci con Memoization (Estrategia Pro)
    public static long FibonacciPro(int n, long[] cache)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;

        // ¿Ya lo calculamos antes? Verifica el caché
        if (cache[n] != -1)
        {
            return cache[n]; // Retorno inmediato en tiempo O(1)
        }

        // Si no existe, lo calcula, lo guarda en el caché y lo retorna
        cache[n] = FibonacciPro(n - 1, cache) + FibonacciPro(n - 2, cache);
        return cache[n];
    }
}
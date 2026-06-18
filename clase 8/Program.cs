using System;
using System.Numerics;

class Program
{
    // --- PARTE A: Implementación Tradicional ---
    
    // 1. Función Recursiva (Falla en n=13)
    static int FactorialInt(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        return n * FactorialInt(n - 1);
    }

    // 2. Función Iterativa (Falla en n=13)
    static int FactorialIterativo(int n)
    {
        int resultado = 1;
        for (int i = 2; i <= n; i++)
        {
            resultado *= i;
        }
        return resultado;
    }

    // --- PARTE B: Refactorización Profesional ---

    // 3. Función Recursiva de Alta Precisión con BigInteger
    static BigInteger FactorialProfesional(BigInteger n)
    {
        // Caso Base
        if (n == 0 || n == 1)
            return BigInteger.One;

        // Caso Recursivo
        return n * FactorialProfesional(n - 1);
    }

    // --- EJECUCIÓN (Main) ---
    static void Main()
    {
        Console.WriteLine("--- Diagnóstico del Punto de Quiebre (int) ---");
        // Ciclo de diagnóstico: Veremos el colapso a partir de n=13
        for (int i = 1; i <= 20; i++)
        {
            Console.WriteLine($"n={i:D2} | Recursivo: {FactorialInt(i),15} | Iterativo: {FactorialIterativo(i),15}");
        }

        Console.WriteLine("\n--- Solución con BigInteger ---");
        // Prueba con un número masivo que es imposible para un int
        BigInteger resultado = FactorialProfesional(100);
        Console.WriteLine($"100! = {resultado}");
    }
}
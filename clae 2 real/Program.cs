using System;

class Program
{
    static void Main()
    {
        // Valores iniciales
        int miNumero = 50;
        int[] miArreglo = { 50, 60, 70 };

        // Imprimir resultados ANTES
        Console.WriteLine("--- ANTES DE LAS FUNCIONES ---");
        Console.WriteLine($"Valor del entero: {miNumero}");
        Console.WriteLine($"Primer elemento del arreglo: {miArreglo[0]}\n");

        // Llamar a ambas funciones
        CambiarValor(miNumero);
        CambiarReferencia(miArreglo);

        // Imprimir resultados DESPUÉS
        Console.WriteLine("--- DESPUÉS DE LAS FUNCIONES ---");
        Console.WriteLine($"Valor del entero: {miNumero} (No cambió)");
        Console.WriteLine($"Primer elemento del arreglo: {miArreglo[0]} (Sí cambió)");
    }

    // 1. Intenta cambiar el valor de un entero a 100
    static void CambiarValor(int x)
    {
        x = 100;
    }

    // 2. Intenta cambiar el primer elemento de un arreglo a 100
    static void CambiarReferencia(int[] arr)
    {
        // Se verifica que el arreglo no esté vacío por seguridad
        if (arr != null && arr.Length > 0)
        {
            arr[0] = 100;
        }
    }
}

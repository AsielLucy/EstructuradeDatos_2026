using System;

public class SimuladorStack
{
    // Ejercicio A: Cuenta Regresiva de Memoria
    public static void ImprimirCuentaRegresiva(int numero)
    {
        // FASE DE APILADO: Caso Base
        if (numero < 1) 
            return; // si numero es menor que 1, la secuencia termina [cite: 235]

        // Mensaje antes de la recursión (Fase de apilado)
        Console.WriteLine($"APILANDO: {numero}"); // [cite: 236]
        
        // Llamada recursiva con el problema reducido
        ImprimirCuentaRegresiva(numero - 1); // [cite: 236]
        
        // FASE DE RETORNO: Se ejecuta después de que la llamada regresa
        Console.WriteLine($"LIBERANDO: {numero}"); // [cite: 239]
    }

    // Ejercicio B: Sumatoria Recursiva Dinámica
    public static int SumarHasta(int n) // [cite: 250]
    {
        // CASO BASE: La suma de 1 número es 1
        if (n == 1) // [cite: 250]
            return 1; // [cite: 250]
            
        // CASO RECURSIVO: n más la suma de todo lo anterior
        return n + SumarHasta(n - 1); // [cite: 251]
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- EJERCICIO A: Simulador Call Stack ---");
        SimuladorStack.ImprimirCuentaRegresiva(3);
        Console.WriteLine("¡Despegue! 🚀"); // [cite: 312, 313]
        
        Console.WriteLine("\n--- EJERCICIO B: Sumatoria Recursiva ---");
        Console.Write("Introduce un número positivo: "); // [cite: 253]
        string? entrada = Console.ReadLine(); // [cite: 253]
        
        // Validación en la Frontera [cite: 221]
        if (int.TryParse(entrada, out int numero) && numero > 0) // [cite: 254]
        {
            int resultado = SimuladorStack.SumarHasta(numero); // [cite: 254]
            Console.WriteLine($"La suma de 1 hasta {numero} es: {resultado}"); // [cite: 255]
        }
        else // [cite: 255]
        {
            Console.ForegroundColor = ConsoleColor.Red; // [cite: 255]
            Console.WriteLine("Error: Solo se aceptan enteros positivos."); // [cite: 256]
            Console.ResetColor(); // [cite: 256]
        }
        // Tu punto de entrada. Aquí pruebas tu estructura de datos.
        Console.WriteLine("Iniciando el programa...");
        
        // Ejemplo:
        // ArbolBST miArbol = new ArbolBST();
        // miArbol.Insertar(50);
    }
}

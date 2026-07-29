using System;

class Program {
    
    // Búsqueda Lineal - O(n)
    static int BusquedaLineal(int[] arr, int objetivo, out int iteraciones) {
        iteraciones = 0;
        for (int i = 0; i < arr.Length; i++) {
            iteraciones++;
            if (arr[i] == objetivo) return i;
        }
        return -1;
    }

    // Búsqueda Binaria - O(log n)
    static int BusquedaBinaria(int[] arr, int objetivo, out int iteraciones) {
        iteraciones = 0;
        int izquierda = 0, derecha = arr.Length - 1;
        
        while (izquierda <= derecha) {
            iteraciones++;
            
            // Variante segura para calcular el centro
            int centro = izquierda + (derecha - izquierda) / 2;
            
            if (arr[centro] == objetivo)
                return centro;
            
            if (arr[centro] < objetivo)
                izquierda = centro + 1;
            else
                derecha = centro - 1;
        }
        
        return -1;
    }

    static void Main() {
        // Generador de Datos (Main)
        int[] matriculas = new int[10000];
        for (int i = 0; i < matriculas.Length; i++) {
            matriculas[i] = i + 1;
        }

        // El Banco de Pruebas e Instrumentación
        Console.Write("Ingresa la matrícula a buscar: ");
        int objetivo = int.Parse(Console.ReadLine()!);
        
        int iterLineal, iterBinaria;
        int idxLineal = BusquedaLineal(matriculas, objetivo, out iterLineal);
        int idxBinaria = BusquedaBinaria(matriculas, objetivo, out iterBinaria);
        
        // Reporte en Consola
        Console.WriteLine("\n=== REPORTE DE BÚSQUEDA ===");
        Console.WriteLine($"Tamaño del arreglo: {matriculas.Length}");
        Console.WriteLine($"Matrícula objetivo: {objetivo}");
        
        if (idxLineal != -1)
            Console.WriteLine($"[Lineal] Encontrado en índice: {idxLineal}");
        else
            Console.WriteLine("[Lineal] No encontrado.");
        Console.WriteLine($"[Lineal] Iteraciones realizadas: {iterLineal}");
        
        if (idxBinaria != -1)
            Console.WriteLine($"[Binaria] Encontrado en índice: {idxBinaria}");
        else
            Console.WriteLine("[Binaria] No encontrado.");
        Console.WriteLine($"[Binaria] Iteraciones realizadas: {iterBinaria}");
        
        Console.WriteLine("\nObservación:");
        Console.WriteLine("La búsqueda lineal puede revisar casi todo el arreglo.");
        Console.WriteLine("La búsqueda binaria aprovecha que los datos están ordenados.");
    }
}
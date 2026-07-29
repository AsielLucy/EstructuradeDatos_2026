using System;
using System.Diagnostics;

// INVARIANTE: No modificar. Fase 1 y Fase 2 operan sobre este mismo struct.
public struct RegistroDatos
{
    public int Id { get; }
    public string HashValidacion { get; }
    public double PesoBytes { get; }

    public RegistroDatos(int id, string hashValidacion, double pesoBytes)
    {
        if (id <= 0)
            throw new ArgumentException("El Id debe ser un entero positivo mayor que cero.", nameof(id));
        
        if (string.IsNullOrEmpty(hashValidacion))
            throw new ArgumentNullException(nameof(hashValidacion), "El HashValidacion no puede ser null ni una cadena vacía.");
        
        if (pesoBytes <= 0)
            throw new ArgumentOutOfRangeException(nameof(pesoBytes), "El PesoBytes debe ser un valor numérico positivo mayor que cero.");

        Id = id;
        HashValidacion = hashValidacion;
        PesoBytes = pesoBytes;
    }

    public override string ToString()
    {
        return $"[Id: {Id} | Hash: {HashValidacion} | Peso: {PesoBytes:F2}]";
    }
}

class Program
{
    // Instrumentación
    static long contadorLlamadas = 0;
    static long contadorComparaciones = 0;
    static long contadorIntercambios = 0;

    static void Main(string[] args)
    {
        int tamaño = 10000;
        RegistroDatos[] arregloOriginal = GenerarArregloAleatorio(tamaño);

        // Clonar para condiciones idénticas
        RegistroDatos[] copiaSeleccion = (RegistroDatos[])arregloOriginal.Clone();
        RegistroDatos[] copiaQuickSort = (RegistroDatos[])arregloOriginal.Clone();

        // --- BENCHMARK 1: Selección (Fase 1) ---
        contadorComparaciones = 0;
        contadorIntercambios = 0;
        Stopwatch swSeleccion = Stopwatch.StartNew();
        OrdenarPorSeleccion(copiaSeleccion);
        swSeleccion.Stop();
        long opSeleccion = contadorComparaciones + contadorIntercambios;
        long msSeleccion = swSeleccion.ElapsedMilliseconds;

        // --- BENCHMARK 2: QuickSort (Fase 2) ---
        contadorLlamadas = 0;
        Stopwatch swQuickSort = Stopwatch.StartNew();
        QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
        swQuickSort.Stop();
        long msQuickSort = swQuickSort.ElapsedMilliseconds;

        // Verificar correctitud
        if (!EstaOrdenado(copiaQuickSort))
        {
            Console.WriteLine("ERROR: El arreglo QuickSort no se ordenó correctamente.");
        }

        // --- REPORTE COMPARATIVO ---
        Console.WriteLine("========== REPORTE COMPARATIVO DE ORDENAMIENTO ==========");
        Console.WriteLine($"Registros procesados (tamaño): {tamaño:N0}");
        Console.WriteLine();
        
        Console.WriteLine("Algoritmo: Selección Directa");
        Console.WriteLine($"Comparaciones: {contadorComparaciones:N0}");
        Console.WriteLine($"Intercambios: {contadorIntercambios:N0}");
        Console.WriteLine($"Tiempo de ejecución: {msSeleccion} ms");
        Console.WriteLine();
        
        Console.WriteLine("Algoritmo: QuickSort");
        Console.WriteLine($"Llamadas recursivas QS: {contadorLlamadas:N0}");
        Console.WriteLine($"Tiempo de ejecución: {msQuickSort} ms");
        Console.WriteLine();
        
        if (msQuickSort > 0)
        {
            Console.WriteLine($"Ratio de velocidad: QuickSort fue {msSeleccion / msQuickSort}x más rápido");
        }
    }

    // Método principal QuickSort recursivo
    public static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        contadorLlamadas++;

        if (bajo < alto)
        {
            int indicePivote = Particionar(arr, bajo, alto);
            QuickSort(arr, bajo, indicePivote - 1);
            QuickSort(arr, indicePivote + 1, alto);
        }
    }

    // Particionado basado en Lomuto con pivote al final
    private static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos pivote = arr[alto];
        int i = bajo - 1;

        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= pivote.Id)
            {
                i++;
                RegistroDatos temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // Intercambio final para ubicar el pivote
        RegistroDatos temp2 = arr[i + 1];
        arr[i + 1] = arr[alto];
        arr[alto] = temp2;

        return i + 1;
    }

    // Método de simulación para generar los datos con semilla
    static RegistroDatos[] GenerarArregloAleatorio(int cantidad)
    {
        Random rnd = new Random(42); 
        RegistroDatos[] arreglo = new RegistroDatos[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = new RegistroDatos(
                id: rnd.Next(1, 100001),
                hashValidacion: Guid.NewGuid().ToString(),
                pesoBytes: 1.0 + rnd.NextDouble() * 9999.0
            );
        }
        return arreglo;
    }

    // Método para validar funcionalidad
    static bool EstaOrdenado(RegistroDatos[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].Id > arr[i + 1].Id)
                return false;
        }
        return true;
    }

    // Ordenamiento burbuja simulando Selección de Fase 1 (Sustituye por tu propio código de Fase 1)
    static void OrdenarPorSeleccion(RegistroDatos[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int min_idx = i;
            for (int j = i + 1; j < n; j++)
            {
                contadorComparaciones++;
                if (arr[j].Id < arr[min_idx].Id)
                {
                    min_idx = j;
                }
            }
            if (min_idx != i)
            {
                contadorIntercambios++;
                RegistroDatos temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }
        }
    }
}

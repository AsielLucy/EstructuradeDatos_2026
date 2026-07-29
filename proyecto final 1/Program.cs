using System;

namespace DataCore
{
    // 1. Estructura RegistroDatos
    public struct RegistroDatos
    {
        public int Id;
        public long HashValidacion;
        public int PesoBytes;

        // Constructor con validación de contrato
        public RegistroDatos(int id, long hash, int pesoBytes)
        {
            if (pesoBytes <= 0)
            {
                throw new ArgumentException(
                    "PesoBytes debe ser mayor a 0. Un registro no puede tener tamaño nulo o negativo.", 
                    nameof(pesoBytes));
            }
            Id = id;
            HashValidacion = hash;
            PesoBytes = pesoBytes;
        }
    }

    class Program
    {
        // 2. El Motor de Ordenación: Selection Sort Instrumentado
        static void OrdenarPorSeleccion(RegistroDatos[] arr)
        {
            int comparaciones = 0;
            int intercambios = 0;
            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int indiceMinimo = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    comparaciones++;
                    if (arr[j].Id < arr[indiceMinimo].Id)
                    {
                        indiceMinimo = j;
                    }
                }
                
                // Intercambio condicional
                if (indiceMinimo != i)
                {
                    // Sintaxis de Tupla moderna de C# sin variable temporal
                    (arr[i], arr[indiceMinimo]) = (arr[indiceMinimo], arr[i]);
                    intercambios++;
                }
            }
            
            Console.WriteLine($"\nComparaciones realizadas: {comparaciones}");
            Console.WriteLine($"Intercambios reales: {intercambios}");
        }

        // 3. Orquestador Base
        static void Main(string[] args)
        {
            var rng = new Random();
            var arreglo = new RegistroDatos[40];
            
            try
            {
                // Generación de 40 Registros
                for (int i = 0; i < arreglo.Length; i++)
                {
                    arreglo[i] = new RegistroDatos(
                        id: rng.Next(1, 1001),
                        hash: rng.NextInt64(),
                        pesoBytes: rng.Next(10, 5001)
                    );
                }

                // Impresión del Estado Inicial
                Console.WriteLine("--- ESTADO INICIAL ---");
                foreach (var r in arreglo)
                {
                    Console.WriteLine($"Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");
                }

                // Ejecución 
                OrdenarPorSeleccion(arreglo);

                // Impresión del Estado Final
                Console.WriteLine("\n--- ESTADO FINAL ORDENADO ---");
                foreach (var r in arreglo)
                {
                    Console.WriteLine($"Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error al crear registro: {ex.Message}");
            }
        }
    }
}
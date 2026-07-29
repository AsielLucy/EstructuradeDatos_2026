using System;

namespace EstructuraDeDatos_2026
{
    // Fase 3 (Parte A): Estructura de Datos Transaccion
    struct Transaccion
    {
        public int Id;             // Identificador único de la transacción
        public double Monto;       // Importe en moneda local
        public long Timestamp;     // Marca de tiempo en milisegundos (epoch)

        // Constructor para facilitar la inicialización de cada registro
        public Transaccion(int id, double monto, long timestamp)
        {
            Id = id;
            Monto = monto;
            Timestamp = timestamp;
        }

        // Override de ToString para visualización legible en consola
        public override string ToString()
        {
            return $"ID: {Id,4} | Monto: {Monto,10:F2} | Timestamp: {Timestamp}";
        }
    }

    class Program
    {
        // Fase 3 (Parte B): El Módulo de Insertion Sort
        static int OrdenarPorInsercion(Transaccion[] arr)
        {
            int contadorDesplazamientos = 0; // Cuenta cuántas veces se mueve un elemento
            int n = arr.Length;              // Guarda la longitud del arreglo una sola vez

            for (int i = 1; i < n; i++)
            {
                // El subarreglo arr[0..i-1] ya está ordenado
                Transaccion clave = arr[i]; // Elemento que se quiere insertar
                int j = i - 1;              // Empieza a comparar desde el elemento anterior

                // Mientras haya elementos mayores que la clave, se desplazan a la derecha
                while (j >= 0 && arr[j].Id > clave.Id)
                {
                    arr[j + 1] = arr[j];       // Abre espacio moviendo el valor hacia la derecha
                    contadorDesplazamientos++; // Registra el costo real de movimiento
                    j--;                       // Avanza hacia la izquierda en la zona ordenada
                }
                
                arr[j + 1] = clave; // Inserta la clave exactamente en su posición
            }
            return contadorDesplazamientos; // Devuelve el total de desplazamientos efectuados
        }

        // Fase 3 (Parte C): Módulo Main y Pruebas de Estrés
        static void Main(string[] args)
        {
            try
            {
                // Se crea la Bitácora con capacidad fija para 50 transacciones.
                // Esto permite controlar el tamaño del escenario de prueba.
                Transaccion[] bitacora = new Transaccion[50];
                Random rng = new Random();

                // Primeros 45 elementos: IDs en orden ascendente.
                // Esta parte simula datos normales y bien estructurados,
                // como transacciones registradas en tiempo real.
                for (int i = 0; i < 45; i++)
                {
                    bitacora[i] = new Transaccion(
                        id: i + 1,
                        monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                        timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + i * 100
                    );
                }

                // Últimos 5 elementos: IDs aleatorios fuera de rango.
                // Representan transacciones tardías, correcciones o registros
                // que llegan después del lote principal.
                int[] idsAleatorios = { 78, 3, 99, 12, 55 };
                for (int i = 0; i < 5; i++)
                {
                    bitacora[45 + i] = new Transaccion(
                        id: idsAleatorios[i],
                        monto: Math.Round(rng.NextDouble() * 9999.99 + 0.01, 2),
                        timestamp: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (45 + i) * 100
                    );
                }

                Console.WriteLine("--- OPTIMIZADOR DE BITÁCORAS DE TRANSACCIONES ---");
                
                // Se ejecuta el algoritmo de inserción instrumentado.
                // El valor retornado permite medir cuántos desplazamientos reales
                // se necesitaron para ordenar la estructura.
                int totalDesplazamientos = OrdenarPorInsercion(bitacora);

                Console.WriteLine("\nTransacciones ordenadas por ID:");
                foreach (var t in bitacora)
                {
                    Console.WriteLine(t.ToString());
                }

                // Se informa el costo total observado.
                // Esto ayuda a comparar el comportamiento real con el peor caso teórico.
                Console.WriteLine($"\nTotal de desplazamientos realizados: {totalDesplazamientos}");
                
                // Se calcula un porcentaje de eficiencia respecto al peor caso.
                // Para 50 elementos, el peor caso de insertion sort es n(n-1)/2.
                Console.WriteLine($"Eficiencia: {((double)totalDesplazamientos / (50 * 49 / 2)) * 100:F1}% mejor que el peor caso");
            }
            catch (OverflowException ex)
            {
                // Captura de errores por desbordamiento numérico.
                // En contextos financieros, esto puede evitar montos o marcas de tiempo inválidas.
                Console.WriteLine($"[ERROR] Desbordamiento de datos: {ex.Message}");
            }
            catch (FormatException ex)
            {
                // Captura de errores de formato si en una versión futura
                // se incorporara lectura de datos desde archivos o consola.
                Console.WriteLine($"[ERROR] Formato de entrada inválido: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Captura genérica para fallos no previstos.
                // Sirve como último nivel de protección para evitar que la aplicación se cierre sin control.
                Console.WriteLine($"[ERROR] Excepción inesperada: {ex.Message}");
            }
        }
    }
}

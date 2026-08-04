using System;

namespace DataCore
{
    /// <summary>
    /// Clase que representa un nodo en la base de datos en memoria.
    /// </summary>
    public class RegistroDatos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public RegistroDatos? Siguiente { get; set; }

        public RegistroDatos(int id, string nombre, string valor)
        {
            Id = id;
            Nombre = nombre;
            Valor = valor;
            Siguiente = null;
        }
    }

    /// <summary>
    /// Estructura de almacenamiento principal (Lista Enlazada Simple).
    /// </summary>
    public class TablaDinamica
    {
        private RegistroDatos? cabeza;
        public int CantidadRegistros { get; private set; }

        public TablaDinamica()
        {
            cabeza = null;
            CantidadRegistros = 0;
        }

        /// <summary>
        /// Inserta un nuevo registro al final de la lista.
        /// </summary>
        public void Insertar(int id, string nombre, string valor)
        {
            RegistroDatos nuevo = new RegistroDatos(id, nombre, valor);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                RegistroDatos actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            CantidadRegistros++;
        }

        /// <summary>
        /// Elimina un registro por su ID.
        /// </summary>
        public bool Eliminar(int id)
        {
            if (cabeza == null) throw new InvalidOperationException("La tabla está vacía.");

            if (cabeza.Id == id)
            {
                cabeza = cabeza.Siguiente;
                CantidadRegistros--;
                return true;
            }

            RegistroDatos actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Id != id)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                CantidadRegistros--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Muestra todos los registros en la consola.
        /// </summary>
        public void Mostrar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La tabla está vacía.");
                return;
            }

            RegistroDatos? actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"[ID: {actual.Id}] - Nombre: {actual.Nombre} | Valor: {actual.Valor}");
                actual = actual.Siguiente;
            }
        }

        /// <summary>
        /// Extrae la lista a un arreglo secuencial para indexación.
        /// </summary>
        public RegistroDatos[] ExtraerAArreglo()
        {
            if (CantidadRegistros == 0) throw new InvalidOperationException("No hay datos para extraer.");
            
            RegistroDatos[] arreglo = new RegistroDatos[CantidadRegistros];
            RegistroDatos? actual = cabeza;
            int i = 0;
            while (actual != null)
            {
                arreglo[i] = actual;
                actual = actual.Siguiente;
                i++;
            }
            return arreglo;
        }
    }

    class Program
    {
        static TablaDinamica tabla = new TablaDinamica();
        static RegistroDatos[]? indiceOrdenado = null;

        static void Main(string[] args)
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("      DATACORE v4.0 - MENÚ MAESTRO    ");
                Console.WriteLine("======================================");
                Console.WriteLine($"Registros actuales: {tabla.CantidadRegistros}");
                Console.WriteLine("1. Insertar Registro");
                Console.WriteLine("2. Eliminar por ID");
                Console.WriteLine("3. Mostrar Registros");
                Console.WriteLine("4. Indexar y Ordenar (QuickSort)");
                Console.WriteLine("5. Búsqueda Binaria");
                Console.WriteLine("6. Salir");
                Console.WriteLine("======================================");
                Console.Write("Selecciona una opción: ");

                string? input = Console.ReadLine();
                if (int.TryParse(input, out int opcion))
                {
                    ProcesarOpcion(opcion, ref salir);
                }
                else
                {
                    Console.WriteLine("ERROR: Por favor ingresa un número válido.");
                    Pausar();
                }

            } while (!salir);
        }

        static void ProcesarOpcion(int opcion, ref bool salir)
        {
            switch (opcion)
            {
                case 1:
                    EjecutarInsercion();
                    break;
                case 2:
                    EjecutarEliminacion();
                    break;
                case 3:
                    EjecutarMostrar();
                    break;
                case 4:
                    EjecutarIndexado();
                    break;
                case 5:
                    EjecutarBusqueda();
                    break;
                case 6:
                    Console.WriteLine("Saliendo del sistema de forma segura...");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    Pausar();
                    break;
            }
        }

        static void EjecutarInsercion()
        {
            try
            {
                Console.Write("Ingresa el ID (numérico): ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("El ID debe ser un número entero positivo.");

                Console.Write("Ingresa el Nombre: ");
                string? nombre = Console.ReadLine();

                Console.Write("Ingresa el Valor: ");
                string? valor = Console.ReadLine();

                tabla.Insertar(id, nombre!, valor!);
                indiceOrdenado = null; // Invalida el índice porque la tabla cambió
                Console.WriteLine($"Registro insertado con éxito. ID Asignado: {id}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"ERROR de Formato: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
            Pausar();
        }

        static void EjecutarEliminacion()
        {
            try
            {
                Console.Write("Ingresa el ID a eliminar: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("El ID debe ser un número.");

                bool eliminado = tabla.Eliminar(id);
                if (eliminado)
                {
                    indiceOrdenado = null; // Invalida el índice porque la tabla cambió
                    Console.WriteLine("Registro eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún registro con ese ID.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Pausar();
        }

        static void EjecutarMostrar()
        {
            try
            {
                Console.WriteLine("\n--- Registros en la Tabla ---");
                tabla.Mostrar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar registros: {ex.Message}");
            }
            Pausar();
        }

        static void EjecutarIndexado()
        {
            try
            {
                Console.WriteLine("Extrayendo datos al arreglo auxiliar...");
                indiceOrdenado = tabla.ExtraerAArreglo();

                Console.WriteLine("Ordenando datos con QuickSort...");
                QuickSort(indiceOrdenado, 0, indiceOrdenado.Length - 1);

                Console.WriteLine("¡Índice construido y ordenado exitosamente! Listo para búsquedas.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al indexar: {ex.Message}");
            }
            Pausar();
        }

        static void EjecutarBusqueda()
        {
            try
            {
                if (indiceOrdenado == null)
                    throw new InvalidOperationException("Debes generar el índice (Opción 4) antes de buscar.");

                Console.Write("Ingresa el ID a buscar: ");
                if (!int.TryParse(Console.ReadLine(), out int idBuscado))
                    throw new FormatException("El ID debe ser numérico.");

                int comparaciones;
                RegistroDatos? encontrado = BuscarRegistroIndexado(indiceOrdenado, idBuscado, out comparaciones);

                if (encontrado != null)
                {
                    Console.WriteLine($"\n¡Registro Encontrado!");
                    Console.WriteLine($"Nombre: {encontrado.Nombre} | Valor: {encontrado.Valor}");
                }
                else
                {
                    Console.WriteLine("\nEl registro no existe en la base de datos.");
                }
                Console.WriteLine($"> Comparaciones realizadas en tiempo O(log n): {comparaciones}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"ERROR de Formato: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"ERROR de Operación: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda: {ex.Message}");
            }
            Pausar();
        }

        /// <summary>
        /// Implementación de Búsqueda Binaria iterativa en O(log n).
        /// </summary>
        static RegistroDatos? BuscarRegistroIndexado(RegistroDatos[] arreglo, int idBuscado, out int comparaciones)
        {
            comparaciones = 0;
            if (arreglo == null || arreglo.Length == 0) return null;

            int izquierda = 0;
            int derecha = arreglo.Length - 1;

            while (izquierda <= derecha)
            {
                comparaciones++;
                int medio = izquierda + (derecha - izquierda) / 2;

                if (arreglo[medio].Id == idBuscado)
                {
                    return arreglo[medio];
                }

                if (arreglo[medio].Id < idBuscado)
                {
                    izquierda = medio + 1; // Descartar mitad izquierda
                }
                else
                {
                    derecha = medio - 1;   // Descartar mitad derecha
                }
            }
            return null; // No encontrado
        }

        /// <summary>
        /// Algoritmo de ordenamiento QuickSort en tiempo O(n log n).
        /// </summary>
        static void QuickSort(RegistroDatos[] arreglo, int izq, int der)
        {
            if (izq < der)
            {
                int indiceParticion = Particionar(arreglo, izq, der);
                QuickSort(arreglo, izq, indiceParticion - 1);
                QuickSort(arreglo, indiceParticion + 1, der);
            }
        }

        static int Particionar(RegistroDatos[] arreglo, int izq, int der)
        {
            int pivote = arreglo[der].Id;
            int i = izq - 1;

            for (int j = izq; j < der; j++)
            {
                if (arreglo[j].Id <= pivote)
                {
                    i++;
                    // Intercambiar
                    RegistroDatos temp = arreglo[i];
                    arreglo[i] = arreglo[j];
                    arreglo[j] = temp;
                }
            }
            // Intercambiar el pivote
            RegistroDatos temp1 = arreglo[i + 1];
            arreglo[i + 1] = arreglo[der];
            arreglo[der] = temp1;

            return i + 1;
        }

        static void Pausar()
        {
            Console.WriteLine("\nPresiona ENTER para volver al menú...");
            Console.ReadLine();
        }
    }
}

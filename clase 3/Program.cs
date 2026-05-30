using System;
using System.Collections.Generic;
using System.Linq;

namespace InventarioApp
{
    // === PASO 2: Modelo de Datos ===
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }

        // Constructor
        public Producto(int id, string nombre, double precio, int cantidad)
        {
            ID = id;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }

        // Método ToString para facilitar la impresión
        public override string ToString()
        {
            return $"[{ID}] {Nombre} - ${Precio:F2} | Stock: {Cantidad}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // === PASO 3: Construyendo el Inventario con List<T> ===
            
            // Sintaxis 1: Inicializador de colección
            List<Producto> inventario = new List<Producto>
            {
                new Producto(1, "Laptop Lenovo", 15999.00, 10),
                new Producto(2, "Mouse Inalámbrico", 349.00, 25),
                new Producto(3, "Teclado Mecánico", 899.00, 0)
            };

            // Sintaxis 2: Agregar elementos con .Add()
            inventario.Add(new Producto(4, "Monitor 24\"", 4500.00, 5));
            inventario.Add(new Producto(5, "Audífonos Sony", 1200.00, 0));

            Console.WriteLine($"\nTotal en inventario: {inventario.Count} productos");
            Console.WriteLine("-------------------------------------------------");

            // === PASO 4: Consultas LINQ ===

            // Consulta 1: Ordenar por precio descendente
            var porPrecio = inventario.OrderByDescending(p => p.Precio).ToList();
            
            Console.WriteLine("\n=== Productos por Precio (Mayor a Menor) ===");
            foreach (var p in porPrecio)
            {
                Console.WriteLine(p);
            }

            // Consulta 2: Filtrar productos agotados
            var agotados = inventario.Where(p => p.Cantidad == 0).ToList();
            
            Console.WriteLine("\n=== Productos Agotados ===");
            if (agotados.Count == 0)
            {
                Console.WriteLine("Sin productos agotados.");
            }
            else
            {
                agotados.ForEach(p => Console.WriteLine(p));
            }
            Console.WriteLine("-------------------------------------------------");

            // === PASO 5: Búsqueda Instantánea con Dictionary<K,V> ===

            // Convertir la lista en un diccionario
            Dictionary<int, Producto> catalogo = inventario.ToDictionary(p => p.ID, p => p);

            // Búsqueda interactiva
            Console.Write("\nIngresa el ID del producto a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                if (catalogo.TryGetValue(idBuscado, out Producto encontrado))
                {
                    Console.WriteLine($"\n✅ Producto encontrado: {encontrado}");
                }
                else
                {
                    Console.WriteLine("\n❌ El producto con ese ID no existe en el catálogo.");
                }
            }
            else
            {
                Console.WriteLine("\n Entrada no válida. Debes ingresar un número entero.");
            }
            
            // Pausa para que la consola no se cierre inmediatamente
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
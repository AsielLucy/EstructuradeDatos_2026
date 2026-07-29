using System;

// (Asumido de las fases 1 y 2, necesario para que el código del documento compile)
public struct RegistroDatos
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Monto { get; set; }

    public RegistroDatos(int id, string nombre, decimal monto)
    {
        Id = id;
        Nombre = nombre;
        Monto = monto;
    }
}

// 1. CLASE NODOREGISTRO
public class NodoRegistro
{
    // El dato que este nodo almacena
    public RegistroDatos Dato { get; set; }
    
    // Referencia al siguiente nodo
    // null si es el último eslabón
    public NodoRegistro? Siguiente { get; set; }

    // Constructor: inicializa el dato
    // Siguiente queda en null por defecto
    public NodoRegistro(RegistroDatos dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// 2. CLASE TABLADINAMICA
public class TablaDinamica
{
    private NodoRegistro? cabeza;
    private int contadorRegistros;

    public TablaDinamica()
    {
        cabeza = null;
        contadorRegistros = 0;
    }

    public void InsertarInicio(RegistroDatos nuevoRegistro)
    {
        // Guarda de seguridad sugerida para evitar NullReferenceException
        // if (nuevoRegistro == null) throw new ArgumentNullException(nameof(nuevoRegistro));
        
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        // El nuevo nodo apunta a quien era la cabeza anterior
        nuevoNodo.Siguiente = cabeza;
        // El nuevo nodo ES la nueva cabeza
        cabeza = nuevoNodo;
        contadorRegistros++;
    }

    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        if (cabeza == null) {
            cabeza = nuevoNodo;
        } else {
            NodoRegistro actual = cabeza;
            // Recorre hasta el último nodo
            while (actual.Siguiente != null) {
                actual = actual.Siguiente;
            }
            // Enlaza el nuevo al final
            actual.Siguiente = nuevoNodo;
        }
        contadorRegistros++;
    }

    public void EliminarPorId(int idTarget)
    {
        if (cabeza == null) return;

        // Caso especial: eliminar la cabeza
        if (cabeza.Dato.Id == idTarget) {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null) {
            if (actual.Dato.Id == idTarget) {
                // Reconecta saltando el nodo
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    public RegistroDatos[] ObtenerComoArreglo()
    {
        RegistroDatos[] resultado = new RegistroDatos[contadorRegistros];
        NodoRegistro? actual = cabeza;
        int i = 0;
        
        while (actual != null) {
            resultado[i] = actual.Dato;
            actual = actual.Siguiente;
            i++;
        }
        return resultado;
    }
}

// 3. CLASE PRINCIPAL (MAIN ORCHESTRATOR)
class Program
{
    static void Main(string[] args)
    {
        // Instanciar la estructura dinámica
        TablaDinamica dataCore = new TablaDinamica();

        // Paso 1: Insertar 15 registros dinámicos
        for (int i = 1; i <= 15; i++)
        {
            RegistroDatos reg = new RegistroDatos(i, $"Transacción-{i}", i * 100.0m);
            dataCore.InsertarFinal(reg);
            Console.WriteLine($"[INSERT] Registro {i} añadido a la cadena.");
        }

        // Paso 2: Eliminar 2 registros específicos
        Console.WriteLine("\n--- Eliminando registros con Id 5 y Id 11 ---");
        dataCore.EliminarPorId(5);
        dataCore.EliminarPorId(11);
        Console.WriteLine("Cadena reestructurada exitosamente. Sin NullReferenceException.");

        // Paso 3: Convertir a arreglo y ordenar con QuickSort (Fase 2)
        RegistroDatos[] arreglo = dataCore.ObtenerComoArreglo();
        Console.WriteLine($"\nRegistros en arreglo: {arreglo.Length} (esperado: 13)");

        QuickSort(arreglo, 0, arreglo.Length - 1); // Motor de Fase 2

        Console.WriteLine("\n--- Arreglo ordenado por Id (QuickSort) ---");
        foreach (var r in arreglo)
        {
            Console.WriteLine($"Id: {r.Id} | Nombre: {r.Nombre} | Monto: {r.Monto:C}");
        }
        
        // Console.ReadKey(); // Sugerido en el documento si la consola se cierra muy rápido
    }

    // Método QuickSort simplificado referenciado en la Fase 2 (necesario para compilar)
    static void QuickSort(RegistroDatos[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            QuickSort(arr, left, pivot - 1);
            QuickSort(arr, pivot + 1, right);
        }
    }

    static int Partition(RegistroDatos[] arr, int left, int right)
    {
        int pivot = arr[right].Id;
        int i = (left - 1);
        for (int j = left; j < right; j++)
        {
            if (arr[j].Id < pivot)
            {
                i++;
                RegistroDatos temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        RegistroDatos temp1 = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = temp1;
        return i + 1;
    }
}

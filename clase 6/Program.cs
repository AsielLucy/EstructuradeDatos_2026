using System;

// CLASE: Al ser un tipo de referencia, sus instancias vivirán en el Heap.
class Personaje
{
    public string? Nombre { get; set; }
    public string? Titulo { get; set; }
}

class Program
{
    // 1. MODIFICADOR REF: Opera sobre la dirección de memoria original en el Stack.
    // Ambas variables ya deben tener un valor antes de entrar aquí.
    static void IntercambiarReliquias(ref string reliquia1, ref string reliquia2)
    {
        string temp = reliquia1;
        reliquia1 = reliquia2;
        reliquia2 = temp;
    }

    // 2. MODIFICADOR OUT: Permite devolver más de un valor.
    // El parámetro 'critico' no necesita valor inicial, pero es OBLIGATORIO asignarle uno antes de salir.
    static int InvocarEspiritus(int poderBase, int multiplicador, out bool critico)
    {
        int poderTotal = poderBase * multiplicador;
        
        // Asignamos el valor al parámetro 'out'
        critico = poderTotal > 100; 
        
        // Retornamos el valor principal
        return poderTotal;
    }

    static void Main()
    {
        // ... (todo el código anterior) ...

        Console.WriteLine("Explicación: El método nos devolvió el daño (int) con el 'return' normal, y el estado crítico (bool) a través del canal de 'out'.");
        
        // Agrega esta línea para pausar el programa
        Console.ReadLine(); 
    
        Console.WriteLine("--- 1. DEMOSTRACIÓN DE OBJETOS (HEAP) ---");
        // Creamos un solo objeto en la memoria (Heap).
        Personaje deidad = new Personaje { Nombre = "Desconocido", Titulo = "Hija de la Muerte Egipcia" };
        
        // Asignamos la referencia, NO estamos creando un clon.
        Personaje avatar = deidad; 

        // Modificamos a través de la segunda variable.
        avatar.Nombre = "Reina del Inframundo"; 

        Console.WriteLine($"Nombre en la variable 'deidad': {deidad.Nombre}");
        Console.WriteLine($"Nombre en la variable 'avatar': {avatar.Nombre}");
        Console.WriteLine("Explicación: Ambas variables apuntan exactamente al mismo espacio en la memoria.\n");

        Console.WriteLine("--- 2. DEMOSTRACIÓN DE REF (STACK) ---");
        string manoIzquierda = "Cetro de Anubis";
        string manoDerecha = "Nada";

        Console.WriteLine($"Antes del cambio -> Izquierda: {manoIzquierda} | Derecha: {manoDerecha}");
        
        // Pasamos las variables por referencia usando 'ref'
        IntercambiarReliquias(ref manoIzquierda, ref manoDerecha);
        
        Console.WriteLine($"Después del cambio -> Izquierda: {manoIzquierda} | Derecha: {manoDerecha}");
        Console.WriteLine("Explicación: El método no intercambió copias, intercambió los valores originales en sus celdas de memoria.\n");

        Console.WriteLine("--- 3. DEMOSTRACIÓN DE OUT ---");
        // Declaramos la variable 'esCritico' en la misma línea de la llamada (característica de C# moderno)
        int daño = InvocarEspiritus(40, 3, out bool esCritico);
        
        Console.WriteLine($"Daño de invocación generado: {daño}");
        if (esCritico)
        {
            Console.WriteLine("¡Fue un ataque crítico que asustaría a cualquiera!");
        }
        else
        {
            Console.WriteLine("Fue un ataque normal.");
        }
        Console.WriteLine("Explicación: El método nos devolvió el daño (int) con el 'return' normal, y el estado crítico (bool) a través del canal de 'out'.");
    }
}
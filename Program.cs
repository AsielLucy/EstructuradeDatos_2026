using System;

class ProgramaPoligonos
{
    static void Main()
    {
        int lados = SeleccionarPoligono();
        (double lado, double apotema) = PedirDatos();
        double area = CalcularArea(lados, lado, apotema);

        Console.WriteLine($"El área del polígono de {lados} lados es: {area}");
    }

    static int SeleccionarPoligono()
    {
        Console.WriteLine("Selecciona el polígono:");
        Console.WriteLine("1. Pentágono (5 lados)");
        Console.WriteLine("2. Hexágono (6 lados)");
        Console.WriteLine("3. Heptágono (7 lados)");
        Console.WriteLine("4. Octágono (8 lados)");

        int opcion;
        while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 4)
        {
            Console.WriteLine("❌ Opción inválida. Intenta de nuevo.");
        }

        return opcion + 4; // porque la opción 1 corresponde a 5 lados, etc.
    }

    static (double lado, double apotema) PedirDatos()
    {
        double lado = LeerDecimalPositivo("Ingresa la medida del lado: ");
        double apotema = LeerDecimalPositivo("Ingresa la medida de la apotema: ");
        return (lado, apotema);
    }

    static double LeerDecimalPositivo(string mensaje)
    {
        double valor;
        Console.Write(mensaje);
        while (!double.TryParse(Console.ReadLine(), out valor) || valor <= 0)
        {
            Console.WriteLine("❌ Valor inválido. Debe ser un número decimal positivo.");
            Console.Write(mensaje);
        }
        return valor;
    }

    static double CalcularArea(int lados, double lado, double apotema)
    {
        double perimetro = lados * lado;
        return (perimetro * apotema) / 2;
    }
}

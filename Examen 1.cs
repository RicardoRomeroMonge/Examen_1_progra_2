using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese su nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Ingrese su apellido: ");
        string apellido = Console.ReadLine();

        Console.WriteLine("Seleccione su puesto:");
        Console.WriteLine("1. Administrativo");
        Console.WriteLine("2. Regular");
        Console.Write("Opción: ");
        int opcion = int.Parse(Console.ReadLine());

        int horasSemanales = (opcion == 1) ? 48 : 43;
        double tarifaHora = (opcion == 1) ? 3475 : 2845;

        double totalHoras = 0, totalExtras = 0, totalExtrasDobles = 0, totalExtrasNormales = 0;

        for (int i = 1; i <= 5; i++) // Asumiendo una semana laboral de 5 días
        {
            Console.Write($"Ingrese hora de entrada del día {i} (Formato 12h, ej: 08:00 AM/PM): ");
            DateTime entrada = DateTime.Parse(Console.ReadLine());
            Console.Write($"Ingrese hora de salida del día {i} (Formato 12h, ej: 05:30 PM): ");
            DateTime salida = DateTime.Parse(Console.ReadLine());

            double horasDia = (salida - entrada).TotalHours;
            totalHoras += horasDia;

            if (horasDia > 8)
            {
                double extras = horasDia - 8;
                if (salida.Hour >= 22 || entrada.Hour < 5)
                {
                    totalExtrasDobles += extras;
                }
                else
                {
                    totalExtrasNormales += extras;
                }
                totalExtras += extras;
            }
        }

        double salarioBase = horasSemanales * tarifaHora;
        double pagoExtrasDobles = totalExtrasDobles * tarifaHora * 2;
        double pagoExtrasNormales = totalExtrasNormales * tarifaHora * 1.5;
        double salarioTotal = salarioBase + pagoExtrasDobles + pagoExtrasNormales;

        Console.WriteLine("\nResumen de Pago:");
        Console.WriteLine($"Empleado: {nombre} {apellido}");
        Console.WriteLine($"Puesto: {(opcion == 1 ? "Administrativo" : "Regular")}");
        Console.WriteLine($"Horas trabajadas: {totalHoras}");
        Console.WriteLine($"Horas extra: {totalExtras} (Dobles: {totalExtrasDobles}, Normales: {totalExtrasNormales})");
        Console.WriteLine($"Salario total: {salarioTotal} colones");

        Console.Write("¿Desea generar un reporte? (s/n): ");
        string generarReporte = Console.ReadLine().ToLower();

        if (generarReporte == "s")
        {
            string reporte = $"Empleado: {nombre} {apellido}\n" +
                             $"Puesto: {(opcion == 1 ? "Administrativo" : "Regular")}\n" +
                             $"Horas trabajadas: {totalHoras}\n" +
                             $"Horas extra: {totalExtras} (Dobles: {totalExtrasDobles}, Normales: {totalExtrasNormales})\n" +
                             $"Salario total: {salarioTotal} colones\n";

            File.WriteAllText("Reporte_Salarial.txt", reporte);
            Console.WriteLine("Reporte generado con éxito: Reporte_Salarial.txt");
        }
    }
}

using System;

namespace FigurasGeometricas
{
    // Esta clase representa un círculo
    public class Circulo
    {
        private double radio;

        // Constructor para crear un círculo con un radio
        public Circulo(double r)
        {
            radio = r;
        }

        // Calcula el área del círculo
        public double CalcularArea()
        {
            return Math.PI * radio * radio;
        }

        // Calcula el perímetro del círculo
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }
    }

    // Esta clase representa un rectángulo
    public class Rectangulo
    {
        private double baseRect;
        private double altura;

        // Constructor para crear un rectángulo con base y altura
        public Rectangulo(double b, double h)
        {
            baseRect = b;
            altura = h;
        }

        // Calcula el área del rectángulo
        public double CalcularArea()
        {
            return baseRect * altura;
        }

        // Calcula el perímetro del rectángulo
        public double CalcularPerimetro()
        {
            return 2 * (baseRect + altura);
        }
    }

    // Programa principal para probar las clases
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un círculo con radio 5
            Circulo miCirculo = new Circulo(5);
            Console.WriteLine("Área del círculo: " + miCirculo.CalcularArea());
            Console.WriteLine("Perímetro del círculo: " + miCirculo.CalcularPerimetro());

            // Crear un rectángulo con base 4 y altura 3
            Rectangulo miRectangulo = new Rectangulo(4, 3);
            Console.WriteLine("Área del rectángulo: " + miRectangulo.CalcularArea());
            Console.WriteLine("Perímetro del rectángulo: " + miRectangulo.CalcularPerimetro());
        }
    }
}

using System;

class ComplexNumber
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Сложение
    public ComplexNumber Add(ComplexNumber other)
    {
        return new ComplexNumber(this.Real + other.Real, this.Imaginary + other.Imaginary);
    }

    // Вычитание
    public ComplexNumber Subtract(ComplexNumber other)
    {
        return new ComplexNumber(this.Real - other.Real, this.Imaginary - other.Imaginary);
    }

    // Умножение
    public ComplexNumber Multiply(ComplexNumber other)
    {
        double real = this.Real * other.Real - this.Imaginary * other.Imaginary;
        double imaginary = this.Real * other.Imaginary + this.Imaginary * other.Real;
        return new ComplexNumber(real, imaginary);
    }

    public override string ToString()
    {
        return $"{Real} {(Imaginary >= 0 ? "+" : "-")} {Math.Abs(Imaginary)}i";
    }
}
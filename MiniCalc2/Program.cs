interface ILogger
{
    void LogError(string message);
    void LogEvent(string message);
}

class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Ошибка]: {message}");
        Console.ResetColor();
    }

    public void LogEvent(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[Событие]: {message}");
        Console.ResetColor();
    }
}

interface ICalculator
{
    double Add(double a, double b);
}

class SimpleCalculator : ICalculator
{
    private readonly ILogger _logger;

    public SimpleCalculator(ILogger logger)
    {
        _logger = logger;
    }

    public double Add(double a, double b)
    {
        _logger.LogEvent($"Выполняется сложение чисел {a} и {b}");
        return a + b;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Привет! Это мини-калькулятор.");
        ILogger logger = new ConsoleLogger();
        ICalculator calculator = new SimpleCalculator(logger);

        double num1 = 0, num2 = 0;
        bool isValidInput = false;

        do
        {
            try
            {
                Console.Write("Введите первое число: ");
                string input1 = Console.ReadLine();

                if (!double.TryParse(input1, out num1))
                    throw new FormatException();

                Console.Write("Введите второе число: ");
                string input2 = Console.ReadLine();

                if (!double.TryParse(input2, out num2))
                    throw new FormatException();

                isValidInput = true;
            }
            catch (FormatException)
            {
                logger.LogError("Введено некорректное значение. Пожалуйста, введите число.");
            }
            finally
            {
                logger.LogEvent("Для продолжения введите числа снова.");
            }
        } while (!isValidInput);

        double sum = calculator.Add(num1, num2);
        Console.WriteLine($"Сумма чисел {num1} и {num2} равна {sum}.");
    }
}
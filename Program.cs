using System;
using Serilog;
using Serilog.Configuration;

namespace exchange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding=System.Text.Encoding.Unicode;
            var tryParseResult = false;
            var kurs = 0.1315d;
            var com050 = 0.5d;
            var com500 = 500d;
            double sum1 = 0, sum2 = 0, value=0;
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Курс тенге:", kurs)
                .WriteTo.Seq("http://localhost:5341/", apiKey: "UQEo5vFc4UBwpM3fVDO1")
                .CreateLogger();
     

            while (!tryParseResult)
            {
                Console.Write("Введите кол-во валюты (тенге): ");
                var userInput = Console.ReadLine();
                tryParseResult = double.TryParse(userInput, out value);
            
                if(!tryParseResult)
                {
                    Log.Error("Недопустимое значение");
                }
            }
            Log.Information($"Недопустимое значение: {value}");
            sum1 += value * kurs - 13;
            sum2 += value * kurs * 0.9963;
            if (value <= com500 / kurs)
            {
                Console.WriteLine("Курс тенге: " + kurs + " ₽");
                Console.WriteLine("Кол-во средств на вывод: {0:#} ₽", (kurs * value));
                Console.WriteLine("Комиссия: 13 ₽");
                Console.WriteLine("Итог: {0:#} ₽", sum1);
                Log.Information($"К выдаче: {sum1:C2}");
            }
            else
            {
                Console.WriteLine("Курс тенге: " + kurs + " ₽");
                Console.WriteLine("Кол-во средств на вывод: {0:#} ₽", (value * kurs));
                Console.WriteLine("Комиссия: " + com050 + " %");
                Console.WriteLine("Итог: {0:#} ₽", sum2);
                Log.Information($"К выдаче: {sum2:C2}");
            }
            Log.Information("Удачное конвертирование");
            Log.CloseAndFlush();
        }
        
    }
}
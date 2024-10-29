using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Polinom işlemleri yapmak için iki polinom girin (örneğin: 2x^2 + 3x - 5). Çıkmak için 'exit' yazın.");

        while (true)
        {
            // İlk polinomu al
            Console.Write("Birinci polinom: ");
            string polinom1 = Console.ReadLine();
            if (polinom1.ToLower() == "exit") break;

            // İkinci polinomu al
            Console.Write("İkinci polinom: ");
            string polinom2 = Console.ReadLine();
            if (polinom2.ToLower() == "exit") break;

            // Polinomları çözümle
            var poly1 = PolinomCozumle(polinom1);
            var poly2 = PolinomCozumle(polinom2);

            // Polinomları topla ve sonucu göster
            var toplam = PolinomTopla(poly1, poly2);
            Console.WriteLine("Toplam: " + PolinomGoster(toplam));

            // Polinomları çıkar ve sonucu göster
            var fark = PolinomCikar(poly1, poly2);
            Console.WriteLine("Fark: " + PolinomGoster(fark));
        }
    }

    // Polinom terimlerini ayıran ve katsayı ve dereceleri tutan metot
    static Dictionary<int, int> PolinomCozumle(string polinom)
    {
        var sonuc = new Dictionary<int, int>();
        var terimler = Regex.Matches(polinom.Replace(" ", ""), @"([+-]?\d*)x\^?(\d*)|([+-]?\d+)");

        foreach (Match terim in terimler)
        {
            int katsayi = 0;
            int derece = 0;

            if (terim.Groups[1].Success) // x'li terim
            {
                katsayi = terim.Groups[1].Value == "" || terim.Groups[1].Value == "+" ? 1 :
                          terim.Groups[1].Value == "-" ? -1 : int.Parse(terim.Groups[1].Value);

                derece = terim.Groups[2].Value == "" ? 1 : int.Parse(terim.Groups[2].Value);
            }
            else // Sabit terim
            {
                katsayi = int.Parse(terim.Groups[3].Value);
                derece = 0;
            }

            if (sonuc.ContainsKey(derece))
                sonuc[derece] += katsayi;
            else
                sonuc[derece] = katsayi;
        }

        return sonuc;
    }

    // Polinom toplama metodu
    static Dictionary<int, int> PolinomTopla(Dictionary<int, int> poly1, Dictionary<int, int> poly2)
    {
        var sonuc = new Dictionary<int, int>(poly1);

        foreach (var terim in poly2)
        {
            if (sonuc.ContainsKey(terim.Key))
                sonuc[terim.Key] += terim.Value;
            else
                sonuc[terim.Key] = terim.Value;
        }

        return sonuc;
    }

    // Polinom çıkarma metodu
    static Dictionary<int, int> PolinomCikar(Dictionary<int, int> poly1, Dictionary<int, int> poly2)
    {
        var sonuc = new Dictionary<int, int>(poly1);

        foreach (var terim in poly2)
        {
            if (sonuc.ContainsKey(terim.Key))
                sonuc[terim.Key] -= terim.Value;
            else
                sonuc[terim.Key] = -terim.Value;
        }

        return sonuc;
    }

    // Polinomu string formatında gösterme metodu
    static string PolinomGoster(Dictionary<int, int> polinom)
    {
        var terimler = new List<string>();

        foreach (var terim in polinom)
        {
            if (terim.Value == 0) continue;

            string katsayi = terim.Value == 1 && terim.Key != 0 ? "" :
                             terim.Value == -1 && terim.Key != 0 ? "-" :
                             terim.Value.ToString();

            string xTerimi = terim.Key == 0 ? "" : terim.Key == 1 ? "x" : $"x^{terim.Key}";

            terimler.Add($"{katsayi}{xTerimi}");
        }

        return string.Join(" + ", terimler).Replace("+ -", "- ");
    }
}

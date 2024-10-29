using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Uygun tarihleri listeleme başlıyor...");

        // Uygun tarihleri tutan liste
        List<string> uygunTarihler = new List<string>();

        // Geçerli yıl aralığını belirle
        int simdikiYil = DateTime.Now.Year;
        int baslangicYil = simdikiYil + 1;
        int bitisYil = 3000;

        for (int yil = baslangicYil; yil <= bitisYil; yil++)
        {
            // Yıl koşulunu kontrol et
            if (!YilKosulu(yil)) continue;

            for (int ay = 1; ay <= 12; ay++)
            {
                // Ay koşulunu kontrol et
                if (!AyKosulu(ay)) continue;

                for (int gun = 1; gun <= DateTime.DaysInMonth(yil, ay); gun++)
                {
                    // Gün koşulunu kontrol et
                    if (!GunAsalMi(gun)) continue;

                    // Geçerli bir tarih bulundu
                    uygunTarihler.Add($"{gun:D2}/{ay:D2}/{yil}");
                }
            }
        }

        // Tüm uygun tarihleri ekrana yazdır
        Console.WriteLine("Uygun tarihler:");
        foreach (var tarih in uygunTarihler)
        {
            Console.WriteLine(tarih);
        }
    }

    // Gün sayısının asal olup olmadığını kontrol etme metodu
    static bool GunAsalMi(int gun)
    {
        if (gun < 2) return false;
        for (int i = 2; i <= Math.Sqrt(gun); i++)
        {
            if (gun % i == 0) return false;
        }
        return true;
    }

    // Ay sayısının basamakları toplamının çift olup olmadığını kontrol etme metodu
    static bool AyKosulu(int ay)
    {
        int toplam = 0;
        while (ay > 0)
        {
            toplam += ay % 10;
            ay /= 10;
        }
        return toplam % 2 == 0;
    }

    // Yıl koşulunu kontrol etme metodu
    static bool YilKosulu(int yil)
    {
        int rakamToplami = 0;
        int tempYil = yil;

        while (tempYil > 0)
        {
            rakamToplami += tempYil % 10;
            tempYil /= 10;
        }

        return rakamToplami < (yil / 4);
    }
}

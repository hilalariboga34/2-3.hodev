using System;
using System.Collections.Generic;

class Program
{
    static int M = 5; // Labirent satır sayısı
    static int N = 5; // Labirent sütun sayısı
    static (int, int) hedef = (M - 1, N - 1); // Hedef koordinat (M-1, N-1)

    static void Main()
    {
        // Labirentteki hücreleri ziyaret durumu için bir ızgara tanımla
        bool[,] ziyaretEdildi = new bool[M, N];

        // Başlangıçtan hedefe ulaşmak için bir yol olup olmadığını kontrol et
        if (LabirentiCoz(0, 0, ziyaretEdildi))
            Console.WriteLine("Şehre ulaşmak için bir yol bulundu!");
        else
            Console.WriteLine("Şehir kayboldu!");
    }

    // Labirenti çözmek için geri izleme algoritması
    static bool LabirentiCoz(int x, int y, bool[,] ziyaretEdildi)
    {
        // Hedefe ulaştıysak başarılı bir yol bulundu
        if ((x, y) == hedef)
        {
            Console.WriteLine($"Hedefe ulaşıldı: ({x}, {y})");
            return true;
        }

        // Hücre sınırları dışında kalıyorsa veya zaten ziyaret edildiyse geri dön
        if (x < 0 || y < 0 || x >= M || y >= N || ziyaretEdildi[x, y] || !KapilarAcikMi(x, y))
            return false;

        // Geçerli hücreyi ziyaret edilmiş olarak işaretle
        ziyaretEdildi[x, y] = true;
        Console.WriteLine($"({x}, {y}) hücresine gidildi.");

        // Sağ, aşağı, sol ve yukarı hücreleri ziyaret et (sırasıyla)
        if (LabirentiCoz(x + 1, y, ziyaretEdildi) || LabirentiCoz(x, y + 1, ziyaretEdildi) ||
            LabirentiCoz(x - 1, y, ziyaretEdildi) || LabirentiCoz(x, y - 1, ziyaretEdildi))
            return true;

        // Geçerli yol çıkmazsa geri çekil ve işareti kaldır
        ziyaretEdildi[x, y] = false;
        return false;
    }

    // Kapının açılabilir olup olmadığını kontrol etme fonksiyonu
    static bool KapilarAcikMi(int x, int y)
    {
        return BasamakAsalMi(x) && BasamakAsalMi(y) && ToplamCarpimaBolunurMu(x, y);
    }

    // x ve y'nin basamaklarının asal olup olmadığını kontrol eder
    static bool BasamakAsalMi(int sayi)
    {
        int birlerBasamagi = sayi % 10;
        int onlarBasamagi = sayi / 10;
        return AsalMi(birlerBasamagi) && AsalMi(onlarBasamagi);
    }

    // Sayının asal olup olmadığını kontrol eder
    static bool AsalMi(int sayi)
    {
        if (sayi < 2) return false;
        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0) return false;
        }
        return true;
    }

    // x ve y toplamının, x ve y çarpımına bölünüp bölünmediğini kontrol eder
    static bool ToplamCarpimaBolunurMu(int x, int y)
    {
        int toplam = x + y;
        int carpim = x * y;
        return carpim != 0 && toplam % carpim == 0;
    }
}

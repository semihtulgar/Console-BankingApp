using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    class ArayuzGoster
    {
        public void InitializeUI()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-> Hesap Açma İşlemleri \n");
            string[] hesapTurleri = new string[] {
                "-> Kısa Vadeli Hesap Açma (1) (min 5.000 TL)",
                "-> Uzun Vadeli Hesap Açma (2) (min 10.000 TL)",
                "-> Özel Hesap Açma (3) (Minimum Limit Yok)",
                "-> Cari Hesap Açma (4) (Minimum Limit Yok)"
            };
            foreach (var hesapTuru in hesapTurleri)
            {
                Console.WriteLine($"\t{hesapTuru}\n");
            }

            string[] digerHesapIslemleri = new string[] {
                "-> Para Yatırma İşlemi (5)",
                "-> Para Çekme İşlemi (6)",
                "-> Hesap Listesi (7)",
                "-> Hesap Durumu (8)",
                "-> Hesap İşlem Kayıtları (9)",
                "-> Çekiliş (10)",
                $"-> Çıkış için \"q\" giriniz"
            };

            foreach (var digerHesapIslemi in digerHesapIslemleri)
            {
                Console.WriteLine($"{digerHesapIslemi}\n");
            }

        }
    }
}

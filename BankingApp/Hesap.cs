using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    class Hesap
    {
        public Hesap()
        {

        }

        public string HesapNo { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public HesapTuru HesapTuruDeger { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public double Bakiye { get; set; }
        public int CekilisHakki { get; set; }


        public Hesap HesapAc()
        {
            Hesap hesap = new Hesap();

            hesap.HesapNo = HesapNoTanımla();

            Console.Write("İsim : ");
            hesap.Isim = Console.ReadLine();
            Console.WriteLine("\n");

            Console.Write("Soyisim : ");
            hesap.Soyisim = Console.ReadLine();
            Console.WriteLine("\n");

            foreach (var hesapTuru in hesapTurleri)
            {
                Console.WriteLine($"\t{hesapTuru}\n");
            }
            Console.Write("Hesap Türü (1-4) : ");
            int geciciHesapTuruDeger = Convert.ToInt16(Console.ReadLine());
            while (HesapTuruOnay(geciciHesapTuruDeger) == false)
            {
                Console.WriteLine("Geçersiz Hesap Türü Değeri!\n\n");
                Console.Write("Hesap Türü (1-4) : ");
                geciciHesapTuruDeger = Convert.ToInt16(Console.ReadLine());
            }
            hesap.HesapTuruDeger = (HesapTuru)geciciHesapTuruDeger;
            Console.WriteLine("\n");


            Console.Write("Tarih Giriniz (YIL,AY,GÜN) : ");
            hesap.OlusturulmaTarihi = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            Console.Write("Yüklenecek Para Miktarı (₺) : ");
            hesap.Bakiye = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("\n");

            hesap.CekilisHakki = 0;

            return hesap;
        }
        public string HesapNoTanımla()
        {
            string hesapNo = "";
            for (int i = 0; i < 8; i++)
            {
                Random random = new Random();
                hesapNo += random.Next(0, 9);
            }
            return hesapNo;
        }

        public bool HesapTuruOnay(int geciciHesapTuruDeger)
        {
            if (geciciHesapTuruDeger.Equals(1) || geciciHesapTuruDeger.Equals(2) || geciciHesapTuruDeger.Equals(3) || geciciHesapTuruDeger.Equals(4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double HesapBakiyeOnay(Hesap hesap, double geciciBakiye)
        {
            return 0;
        }

        readonly string[] hesapTurleri = new string[] {
                "-> Kısa Vadeli Hesap Açma (1)",
                "-> Uzun Vadeli Hesap Açma (2)",
                "-> Özel Hesap Açma (3)",
                "-> Cari Hesap Açma (4)"
        };

    }

    enum HesapTuru
    {
        KısaVadeli = 1,
        UzunVadeli = 2,
        Ozel = 3,
        Cari = 4
    }
}

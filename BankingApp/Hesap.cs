﻿using System;
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

            Console.WriteLine("\n");

            Console.Write("İsim : ");
            hesap.Isim = Console.ReadLine().ToUpper();

            Console.WriteLine("\n");

            Console.Write("Soyisim : ");
            hesap.Soyisim = Console.ReadLine().ToUpper();

            Console.WriteLine("\n");

            foreach (var hesapTuru in hesapTurleri)
            {
                Console.WriteLine($"\t{hesapTuru}\n");
            }


            string geciciHesapTuruDegeri;
            do
            {
                Console.Write("Hesap Türü (1-4) : ");
                geciciHesapTuruDegeri = Console.ReadLine();

            } while (HesapTuruOnay(geciciHesapTuruDegeri) == false);

            hesap.HesapTuruDeger = (HesapTuru)Convert.ToInt32(geciciHesapTuruDegeri);
            Console.WriteLine("\n");


            string geciciTarihString = "";
            DateTime geciciTarihDeger;
            do
            {
                Console.Write("Tarih Giriniz (YIL,AY,GÜN) ya da (GÜN/AY/YIL) : ");
                geciciTarihString = Console.ReadLine();

            } while (!DateTime.TryParse(geciciTarihString, out geciciTarihDeger));
            hesap.OlusturulmaTarihi = geciciTarihDeger;
            Console.WriteLine("\n");

            Console.Write("Yüklenecek Para Miktarı (Kuruşu \",\" ile belirtiniz) : ");
            double geciciBakiye = Convert.ToDouble(Console.ReadLine());
            while (HesapBakiyeOnay(hesap.HesapTuruDeger, geciciBakiye) == false)
            {
                Console.WriteLine("***************************");
                Console.WriteLine("\tYetersiz Miktar!");
                Console.WriteLine("***************************");
                Console.WriteLine("\n\n");
                Console.Write("Yüklenecek Para Miktarı (₺) : ");
                geciciBakiye = Convert.ToDouble(Console.ReadLine());
            }
            hesap.Bakiye = geciciBakiye;

            Console.WriteLine("\n");

            hesap.CekilisHakki = 0;

            return hesap;
        }

        public void HesapDurum(bool hesapDurum, string hesapNo, List<Hesap> bankaHesaplari)
        {
            if (hesapDurum)
            {
                int result = bankaHesaplari.FindIndex(item => item.HesapNo == hesapNo);
                Console.WriteLine("\n");
                Console.WriteLine("********************* HESAP DURUMU *********************");
                Console.WriteLine($"Hesap Numarası : {bankaHesaplari[result].HesapNo}");
                Console.WriteLine($"İsim : {bankaHesaplari[result].Isim}");
                Console.WriteLine($"Soyisim : {bankaHesaplari[result].Soyisim}");
                Console.WriteLine($"Banka Hesap Türü : {bankaHesaplari[result].HesapTuruDeger}");
                Console.WriteLine($"Hesap Oluşturulma Tarihi : {bankaHesaplari[result].OlusturulmaTarihi}");
                Console.WriteLine($"Bakiye : {bankaHesaplari[result].Bakiye} TL");
                Console.WriteLine($"Çekiliş Hakkı : {bankaHesaplari[result].CekilisHakki}");
                Console.WriteLine("********************* HESAP DURUMU *********************");
            }
            else
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("************************************");
                Console.WriteLine("Hesap Bulunamadı!");
                Console.WriteLine("************************************");
            }
        }

        private string HesapNoTanımla()
        {
            string hesapNo = "";
            for (int i = 0; i < 8; i++)
            {
                Random random = new Random();
                hesapNo += random.Next(0, 9);
            }
            return hesapNo;
        }

        public void KarTutari(bool hesapDurum, string hesapNo, List<Hesap> bankaHesaplari, List<Islem> islemGecmisi)
        {
            //Islem islem = new Islem();

            //if (hesapDurum)
            //{
            //    // aranan hesabın indexini bulur.
            //    int result = bankaHesaplari.FindIndex(bankaHesabi => bankaHesabi.HesapNo == hesapNo);
            //    for (int i = 0; i < islemGecmisi.Count; i++)
            //    {
            //        if (islemGecmisi[i].HesapNo == bankaHesaplari[result].HesapNo)
            //        {
            //            switch (islemGecmisi[i].IslemTuru)
            //            {
            //                case IslemTuru.HesapOlusturma:
            //                    if (bankaHesaplari[result].HesapTuruDeger == HesapTuru.KısaVadeli)
            //                    {
            //                        DateTime dateTimeSimdi = DateTime.Today;
            //                        TimeSpan gunFarki = dateTimeSimdi.Subtract(islemGecmisi[i].IslemTarihi);
            //                        bankaHesaplari[result].Bakiye = Math.Round(bankaHesaplari[result].Bakiye + ((bankaHesaplari[result].Bakiye * 15 * (int)gunFarki.Days) / (100 * 365)), 2, MidpointRounding.ToZero);
            //                        Console.WriteLine(bankaHesaplari[result].Bakiye);
            //                    }
            //                    else if (bankaHesaplari[result].HesapTuruDeger == HesapTuru.UzunVadeli)
            //                    {
            //                        DateTime dateTimeSimdi = DateTime.Today;
            //                        TimeSpan gunFarki = dateTimeSimdi.Subtract(islemGecmisi[i].IslemTarihi);
            //                        bankaHesaplari[result].Bakiye = Math.Round(bankaHesaplari[result].Bakiye + ((bankaHesaplari[result].Bakiye * 17 * (int)gunFarki.Days) / (100 * 365)), 2, MidpointRounding.ToZero);
            //                        Console.WriteLine(bankaHesaplari[result].Bakiye);
            //                    }
            //                    else if (bankaHesaplari[result].HesapTuruDeger == HesapTuru.Ozel)
            //                    {
            //                        DateTime dateTimeSimdi = DateTime.Today;
            //                        TimeSpan gunFarki = dateTimeSimdi.Subtract(islemGecmisi[i].IslemTarihi);
            //                        bankaHesaplari[result].Bakiye = Math.Round(bankaHesaplari[result].Bakiye + ((bankaHesaplari[result].Bakiye * 10 * (int)gunFarki.Days) / (100 * 365)), 2, MidpointRounding.ToZero);
            //                        Console.WriteLine(bankaHesaplari[result].Bakiye);
            //                    }
            //                    break;
            //                case IslemTuru.ParaYatirma:

            //                    break;
            //                case IslemTuru.ParaCekme:
            //                    break;
            //                default:
            //                    break;
            //            }

            //        }

            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Hesap Bulunamadı!");
            //}
        }

        private bool HesapTuruOnay(string geciciHesapTuruDeger)
        {
            if (geciciHesapTuruDeger.Equals("1") || geciciHesapTuruDeger.Equals("2") || geciciHesapTuruDeger.Equals("3") || geciciHesapTuruDeger.Equals("4"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HesapBakiyeOnay(HesapTuru hesapTuruDeger, double geciciBakiye)
        {
            bool hesapOnayBool;
            switch ((int)hesapTuruDeger)
            {
                case 1:
                    hesapOnayBool = geciciBakiye >= 5000 ? true : false;
                    return hesapOnayBool;
                case 2:
                    hesapOnayBool = geciciBakiye >= 10000 ? true : false;
                    return hesapOnayBool;
                case 3:
                    hesapOnayBool = geciciBakiye >= 0 ? true : false;
                    return hesapOnayBool;
                case 4:
                    hesapOnayBool = geciciBakiye >= 0 ? true : false;
                    return hesapOnayBool;
                default:
                    return false;
            }
        }

        readonly string[] hesapTurleri = new string[] {
                "-> Kısa Vadeli Hesap Açma (1) (min 5.000 TL)",
                "-> Uzun Vadeli Hesap Açma (2) (min 10.000 TL)",
                "-> Özel Hesap Açma (3) (Minimum Limit Yok)",
                "-> Cari Hesap Açma (4) (Minimum Limit Yok)"
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

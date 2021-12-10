using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    class Banka
    {
        public Banka()
        {
            List<Hesap> bankaHesaplari = new List<Hesap>();
            List<Islem> IslemGecmisi = new List<Islem>();

            ArayuzGoster arayuzGoster = new ArayuzGoster();
            arayuzGoster.InitializeUI();
            Console.Write("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
            string islemDegeri = Console.ReadLine();

            Hesap hesap = new Hesap();
            Islem islem = new Islem();


            do
            {
                switch (islemDegeri)
                {
                    case "1":
                        // Hesap Açma İşlemi
                        bankaHesaplari.Add(hesap.HesapAc());
                        IslemGecmisi.Add(islem.HesapOlusturIslem(IslemGecmisi, bankaHesaplari));
                        IslemDekontuGoster(IslemGecmisi);
                        HesapGoster(bankaHesaplari);
                        Console.WriteLine("\n");
                        arayuzGoster.InitializeUI();
                        Console.Write("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                        break;
                    case "7":
                        // Hesap Listesi
                        HesapListesi(bankaHesaplari);
                        arayuzGoster.InitializeUI();
                        Console.Write("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                        break;
                    case "8":
                        // Hesap Durumu
                        Console.Write("Hesap No Giriniz : ");
                        string hesapNo = Console.ReadLine();
                        // Kar tutarı Hesaplar
                        hesap.KarTutari(HesapNoKontrol(hesapNo, bankaHesaplari), hesapNo, bankaHesaplari, IslemGecmisi);

                        // Hesabın son durumunu görüntüler
                        hesap.HesapDurum(HesapNoKontrol(hesapNo, bankaHesaplari), hesapNo, bankaHesaplari);
                        Console.WriteLine("\n");

                        arayuzGoster.InitializeUI();
                        Console.Write("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                        break;
                    case "q":
                        Console.WriteLine("Güle güle");
                        break;
                    default:
                        Console.WriteLine("\n\n");
                        Console.WriteLine("**********************");
                        Console.WriteLine("Geçersiz İşlem Değeri!");
                        Console.WriteLine("**********************");
                        Console.WriteLine("\n\n");
                        arayuzGoster.InitializeUI();
                        Console.Write("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                        break;
                }
                islemDegeri = Console.ReadLine();

            } while (islemDegeri != "q");
        }

        public bool HesapNoKontrol(string hesapNo, List<Hesap> bankaHesaplari)
        {
            {
                int result = bankaHesaplari.FindIndex(bankaHesabi => bankaHesabi.HesapNo == hesapNo);
                if (result != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ParaYatir(string hesapNo)
        {

        }

        public void ParaCek(string hesapNo) { }

        public void HesapListesi(List<Hesap> bankaHesaplari)
        {
            if (bankaHesaplari.Count > 0)
            {
                foreach (Hesap bankaHesabi in bankaHesaplari)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("********************************************");
                    Console.WriteLine($"Hesap Numarası : {bankaHesabi.HesapNo}");
                    Console.WriteLine($"İsim : {bankaHesabi.Isim}");
                    Console.WriteLine($"Soyisim : {bankaHesabi.Soyisim}");
                    Console.WriteLine($"Banka Hesap Türü : {bankaHesabi.HesapTuruDeger}");
                    Console.WriteLine($"Hesap Oluşturulma Tarihi : {bankaHesabi.OlusturulmaTarihi}");
                    Console.WriteLine($"Hesaba Aktarılacak Bakiye : {bankaHesabi.Bakiye}");
                    Console.WriteLine($"Çekiliş Hakkı : {bankaHesabi.CekilisHakki}");
                    Console.WriteLine("********************************************");
                }
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("******************************");
                Console.WriteLine("Sistemde Hesap Bulunmamaktadır");
                Console.WriteLine("******************************");
            }

        }

        public void HesapGoster(List<Hesap> bankaHesaplari)
        {
            for (int i = bankaHesaplari.Count; i > 0; i--)
            {
                Console.WriteLine("\n");
                Console.WriteLine("********************************************");
                Console.WriteLine($"Hesap Numarası : {bankaHesaplari[i - 1].HesapNo}");
                Console.WriteLine($"İsim : {bankaHesaplari[i - 1].Isim}");
                Console.WriteLine($"Soyisim : {bankaHesaplari[i - 1].Soyisim}");
                Console.WriteLine($"Banka Hesap Türü : {bankaHesaplari[i - 1].HesapTuruDeger}");
                Console.WriteLine($"Hesap Oluşturulma Tarihi : {bankaHesaplari[i - 1].OlusturulmaTarihi}");
                Console.WriteLine($"Hesaba Aktarılacak Bakiye : {bankaHesaplari[i - 1].Bakiye}");
                Console.WriteLine($"Çekiliş Hakkı : {bankaHesaplari[i - 1].CekilisHakki}");
                Console.WriteLine("********************************************");
                break;
            }
        }

        public void IslemDekontuGoster(List<Islem> islemGecmisi)
        {
            int dekontIndex = islemGecmisi.Count - 1;
            Console.WriteLine("\n");
            Console.WriteLine("********************************************");
            Console.WriteLine($"İşlem No : {islemGecmisi[dekontIndex].IslemNo}");
            Console.WriteLine($"Hesap No : {islemGecmisi[dekontIndex].HesapNo}");
            Console.WriteLine($"İşlem Tarihi : {islemGecmisi[dekontIndex].IslemTarihi}");
            Console.WriteLine($"İşlem Türü : {islemGecmisi[dekontIndex].IslemTuru}");
            Console.WriteLine($"Miktar (TL) : {islemGecmisi[dekontIndex].Miktar}");
            Console.WriteLine($"Önceki Bakiye (TL) : {islemGecmisi[dekontIndex].OncekiBakiye}");
            Console.WriteLine($"Sonraki Bakiye (TL) : {islemGecmisi[dekontIndex].SonrakiBakiye}");
            Console.WriteLine("********************************************");
            Console.WriteLine("\n");

        }

        public void Cekilis() { }



    }
}

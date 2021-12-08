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

            ArayuzGoster arayuzGoster = new ArayuzGoster();
            arayuzGoster.InitializeUI();
            Console.WriteLine("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
            string islemDegeri = Console.ReadLine();

            Hesap hesap = new Hesap();


            do
            {
                if (islemDegeri == "1")
                {
                    bankaHesaplari.Add(hesap.HesapAc());
                    arayuzGoster.InitializeUI();
                    Console.WriteLine("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                    islemDegeri = Console.ReadLine();
                }
                else if (islemDegeri == "7")
                {
                    HesapListesi(bankaHesaplari);
                    arayuzGoster.InitializeUI();
                    Console.WriteLine("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                    islemDegeri = Console.ReadLine();
                }
                else if (islemDegeri == "q")
                {
                    Console.WriteLine("Güle güle");
                }
                else
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("**********************");
                    Console.WriteLine("Geçersiz İşlem Değeri!");
                    Console.WriteLine("**********************");
                    Console.WriteLine("\n\n");
                    arayuzGoster.InitializeUI();
                    Console.WriteLine("Yapmak İstediğiniz İşlemi Yazınız (1-9) : ");
                    islemDegeri = Console.ReadLine();
                }

            } while (islemDegeri != "q");
        }




        public void ParaYatir()
        {
            //Convert.ToInt32(Math.Floor(hesap.Bakiye)) / 1000;
        }

        public void ParaCek() { }

        public void HesapListesi(List<Hesap> bankaHesaplari)
        {
            if (bankaHesaplari.Count > 0)
            {
                foreach (Hesap bankaHesabi in bankaHesaplari)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("***************");
                    Console.WriteLine($"Hesap Numarası : {bankaHesabi.HesapNo}");
                    Console.WriteLine($"İsim : {bankaHesabi.Isim}");
                    Console.WriteLine($"Soyisim : {bankaHesabi.Soyisim}");
                    Console.WriteLine($"Banka Hesap Türü : {bankaHesabi.HesapTuruDeger}");
                    Console.WriteLine($"Hesap Oluşturulma Tarihi : {bankaHesabi.OlusturulmaTarihi}");
                    Console.WriteLine($"Hesaba Aktarılacak Bakiye : {bankaHesabi.Bakiye}");
                    Console.WriteLine($"Çekiliş Hakkı : {bankaHesabi.CekilisHakki}");
                    Console.WriteLine("***************");
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

        public void Cekilis() { }



    }
}

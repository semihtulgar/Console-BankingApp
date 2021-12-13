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
                Console.Write("Tarih Giriniz (YIL,AY,GÜN) : ");
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
                Console.WriteLine("\tHesap Bulunamadı!");
                Console.WriteLine("************************************");
            }
        }

        private string HesapNoTanımla()
        {
            string hesapNo = "";
            for (int i = 0; i < 8; i++)
            {
                Random random = new Random();
                hesapNo += random.Next(0, 10);
            }
            return hesapNo;
        }

        public void KarTutari(bool hesapDurum, string hesapNo, List<Hesap> bankaHesaplari, List<Islem> islemGecmisi)
        {
            // İlgili hesap varsa hesaplamaya başla
            if (hesapDurum)
            {
                // İlgili Hesabın İşlem Geçmişini bulalım
                List<Islem> hesapIslemGecmisi = islemGecmisi.FindAll(hesap => hesap.HesapNo.Equals(hesapNo));

                // İlgili hesabın hesap türüne göre faiz hesaplanır
                HesapTuru hesapTuru = bankaHesaplari.Find(item => item.HesapNo == hesapNo).HesapTuruDeger;

                // İlgili hesabın hesap türüne göre sahip olduğu faiz oranını buluyoruz.
                float faizOrani = FaizOraniNedir(hesapTuru);

                double toplamBakiye = 0; // Para çekim , para yükleme işlemleri ve çekiliş işlemleri gerçekleştiren hesabın anapara ile birlikte elde ettiği faiz geliri.

                // Hesap üzerinden gerçekleştirilen işlemlerin sırasıyla tarihlerini aldık.
                List<DateTime> islemTarihleri = new List<DateTime>();
                hesapIslemGecmisi.ForEach(item => islemTarihleri.Add(item.IslemTarihi));

                int bankaHesabiIndex = bankaHesaplari.FindIndex(item => item.HesapNo == hesapNo);

                for (int i = 0; i < hesapIslemGecmisi.Count; i++)
                {
                    switch (hesapIslemGecmisi[i].IslemTuru)
                    {
                        case IslemTuru.HesapOlusturma:
                            if (islemTarihleri.Count == 1)
                            {
                                // Sadece banka hesabı oluşturulduysa ve işlenen faiz isteniyorsa
                                toplamBakiye += Math.Round(hesapIslemGecmisi[i].SonrakiBakiye + (hesapIslemGecmisi[i].SonrakiBakiye * (faizOrani / 365) * (DateTime.Today.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                            }
                            else
                            {
                                // Banka hesabı oluşturulmuş ve hesap üzerinden başka işlemler gerçekleştirilmişse
                                toplamBakiye += Math.Round(hesapIslemGecmisi[i].SonrakiBakiye + (hesapIslemGecmisi[i].SonrakiBakiye * (faizOrani / 365) * (DateTime.Today.Subtract(hesapIslemGecmisi[i + 1].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                            }
                            break;
                        case IslemTuru.ParaYatirma:
                            // Hesap oluşturma işlemi dışında para yatırma, para çekme veya çekiliş işlemleri de gerçekleştirildiyse
                            if (islemTarihleri.Count > 1)
                            {
                                // Yapılan işlem Son işlem değilse
                                if (islemTarihleri[islemTarihleri.Count - 1] != hesapIslemGecmisi[i].IslemTarihi)
                                {
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye += hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye += Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (hesapIslemGecmisi[i + 1].IslemTarihi.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                                else
                                {
                                    // Yapılan işlem son işlemse
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye += hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye = Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (DateTime.Today.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                            }
                            break;
                        case IslemTuru.ParaCekme:
                            // Hesap oluşturma işlemi dışında para yatırma, para çekme veya çekiliş işlemleri de gerçekleştirildiyse
                            if (islemTarihleri.Count > 1)
                            {
                                // Yapılan işlem Son işlem değilse
                                if (islemTarihleri[islemTarihleri.Count - 1] != hesapIslemGecmisi[i].IslemTarihi)
                                {
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye -= hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye = Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (hesapIslemGecmisi[i + 1].IslemTarihi.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                                else
                                {
                                    // Yapılan işlem son işlemse
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye -= hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye = Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (DateTime.Today.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                            }
                            break;
                        case IslemTuru.Cekilis:
                            // Hesap oluşturma işlemi dışında para yatırma, para çekme veya çekiliş işlemleri de gerçekleştirildiyse
                            if (islemTarihleri.Count > 1)
                            {
                                // Yapılan işlem Son işlem değilse
                                if (islemTarihleri[islemTarihleri.Count - 1] == hesapIslemGecmisi[i].IslemTarihi)
                                {
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye += hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye = Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (hesapIslemGecmisi[i + 1].IslemTarihi.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                                else
                                {
                                    // Yapılan işlem son işlemse
                                    // Öncelikle hesaba aktarılmak istenen miktarı aktar
                                    toplamBakiye += hesapIslemGecmisi[i].Miktar;
                                    // Hesaba aktarılan miktarla beraber faizi hesapla
                                    toplamBakiye = Math.Round(toplamBakiye + (toplamBakiye * (faizOrani / 365) * (DateTime.Today.Subtract(hesapIslemGecmisi[i].IslemTarihi)).Days), 2, MidpointRounding.ToZero);
                                    // Toplam bakiyesi hesaplanan miktarı hesaba aktar
                                    bankaHesaplari[bankaHesabiIndex].Bakiye = toplamBakiye;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                // Anapara ve çeşitli işlemlerle birlikte işlenen faizin hesaba yansıtılması

            }
        }

        // İlave Fonksiyonlar
        private float FaizOraniNedir(HesapTuru hesapTuru)
        {
            if (hesapTuru == HesapTuru.KısaVadeli)
            {
                return 0.15F;
            }
            else if (hesapTuru == HesapTuru.UzunVadeli)
            {
                return 0.17F;
            }
            else if (hesapTuru == HesapTuru.Ozel)
            {
                return 0.10F;
            }
            else
            {
                return 0;
            }
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

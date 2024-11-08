﻿namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public int HastaId { get; set; }
        public int DoktorId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string RandevuSaati { get; set; }
        public string RandevuSebebi { get; set; }
        public bool IptalEdildi { get; set; }


        public Hasta Hasta { get; set; }
        public Doktor Doktor { get; set; }
    }
}
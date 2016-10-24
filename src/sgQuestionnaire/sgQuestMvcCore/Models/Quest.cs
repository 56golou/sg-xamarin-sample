using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sgQuestMvcCore.Models
{
    public class Quest
    {
        [Display(Name = "名前")]
        public string name { get; set; }
        [Display(Name = "参加")]
        public int join { get; set; }
        [Display(Name = "星座")]
        public string horoscope { get; set; }
        [Display(Name = "メモ")]
        public string memo { get; set; }

        public static List<Horo> horoscopes
        {
            get
            {
                var Horos = new List<Horo>();
                Horos.Add(new Horo() { Id = "", Text = "選択してください" });
                Horos.Add(new Horo() { Id = "Aries", Text = "おひつじ座" });
                Horos.Add(new Horo() { Id = "Taurus", Text = "おうし座" });
                Horos.Add(new Horo() { Id = "Gemini", Text = "ふたご座" });
                Horos.Add(new Horo() { Id = "Cancer", Text = "かに座" });
                Horos.Add(new Horo() { Id = "Leo", Text = "しし座" });
                Horos.Add(new Horo() { Id = "Virgo", Text = "おとめ座" });
                Horos.Add(new Horo() { Id = "Libra", Text = "てんびん座" });
                Horos.Add(new Horo() { Id = "Scorpio", Text = "さそり座" });
                Horos.Add(new Horo() { Id = "Saggitarius", Text = "いて座" });
                Horos.Add(new Horo() { Id = "Capricorn", Text = "やぎ座" });
                Horos.Add(new Horo() { Id = "Aquarius", Text = "みずがめ座" });
                Horos.Add(new Horo() { Id = "Pisces", Text = "うお座" });
                return Horos;
            }
        } 
    }
    public class Horo
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}

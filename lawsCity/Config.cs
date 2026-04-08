using System.ComponentModel;

namespace lawsCity
{
    public class Config
    {
        [Description("Новый закон от мэра")]
        public string NewLaw { get; set; } = 
            "<size=50><b><color=yellow>⭐ НОВЫЙ ЗАКОН ОТ МЭРА ⭐</color></size>\n<size=25>(посмотреть в консоле на [Ё]  -  \".laws list\")</b></size>";
        
        [Description("Новый мэр")]
        public string NewMayor { get; set; } = 
            "<size=50><b><color=green>✅ ВЫ БЫЛИ НАЗНАЧЕНЫ МЭРОМ</color></size>\n<size=25>(пишите законы в консоле на [Ё]  -  \".laws add <закон>\")</b></size>";
        
        [Description("Больше не мэр")]
        public string OldMayor { get; set; } = 
            "<size=50><b><color=red>❌ ВЫ ПЕРЕСТАЛИ БЫТЬ МЭРОМ</color></size>\n<size=25>а не нужно было воровать...</b></size>";

        [Description("Вид верха списка законов")]
        public string LawsTop { get; set; } = 
            "\n\t<b><size=30><color=#F4A900>ЗАКОНЫ</color></size></b>\n\n%Laws%";
        
        [Description("Вид закона в списке законов")]
        public string LawsList { get; set; } =
            "<b><size=30><color=#FFCF40>%LawNumber%.</color></size> <size=25><color=#FADFAD>%Law%</color></size></b>\n";

        public string MayorId = null;

        public int LawsNumber = 0;
        public string Laws = null;
    }
}
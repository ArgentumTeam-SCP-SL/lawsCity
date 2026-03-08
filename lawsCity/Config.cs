using System.ComponentModel;

namespace lawsCity
{
    public class Config
    {
        [Description("Новый закон от мэра")]
        public string NewLaw { get; set; } = "<size=50><b><color=yellow>⭐ НОВЫЙ ЗАКОН ОТ МЭРА ⭐</color></size>\n<size=25>(посмотреть в консоле на [Ё]  -  \".laws list\")</b></size>";
        
        [Description("Новый мэр")]
        public string NewMayor { get; set; } = "<size=50><b><color=green>✅ ВЫ БЫЛИ НАЗНАЧЕНЫ МЭРОМ</color></size>\n<size=25>(пишите законы в консоле на [Ё]  -  \".laws add <закон>\")</b></size>";
        
        [Description("Больше не мэр")]
        public string OldMayor { get; set; } = "<size=50><b><color=red>❌ ВЫ ПЕРЕСТАЛИ БЫТЬ МЭРОМ</color></size>\n<size=25>а не нужно было воровать...</b></size>";
        
        public int MayorId = 0;

        public int LawsNumber = 0;
        public string Laws = string.Empty;
    }
}
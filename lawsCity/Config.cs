using System.ComponentModel;

namespace lawsCity
{
    public class Config
    {
        [Description("NEW LAW FROM THE MAYOR")]
        public string NewLaw { get; set; } = 
            "<size=50><b><color=yellow>⭐ NEW LAW FROM THE MAYOR ⭐</color></size>\n<size=25>(look at the console for [~]  -  \".laws list\")</b></size>";
        
        [Description("YOU HAVE BEEN APPOINTED MAYOR")]
        public string NewMayor { get; set; } = 
            "<size=50><b><color=green>✅ YOU HAVE BEEN APPOINTED MAYOR</color></size>\n<size=25>(write the laws in the console on [~]  -  \".laws add <law>\")</b></size>";
        
        [Description("YOU ARE NO LONGER MAYOR")]
        public string OldMayor { get; set; } = 
            "<size=50><b><color=red>❌ YOU ARE NO LONGER MAYOR</color></size>\n<size=25>corruption never leads to good things....</b></size>";

        [Description("View of the top of the list of laws")]
        public string LawsTop { get; set; } = 
            "\n\t<b><size=30><color=#F4A900>LAWS</color></size></b>\n\n%Laws%";
        
        [Description("Type of law in the list of laws")]
        public string LawsList { get; set; } =
            "<b><size=30><color=#FFCF40>%LawNumber%.</color></size> <size=25><color=#FADFAD>%Law%</color></size></b>\n";

        public string MayorId = null;

        public int LawsNumber = 0;
        public string Laws = null;
    }
}
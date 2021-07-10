using System.Text;

namespace NameBandit.Helpers
{
    public static class StringFormatHelper
    {       
        public static string RemoveAccents(this string text) {     
            StringBuilder sbReturn = new StringBuilder();     
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();  
            foreach (char letter in arrayText){     
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(letter) != System.Globalization.UnicodeCategory.NonSpacingMark)  
                    sbReturn.Append(letter);     
            }     
            return sbReturn.ToString();     
        } 
      
    }

}

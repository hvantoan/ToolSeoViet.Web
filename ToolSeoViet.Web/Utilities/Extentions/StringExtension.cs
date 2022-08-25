using System.Globalization;
using System.Linq;
using System.Text;

namespace TuanVu.Management.Web.Utilities.Extentions {

    public static class StringExtension {

        public static string UnsignedUnicode(this string text) {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            var chars = text.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
        public static string Base64Encode(this string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TatBlog.Services.Extensions;

public static class GeneSlugExtension
{
    public static string GenerateSlug(this string value)
    {
        string str = value.RemoveAccent().ToLower();

        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

        str = Regex.Replace(str, @"\s+", " ").Trim();

        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
        str = Regex.Replace(str, @"\s+", "-");
        return str;
    }

    private static string RemoveAccent(this string txt)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }
}

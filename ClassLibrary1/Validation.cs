using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary1
{
    public class Validation
    {
        // Валидация текста
        static public string textfild(string text)
        {
            if (text == "") return "Поле не может быть пустым";
            if (text.Length < 5) return "Поле не может быть меньше 5";
            for (int i = 0; i < text.Length; i++) 
            {
                if (text[i] == '-' || text[i] == '!' || text[i] == '.' || text[i] == '*' || text[i] == '^' || text[i] == '%' || text[i] == '$' || text[i] == '#' || text[i] == '@' || text[i] == '&' || text[i] == '_' || text[i] == '+' || text[i] == '=') return "В поле не может быть символов, кроме букв";
            }
            return "";
        }
        //Валидация цифр
        static public string numericfild(string num)
        {
            if (num == "") return "Поле не может быть пустым";
            for (int i = 0; i < num.Length; i++)
            {
                if (!char.IsDigit(num[i])) return "В числе не может быть букв";
            }
            int newNum = Convert.ToInt32(num);

            if (newNum <0) return "Число не может быть меньше 0";
            return "";
        }
    }
}

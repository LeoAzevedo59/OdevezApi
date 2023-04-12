using System;

namespace Odevez.Utils
{
    public static class DataUtils
    {
        public static string ObterMesString(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "jan";
                case 2:
                    return "fev";
                case 3:
                    return "mar";
                case 4:
                    return "abr";
                case 5:
                    return "mai";
                case 6:
                    return "jun";
                case 7:
                    return "jul";
                case 8:
                    return "ago";
                case 9:
                    return "set";
                case 10:
                    return "out";
                case 11:
                    return "nov";
                case 12:
                    return "dez";
                default:
                    return "";
            }
        }

        public static DateTime RetornaDataStringToDate(string data)
        {
            string data_ano = "";
            string data_mes = "";
            string data_dia = "";

            if (string.IsNullOrEmpty(data))
                return DateTime.Now;

            if (data.Length < 8)
                return DateTime.Now;

            data_ano = data.Substring(0, 4);
            data_mes = data.Substring(4, 2);
            data_dia = data.Substring(6, 2);

            return Convert.ToDateTime(string.Concat(data_ano, "/", data_mes, "/", data_dia));
        }

        public static DateTime ConvertStringToDate(string date)
        {
            var retorno = new DateTime();

            try
            {
                string data_ano = "";
                string data_mes = "";
                string data_dia = "";

                if (string.IsNullOrEmpty(date))
                    return DateTime.Now;

                var arrayDate = date.Split('/');

                data_ano = arrayDate[0];
                data_mes = arrayDate[1];
                data_dia = arrayDate[2];

                retorno = Convert.ToDateTime(string.Concat(data_ano, "/", data_mes, "/", data_dia));
            }
            catch (Exception ex) { }
            return retorno;
        }
    }
}

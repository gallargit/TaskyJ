using Newtonsoft.Json;
using STSdb4.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TaskyJ.STSDBServer
{
    public class STSDBMemoryHttpProcessor
    {
        public TcpClient socket;
        public STSDBMemoryHttpServer srv;

        private Stream inputStream;
        protected IStorageEngine engine;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public Dictionary<string, string> query_string;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();

        private const int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB
        private const int BUF_SIZE = 4096;

        public STSDBMemoryHttpProcessor(TcpClient socket, STSDBMemoryHttpServer srv, IStorageEngine engine)
        {
            this.socket = socket;
            this.srv = srv;
            this.engine = engine;
        }

        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }
        public void process()
        {
            inputStream = new BufferedStream(socket.GetStream());
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));
            try
            {
                parseRequest();
                readHeaders();
                if (http_method.Equals("GET"))
                {
                    handleGETRequest();
                }
                else if (http_method.Equals("POST"))
                {
                    handlePOSTRequest();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                writeFailure();
            }
            outputStream.Flush();
            inputStream = null;
            outputStream = null;
            socket.Close();
        }

        public void parseRequest()
        {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];

            if (http_url.IndexOf("?") > 2)
            {
                string qs = http_url.Substring(http_url.IndexOf("?") + 1);
                http_url = http_url.Substring(0, http_url.IndexOf("?"));
                query_string = Regex.Matches(qs, "([^?=&]+)(=([^&]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
            }
            else
            {
                query_string = new Dictionary<string, string>();
            }
            Console.WriteLine("starting: " + request);
        }

        public void readHeaders()
        {
            string line;
            while (!string.IsNullOrEmpty(line = streamReadLine(inputStream)))
            {
                if (!line.Equals(""))
                {
                    int separator = line.IndexOf(':');
                    if (separator == -1)
                    {
                        throw new Exception("invalid http header line: " + line);
                    }
                    String name = line.Substring(0, separator);
                    int pos = separator + 1;
                    while ((pos < line.Length) && (line[pos] == ' '))
                    {
                        pos++;
                    }
                    string value = line.Substring(pos, line.Length - pos);
                    httpHeaders[name] = value;
                }
            }
        }

        public void handleGETRequest()
        {
            srv.handleGETRequest(this);
        }

        public void handlePOSTRequest()
        {
            Console.WriteLine("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length"))
            {
                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                if (content_len > MAX_POST_SIZE)
                {
                    throw new Exception(String.Format("POST Content-Length({0}) too big for this simple server", content_len));
                }
                byte[] buf = new byte[BUF_SIZE];
                int to_read = content_len;
                while (to_read > 0)
                {
                    Console.WriteLine("starting Read, to_read={0}", to_read);

                    int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                    Console.WriteLine("read finished, numread={0}", numread);
                    if (numread == 0)
                    {
                        if (to_read == 0)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("client disconnected during post");
                        }
                    }
                    to_read -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            srv.handlePOSTRequest(this, new StreamReader(ms));
        }

        public void writeSuccess(string content_type = "text/html")
        {
            outputStream.WriteLine("HTTP/1.0 200 OK");
            outputStream.WriteLine("Content-Type: " + content_type);
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
        }

        public void writeFailure()
        {
            outputStream.WriteLine("HTTP/1.0 404 File not found");
            outputStream.WriteLine("Connection: close");
            outputStream.WriteLine("");
        }
    }

    public abstract class STSDBMemoryHttpServer
    {
        protected static int port;
        protected static TcpListener listener = null;
        protected static object lockListener = new object();
        protected bool is_active = true;
        protected static IStorageEngine engine;

        public STSDBMemoryHttpServer(int port)
        {
            STSDBMemoryHttpServer.port = port;
            STSDBMemoryHttpServer.engine = STSdb.FromMemory();
        }

        public STSDBMemoryHttpServer(int port, IStorageEngine engine)
        {
            STSDBMemoryHttpServer.port = port;
            STSDBMemoryHttpServer.engine = engine;
        }

        public void die()
        {
            is_active = false;
            if (listener != null)
            {
                lock (lockListener)
                {
                    if (listener != null)
                    {
                        listener.Stop();
                        listener = null;
                    }
                }
            }
        }

        public void listen()
        {
            bool alreadyRunning = true;
            if (listener == null)
            {
                //lock (lockListener)
                {
                    if (listener == null)
                    {
                        try
                        {
                            listener = new TcpListener(System.Net.IPAddress.Any, port);
                            listener.Start();
                            alreadyRunning = false;
                        }
                        catch (SocketException ex)
                        {
                            if (ex.ErrorCode != 10048)
                                throw;
                        }
                    }
                }
            }
            if (!alreadyRunning)
            {
                while (is_active)
                {
                    try
                    {
                        TcpClient s = listener.AcceptTcpClient();
                        STSDBMemoryHttpProcessor processor = new STSDBMemoryHttpProcessor(s, this, engine);
                        Thread processthread = new Thread(new ThreadStart(processor.process));
                        processthread.Start();
                        Thread.Sleep(1);
                    }
                    catch (SocketException)
                    {
                        is_active = false;
                    }
                }
            }
        }

        public abstract void handleGETRequest(STSDBMemoryHttpProcessor p);
        public abstract void handlePOSTRequest(STSDBMemoryHttpProcessor p, StreamReader inputData);
    }

    public class SimpleSTSDBMemoryHttpServer : STSDBMemoryHttpServer
    {
        private static int internalPort = 0;
        //thread-safe singleton
        private static SimpleSTSDBMemoryHttpServer instance;
        private static object syncRoot = new object();

        private SimpleSTSDBMemoryHttpServer(int port) : base(port, STSdb.FromMemory())
        {
        }

        private static SimpleSTSDBMemoryHttpServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SimpleSTSDBMemoryHttpServer(internalPort);
                    }
                }
                return instance;
            }
        }

        public static SimpleSTSDBMemoryHttpServer GetInstance(int port)
        {
            internalPort = port;
            return Instance;
        }

        public static SimpleSTSDBMemoryHttpServer GetInstance()
        {
            return Instance;
        }

        public void handleRequest(STSDBMemoryHttpProcessor p, Dictionary<string, string> data)
        {
            string contenttype = "text/html";
            StringBuilder putresult = new StringBuilder();
            string dbtable = null;
            data.TryGetValue("t", out dbtable);
            if (!string.IsNullOrEmpty(dbtable))
            {
                if (p.http_url.StartsWith("/put"))
                {
                    try
                    {
                        ITable<int, string> table = engine.OpenXTable<int, string>(dbtable);
                        foreach (var item in data.Where(x => x.Key == "q"))
                        {
                            var decoded = Uri.UnescapeDataString(item.Value).Replace("+", "<%2B>");

                            int idd = 0;
                            if (decoded.IndexOf("ID\":") > 2)
                            {
                                int indexstring = 4 + decoded.IndexOf("ID\":");
                                while (indexstring < decoded.Length)
                                {
                                    if (Char.IsNumber(decoded, indexstring))
                                    {
                                        idd = (idd * 10) + Convert.ToInt32(decoded.Substring(indexstring, 1));
                                        indexstring++;
                                    }
                                    else
                                    {
                                        indexstring = decoded.Length;
                                    }
                                }

                                if (idd == 0) //new item
                                {
                                    if (table.Count() > 0)
                                        idd = 1 + table.Max(x => x.Key);
                                    else
                                        idd = 1;
                                    decoded = decoded.Substring(0, decoded.IndexOf("ID\":") + 4) + idd.ToString() + decoded.Substring(indexstring - 1);
                                }

                            }
                            table[idd] = decoded;
                        }
                        engine.Commit();
                        putresult.Append("ok");
                    }
                    catch (Exception ex)
                    {
                        putresult.Append("Error: " + ex.ToString());
                    }
                }
                else if (p.http_url.Equals("/getall"))
                {
                    try
                    {
                        ITable<int, string> table = engine.OpenXTable<int, string>(dbtable);
                        List<string> lst = table.Select(x => x.Value).ToList();
                        contenttype = "application/json";
                        putresult.Append("[");
                        var firstItem = true;
                        foreach (var item in lst)
                        {
                            if (firstItem)
                                firstItem = false;
                            else
                                putresult.Append(",");
                            putresult.Append(item.Replace("<%2B>", "+"));
                        }
                        putresult.Append("]");
                    }
                    catch (Exception ex)
                    {
                        putresult.Append("Error: " + ex.ToString());
                    }
                }
                else if (p.http_url.Equals("/remove"))
                {
                    foreach (var item in data.Where(x => x.Key.ToUpper() == "ID"))
                    {
                        int idsearch = 0;
                        if (int.TryParse(item.Value, out idsearch))
                        {
                            ITable<int, string> table = engine.OpenXTable<int, string>(dbtable);
                            if (!table.Exists(idsearch))
                            {
                                putresult.Append("not found");
                            }
                            else
                            {
                                table.Delete(idsearch);
                                engine.Commit();
                                putresult.Append("ok");
                            }
                        }
                    }
                }
                else if (p.http_url.Equals("/count"))
                {
                    ITable<int, string> table = engine.OpenXTable<int, string>(dbtable);
                    putresult.Append(table.Count());
                }
                else if (p.http_url.Equals("/get"))
                {
                    foreach (var item in data.Where(x => x.Key.ToUpper() == "ID"))
                    {
                        int idsearch = 0;
                        contenttype = "application/json";
                        if (int.TryParse(item.Value, out idsearch))
                        {
                            ITable<int, string> table = engine.OpenXTable<int, string>(dbtable);
                            if (table.Exists(idsearch))
                            {
                                putresult.Append(table[idsearch]);
                            }
                        }
                    }
                }
            }
            if (p.http_url.Equals("/die"))
            {
                die();
            }
            p.writeSuccess(contenttype);
            p.outputStream.WriteLine(putresult.ToString());
        }

        public override void handleGETRequest(STSDBMemoryHttpProcessor p)
        {
            var data = new Dictionary<string, string>();
            foreach (var key in p.query_string.Keys)
            {
                data[key] = p.query_string[key];
            }
            handleRequest(p, data);
        }

        public override void handlePOSTRequest(STSDBMemoryHttpProcessor p, StreamReader inputData)
        {
            string postdata = inputData.ReadToEnd();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(postdata);
            handleRequest(p, data);
        }
    }
}

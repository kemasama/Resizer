using System.Text;

namespace Resizer
{
    public class ArgumentService
    {
        private string[] args;
        private Dictionary<string, string> arguments;
        public ArgumentService(string[] args)
        {
            this.args = args;
            this.arguments = new Dictionary<string, string>();
            this.Parse();
        }

        public string Get(string key)
        {
            if (this.arguments.TryGetValue(key, out string value))
            {
                return value;
            }

            return string.Empty;
        }

        private void Parse()
        {
            bool isGroup = false;
            string key = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int i = 0 ; i < args.Length ; i++)
            {
                string arg = args[i];
                if (!isGroup && arg.StartsWith("\"") && arg.EndsWith("\""))
                {
                    string v = arg.Substring(1, arg.Length - 2);
                    if (string.IsNullOrEmpty(key))
                    {
                        key = v;
                    }
                    else
                    {
                        this.arguments.Add(key, v);
                        key = string.Empty;
                    }
                }
                else if (isGroup && arg.EndsWith("\""))
                {
                    sb.AppendFormat(" {0}", arg.Substring(0, arg.Length - 1));
                    if (string.IsNullOrEmpty(key))
                    {
                        key = sb.ToString();
                    }
                    else
                    {
                        this.arguments.Add(key, sb.ToString());
                        key = string.Empty;
                    }

                    isGroup = false;
                }
                else if (!isGroup && arg.StartsWith("\""))
                {
                    sb.Clear();
                    sb.Append(arg.Substring(1, arg.Length - 2));
                    isGroup = true;
                }
                else if (arg.StartsWith("-"))
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        if (sb.Length > 0)
                        {
                            this.arguments.Add(key, sb.ToString());
                        }
                        else
                        {
                            this.arguments.Add(key, "true");
                        }
                    }

                    key = arg;
                }
                else
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        key = arg;
                    }
                    else
                    {
                        this.arguments.Add(key, arg);
                        key = string.Empty;
                    }
                }
            }
        }
    }
}
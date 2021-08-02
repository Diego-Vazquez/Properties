using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileProperties
{

    public class Properties
    {
        private string[] lines;
        private StreamWriter writer;
        private string file;
        private int index = 0;

        public Properties(string path)
        {
            try
            {

                if (Path.GetExtension(path).Equals(".properties"))
                {
                    this.file = path;
                }
                else
                {
                    throw new Exception("Extension file must be \".properties\"");
                }
            }
            catch (Exception)
            {
                throw;
            }   
            
        }

        public Properties(){}

        public void SetPropertie(string propertie, string value)
        {

            if (this.GetPropertie(propertie) == "null")
            {
                this.writer = File.AppendText(this.file);
                writer.WriteLine("{0} = {1}", propertie.Trim(), value.Trim()); ;
                writer.Close();

            }
            else
            {
                string text = File.ReadAllText(this.file);
                text = text.Replace(this.GetPropertie(propertie), value);
                File.WriteAllText(this.file, text);
            }

        }

        public string GetPropertie(string propertie)
        {
            try
            {
                lines = File.ReadAllLines(this.file);
                string value = "null";
                foreach (string line in lines)
                {
                    index++;
                    if (line.Split('=')[0].Trim().Equals(propertie))
                    {
                        if (countchars(line, '=') > 1 || countchars(line, '=') == 0)
                        {
                            throw new Exception("Exception in line " + index + " from your properties file");
                        }
                        else
                        {
                            value = line.Split('=')[1].Trim();
                            break;
                        }
                    }
                }
                return value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemovePropertie(string propertie)
        {
            lines = File.ReadAllLines(this.file);
            string replace = "";
            foreach (string a in lines)
            {
                if (a.Trim().StartsWith(propertie)) { replace = a; break; }
            }
            string text = File.ReadAllText(this.file);
            text = text.Replace(replace, string.Empty);
            File.WriteAllText(this.file, text);
            lines = File.ReadAllLines(this.file);
            File.WriteAllLines(this.file, lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());

        }

        public void Clear()
        {
            File.WriteAllText(this.file, string.Empty);
        }

        public string CreateFile(string path)
        {
            int idexception = 0;
            try
            {
                if (!File.Exists(path))
                {
                    if (Path.GetExtension(path).Equals(".properties"))
                    {
                        File.Create(path);
                        this.file = path;
                    }
                    else
                    {
                        idexception = 2;
                        throw new Exception("Extension file must be \".properties\"");
                    }

                }
                else
                {
                    idexception = 1;
                    throw new Exception("IOException source: the file already exists");
                }

            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception b)
            {
                switch (idexception)
                {
                    case 1: throw b;
                    case 2: throw b;
                }
            }
            return path;
        }

        public void UseOtherPropertiesFile(string path)
        {
            try
            {
                if (Path.GetExtension(path).Equals(".properties"))
                {
                    this.file = path;
                }
                else
                {
                    throw new Exception("Exception file must be \".properties\"");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private int countchars(string string_, char character)
        {
            int i = 0;
            char[] characters = string_.ToCharArray();
            foreach (char c in characters)
            {
                if (c == character)
                {
                    i++;
                }
            }
            return i;
        }

    }
}

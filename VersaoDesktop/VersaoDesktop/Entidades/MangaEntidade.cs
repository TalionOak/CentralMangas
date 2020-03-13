using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaoDesktop.Entidades
{
    public class MangaEntidade
    {
        public string Nome { get; set; }
        public Uri LinkPrimeiroCap { get; set; }

        public static MangaEntidade LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                MangaEntidade config = new MangaEntidade();
                config.SaveToFile(path);
                return null;
            }
            else
                using (var sr = new StreamReader(path))
                    return JsonConvert.DeserializeObject<MangaEntidade>(sr.ReadToEnd());
        }

        public void SaveToFile(string path)
        {
            using (var sw = new StreamWriter(path))
                sw.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}

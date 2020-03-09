using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
  public  class SeleniumConfigurations
    {
        public string CaminhoDriverChrome { get; set; } = @"C:\Selenium";
        public string UrlPagina { get; set; }
        public int Timeout { get; set; }
    }
}

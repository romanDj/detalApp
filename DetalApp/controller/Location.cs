using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetalApp.controller
{
    class Location
    {
        public static MainWindow _main;

        public static void render(string location = "Auth") {
            _main.Title = location;
            _main.getFrame().Source = new Uri("/pages/"+ location + ".xaml", UriKind.Relative);
        }

    }
}

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
        public static model.user userGlobal = null;
        public static int block_second = 0;

       

        public static void render(string location = "Auth") {
            _main.Title = location;
            _main.getFrame().Source = new Uri("/pages/"+ location + ".xaml", UriKind.Relative);
            tackingAction();
        }

        public static void tackingAction() {
            block_second = 0;
        }

        public static void protocolAction(string content)
        {
            model.detalDbEntities db = new model.detalDbEntities();

            model.Actions act = new model.Actions();
            act.id_user = userGlobal.id;
            act.time = DateTime.Now;
            act.action = content;
            db.Actions.Add(act);
            db.SaveChanges();
        }

    }
}

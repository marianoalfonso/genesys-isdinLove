using System;
using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



namespace isdinLove.clases
{
    class xlsWatcher
    {

        public static void monitorDirectory(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(@"A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox");
            watcher.NotifyFilter = (
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName);
        }

        //metodos que definen el comportamiento
        private static void onChange(object source, FileSystemEventArgs e)
        {
            WatcherChangeTypes changeTypes = e.ChangeType; //verifica que tipo de cambio se realiza

        }
}




}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.CodigoUsuario
{
    public class CuProceso
    {
        public void FinalizarArbolProcesos(string nombreProceso)
        {
            string comando = string.Format("SELECT * FROM Win32_Process WHERE Name = '{0}'", nombreProceso);
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(comando))
            {
                using (ManagementObjectCollection moc = searcher.Get())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        try
                        {
                            FinalizarArbolProcesos(Convert.ToInt32(mo["ProcessID"]));
                        }
                        catch { break; }
                    }
                }
            }
        }

        public void FinalizarArbolProcesos(int idProceso)
        {
            string comando = string.Format("SELECT * FROM Win32_Process Where ParentProcessID = {0}", idProceso);
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(comando))
            {
                using (ManagementObjectCollection moc = searcher.Get())
                {
                    foreach (ManagementObject mo in moc)
                    {
                        try
                        {
                            FinalizarArbolProcesos(Convert.ToInt32(mo["ProcessID"]));
                        }
                        catch { break; }
                    }
                }
            }

            try
            {
                using (Process proceso = Process.GetProcessById(idProceso))
                {
                    proceso.Kill();
                }
            }
            catch { }
        }
    }
}

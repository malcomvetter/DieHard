using System.Management;

namespace ProtectProcess
{
    public class WMI
    {
        public static void Run(object executable)
        {
            Run(executable as string);
        }
        public static string Run(string executable)
        {
            return Run("localhost", executable);
        }
        public static string Run(string host, string executable)
        {
            var scope = new ManagementScope(@"\\" + host + @"\root\CIMV2");
            var path = new ManagementPath("Win32_Process");
            var classInstance = new ManagementClass(scope, path, null);
            var startupSettings = new ManagementClass("Win32_ProcessStartup");
            startupSettings.Scope = scope;
            startupSettings["CreateFlags"] = 16777216;
            var inParams = classInstance.GetMethodParameters("Create");
            inParams["CommandLine"] = executable;
            inParams["ProcessStartupInformation"] = startupSettings;
            var outParams = classInstance.InvokeMethod("Create", inParams, null);
            var result = outParams["ReturnValue"].ToString();
            return result;
        }
    }
}

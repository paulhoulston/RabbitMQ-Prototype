using System.IO;
using System.Management.Automation;

namespace Integration.WebApi.SelfHosting.Events
{
    class ExecutePowerShellScript : IExecuteScripts
    {
        public void Execute(string scriptPath, dynamic data)
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                var script = File.ReadAllText(scriptPath);
                powerShellInstance.AddScript(script);
                powerShellInstance.AddParameter("data", data);
                powerShellInstance.Invoke();
            }
        }
    }
}
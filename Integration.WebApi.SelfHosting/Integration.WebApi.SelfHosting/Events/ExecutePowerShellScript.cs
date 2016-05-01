namespace Integration.WebApi.SelfHosting.Events
{
    class ExecutePowerShellScript : IExecuteScripts
    {
        public void Execute(string scriptPath, dynamic data)
        {
            System.Console.WriteLine("Executing script '{0}' with data '{1}'", scriptPath, Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }
    }
}
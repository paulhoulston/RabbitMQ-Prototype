namespace Integration.WebApi.SelfHosting.Events
{
    interface IExecuteScripts
    {
        void Execute(string scriptPath, dynamic data);
    }
}
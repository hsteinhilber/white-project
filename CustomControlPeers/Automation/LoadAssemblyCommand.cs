namespace White.CustomControls.Peers.Automation
{
    public class LoadAssemblyCommand : ICommand
    {
        private readonly string assemblyName;
        private readonly byte[] contents;
        private readonly CommandAssemblies commandAssemblies;
        private readonly IKnownTypeHolder knownTypeHolder;

        public LoadAssemblyCommand(string assemblyName, byte[] contents, CommandAssemblies commandAssemblies, IKnownTypeHolder knownTypeHolder)
        {
            this.assemblyName = assemblyName;
            this.contents = contents;
            this.commandAssemblies = commandAssemblies;
            this.knownTypeHolder = knownTypeHolder;
        }

        public virtual object Execute(object control)
        {
            commandAssemblies.Add(assemblyName, contents, knownTypeHolder);
            return null;
        }
    }
}
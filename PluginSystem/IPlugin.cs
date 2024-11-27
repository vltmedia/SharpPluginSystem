
namespace PluginSystem
{
    // IPlugin.cs
    public interface IPlugin
    {
        public string Name { get; }
        public string Id { get; }

        public void Initialize();
        public object ExecuteOperation(string operationName, Dictionary<string, object> args);
        public void Shutdown();
    }
    public interface IUpdatablePlugin
    {
        public void Update();
    }
    public abstract class BasePlugin<TInput>
    {
        public abstract string PluginId { get; }
        public abstract string PluginName { get; }
        public abstract string PluginDescription { get; }
        public abstract string PluginMetadata { get; }
        public abstract string PluginURL { get; }
        public abstract string PluginAuthor { get; }
        public abstract string PluginCategory { get; }
        public abstract string PluginPath { get; }
        public abstract FXPluginType PluginType { get; }

        public abstract List<string> InputNames { get; }
        public abstract List<string> OutputNames { get; }

        private readonly Dictionary<string, Func<TInput, object, object>> functions = new();

        /// <summary>
        /// Lifecycle method called when the plugin is enabled.
        /// </summary>
        public virtual void OnEnabled() { }

        /// <summary>
        /// Lifecycle method called when the plugin is disabled.
        /// </summary>
        public virtual void OnDisabled() { }

        /// <summary>
        /// Register a named function for the plugin's output.
        /// </summary>
        public void RegisterFunction(string outputName, Func<TInput, object, object> function)
        {
            if (!OutputNames.Contains(outputName))
            {
                throw new InvalidOperationException($"Output '{outputName}' is not defined for this plugin.");
            }

            functions[outputName] = function;
        }
        public virtual object[] Run(TInput inputs, object lastResult)
        {
            object[] items = new object[OutputNames.Count];
            return items;

        }

        /// <summary>
        /// Executes a specific function for the given output name.
        /// </summary>
        public object ExecuteFunction(string outputName, TInput inputs, object lastResult)
        {
            if (functions.TryGetValue(outputName, out var function))
            {
                return function(inputs, lastResult);
            }

            throw new InvalidOperationException($"No function registered for output '{outputName}'.");
        }
    }
}

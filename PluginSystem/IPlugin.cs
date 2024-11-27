
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

        private readonly Dictionary<string, Func<TInput, object, object>> syncFunctions = new();
        private readonly Dictionary<string, Func<TInput, object, Task<object>>> asyncFunctions = new();


        /// <summary>
        /// Register a synchronous function for the plugin's output.
        /// </summary>
        public void RegisterFunction(string outputName, Func<TInput, object, object> function)
        {
            if (!OutputNames.Contains(outputName))
            {
                throw new InvalidOperationException($"Output '{outputName}' is not defined for this plugin.");
            }

            syncFunctions[outputName] = function;
        }

        /// <summary>
        /// Register an asynchronous function for the plugin's output.
        /// </summary>
        public void RegisterFunction(string outputName, Func<TInput, object, Task<object>> function)
        {
            if (!OutputNames.Contains(outputName))
            {
                throw new InvalidOperationException($"Output '{outputName}' is not defined for this plugin.");
            }

            asyncFunctions[outputName] = function;
        }


        public virtual object[] Run(TInput inputs, object lastResult)
        {
            object[] items = new object[OutputNames.Count];
            return items;

        }
        public virtual Task<object[]> RunAsync(TInput inputs, object lastResult)
        {
            return Task.Run(() => Run(inputs, lastResult));
        }

        /// <summary>
        /// Executes a specific synchronous function for the given output name.
        /// </summary>
        public object ExecuteFunction(string outputName, TInput inputs, object lastResult)
        {
            if (syncFunctions.TryGetValue(outputName, out var function))
            {
                return function(inputs, lastResult);
            }

            throw new InvalidOperationException($"No synchronous function registered for output '{outputName}'.");
        }

        /// <summary>
        /// Executes a specific asynchronous function for the given output name.
        /// </summary>
        public async Task<object> ExecuteFunctionAsync(string outputName, TInput inputs, object lastResult)
        {
            if (asyncFunctions.TryGetValue(outputName, out var function))
            {
                return await function(inputs, lastResult);
            }

            if (syncFunctions.TryGetValue(outputName, out var syncFunction))
            {
                // Fallback to synchronous function
                return syncFunction(inputs, lastResult);
            }

            throw new InvalidOperationException($"No function registered for output '{outputName}'.");
        }
    }

    public class Plugin : BasePlugin<Dictionary<string, object>>
    {
        public override string PluginId => throw new NotImplementedException();

        public override string PluginName => throw new NotImplementedException();

        public override string PluginDescription => throw new NotImplementedException();

        public override string PluginMetadata => throw new NotImplementedException();

        public override string PluginURL => throw new NotImplementedException();

        public override string PluginAuthor => throw new NotImplementedException();

        public override string PluginCategory => throw new NotImplementedException();

        public override string PluginPath => throw new NotImplementedException();

        public override FXPluginType PluginType => throw new NotImplementedException();

        public override List<string> InputNames => throw new NotImplementedException();

        public override List<string> OutputNames => throw new NotImplementedException();
    }

}

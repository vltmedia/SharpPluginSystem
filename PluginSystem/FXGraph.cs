using PluginSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSystem
{


    public class FXGraph
    {
        private readonly Dictionary<string, FXGraphItem> graphItems = new Dictionary<string, FXGraphItem>();

        public void AddPlugin(BasePlugin<Dictionary<string, object>> plugin)
        {
            if (plugin.PluginType != FXPluginType.Sequential)
            {
                throw new InvalidOperationException($"Plugin '{plugin.PluginName}' is not capable of running in sequence.");
            }

            var item = new FXGraphItem(plugin);
            graphItems[item.UniqueId] = item;
        }

        public void RemovePlugin(string uniqueId)
        {
            graphItems.Remove(uniqueId);
        }

        public void ConnectNodes(string sourceNodeId, string sourceOutput, string targetNodeId, string targetInput)
        {
            if (!graphItems.ContainsKey(sourceNodeId) || !graphItems.ContainsKey(targetNodeId))
            {
                throw new InvalidOperationException("One or both nodes not found in the graph.");
            }

            var sourceNode = graphItems[sourceNodeId];
            var targetNode = graphItems[targetNodeId];

            sourceNode.OutputConnections[sourceOutput] = $"{targetNodeId}.{targetInput}";
            targetNode.InputConnections[targetInput] = $"{sourceNodeId}.{sourceOutput}";
        }

        public object RunFXGraph()
        {
            var results = new Dictionary<string, Dictionary<string, object>>(); // Stores results for each node

            foreach (var item in graphItems.Values)
            {
                // Resolve named inputs for the current node
                var inputs = ResolveInputs(item, results);

                // Execute only connected outputs
                foreach (var outputName in item.Plugin.OutputNames)
                {
                    if (item.OutputConnections.ContainsKey(outputName))
                    {
                        var result = item.RunOutput(outputName, inputs, results.LastOrDefault().Value);
                        results[item.UniqueId] ??= new Dictionary<string, object>();
                        results[item.UniqueId][outputName] = result;
                    }
                }
            }

            return results.LastOrDefault().Value; // Return the results of the last node
        }

        private Dictionary<string, object> ResolveInputs(FXGraphItem item, Dictionary<string, Dictionary<string, object>> results)
        {
            var inputs = new Dictionary<string, object>();

            foreach (var inputName in item.Plugin.InputNames)
            {
                if (item.InputConnections.TryGetValue(inputName, out var sourcePath))
                {
                    var parts = sourcePath.Split('.');
                    var sourceNodeId = parts[0];
                    var sourceOutputName = parts[1];

                    if (results.TryGetValue(sourceNodeId, out var sourceOutputs) &&
                        sourceOutputs.TryGetValue(sourceOutputName, out var value))
                    {
                        inputs[inputName] = value;
                    }
                }
            }

            return inputs;
        }
    }

    public class FXGraphItem
    {
        public string UniqueId { get; private set; }
        public string Name => Plugin.PluginName;

        public BasePlugin<Dictionary<string, object>> Plugin { get; }
        public Dictionary<string, string> InputConnections { get; } = new Dictionary<string, string>(); // Maps input name to source path
        public Dictionary<string, string> OutputConnections { get; } = new Dictionary<string, string>(); // Maps output name to target input

        public FXGraphItem(BasePlugin<Dictionary<string, object>> plugin)
        {
            Plugin = plugin;
            UniqueId = Guid.NewGuid().ToString(); // Generate a unique ID
        }

        public object RunOutput(string outputName, Dictionary<string, object> inputs, object lastResult)
        {
            // Execute the specific function for the given output
            return Plugin.ExecuteFunction(outputName, inputs, lastResult);
        }
    }
    public enum FXPluginType
    {
        Standalone,  // Runs individually
        Sequential   // Can be run in a sequence
    }
}

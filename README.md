
# Plugin System with Node-Based Workflow

## Overview

This plugin system provides a flexible and extensible architecture for defining and executing plugins. It supports named inputs and outputs, node-based workflows, and dynamic execution of specific functions. Designed for advanced use cases like FX graphs or modular applications, this system enables developers to build robust, reusable, and dynamic components.

## Features

- **Strongly-Typed Inputs and Outputs**: Each plugin can define named inputs and outputs with automatic mapping.
- **Dynamic Function Registration**: Register multiple output functions for plugins and execute them only when connected.
- **Node-Based Workflow**: Create and connect plugins dynamically in a graph-style system.
- **Single Plugin Execution**: Execute individual plugins with specified inputs and outputs.
- **Last Result Propagation**: Pass the output of one plugin to the next in sequence.
- **Save and Load Graphs**: Serialize and deserialize the plugin graph for reuse.
- **UI-Friendly Properties**: Includes positional and connection metadata for rendering plugins in a graphical node editor.

## Getting Started

### Installation

1. Clone the repository or download the source files.
2. Add the files to your project.

### Plugin Implementation

Create a custom plugin by subclassing `Plugin`:

```csharp
using PluginSystem;


public class ConsumerPlugin : Plugin
{
    public override string PluginId => "consumer_plugin";
    public override string PluginName => "Consumer Plugin";
    public override FXPluginType PluginType => FXPluginType.Sequential;

    public override List<string> InputNames => new List<string> { "Input" };
    public override List<string> OutputNames => new List<string> { "ProcessedOutput" };

    public ConsumerPlugin()
    {
        RegisterFunction("ProcessedOutput", (inputs, lastResult) =>
        {
            var input = Convert.ToSingle(inputs["Input"]);
            return input * 2; // Example processing
        });
    }
}
```

### Running a Plugin

Run a single plugin by creating an instance and setting its inputs:

```csharp
var mathPlugin = new MathPlugin();

var inputs = new Dictionary<string, object>
{
    { "A", 10f },
    { "B", 5f }
};

var sumResult = mathPlugin.ExecuteFunction("Sum", inputs, null);
Console.WriteLine($"Sum: {sumResult}");

var differenceResult = mathPlugin.ExecuteFunction("Difference", inputs, null);
Console.WriteLine($"Difference: {differenceResult}");
```

### Node-Based Workflow

1. Create an `FXGraph` and add plugins to it.
2. Connect plugins dynamically by specifying source and target nodes.
3. Run the graph to execute connected functions.

Example:

```csharp


static async Task Main()
    {
        var fxGraph = new FXGraph();

        // Create the MathPlugin
        var mathPlugin = new MathPlugin();

        // Add MathPlugin to the graph
        var mathNode = new FXGraphItem(mathPlugin);
        fxGraph.AddPlugin(mathPlugin);

        // Simulate an external consumer node (hypothetical)
        var consumerPlugin = new ConsumerPlugin(); // A plugin that consumes "Sum"
        fxGraph.AddPlugin(consumerPlugin);

        // Connect the "Sum" output of MathPlugin to the "Input" of ConsumerPlugin
        fxGraph.ConnectNodes(mathNode.UniqueId, "Sum", consumerPlugin.PluginId, "Input");

        // Define inputs for the MathPlugin
        var inputs = new Dictionary<string, object> { { "A", 10f }, { "B", 5f } };

        // Run the graph
        var result = await fxGraph.RunFXGraphAsync();

        Console.WriteLine($"Final Result: {result}");
    }
```

### Asynchronous Execution
Just incase you don't want your UI to freeze while executing a plugin, you can run the graph asynchronously.

```csharp
var mathPlugin = new MathPlugin();

var inputs = new Dictionary<string, object>
{
    { "A", 10f },
    { "B", 5f }
};

var sumResult = await mathPlugin.ExecuteFunctionAsync("Sum", inputs, null);
Console.WriteLine($"Sum: {sumResult}");
```


## Save and Load Graphs

### Save Graph

```csharp
fxGraph.SaveToFile("fxgraph.json");
```

### Load Graph

```csharp
var loadedGraph = FXGraph.LoadFromFile("fxgraph.json", availablePlugins);
```

## Contribution

Contributions are welcome! Feel free to submit issues, fork the repo, or create pull requests.

## License

This project is licensed under the Apache 2.0 License. See `LICENSE` for details.

## Contact

For questions or support, fill out an issue.

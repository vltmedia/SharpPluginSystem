
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

public class MathPlugin : Plugin
{
    public override string PluginId => "math_plugin";
    public override string PluginName => "Math Plugin";
    public override FXPluginType PluginType => FXPluginType.Sequential;

    public override List<string> InputNames => new List<string> { "A", "B" };
    public override List<string> OutputNames => new List<string> { "Sum", "Difference" };

    public MathPlugin()
    {
        RegisterFunction("Sum", (inputs, lastResult) =>
        {
            var a = Convert.ToSingle(inputs["A"]);
            var b = Convert.ToSingle(inputs["B"]);
            return a + b;
        });

        RegisterFunction("Difference", (inputs, lastResult) =>
        {
            var a = Convert.ToSingle(inputs["A"]);
            var b = Convert.ToSingle(inputs["B"]);
            return a - b;
        });
    }

    public override object[] Run(Dictionary<string, object> inputs, object lastResult)
    {
        throw new NotImplementedException("Use registered functions to execute specific outputs.");
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
var fxGraph = new FXGraph();

var mathPlugin = new MathPlugin();
fxGraph.AddPlugin(mathPlugin);

var greetingPlugin = new GreetingPlugin();
fxGraph.AddPlugin(greetingPlugin);

fxGraph.ConnectNodes(mathPlugin.PluginId, "Sum", greetingPlugin.PluginId, "Name");

var result = fxGraph.RunFXGraph();
Console.WriteLine($"Final Result: {result}");
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

using PluginSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginSystem;
namespace PluginSystem.Plugins.Basic
{

    public class MathPlugin : Plugin
    {
        public override string PluginId => "math_plugin_addsubtract";
        public override string PluginName => "Math Plugin";
        public override FXPluginType PluginType => FXPluginType.Sequential;

        public override List<string> InputNames => new List<string> { "A", "B" };
        public override List<string> OutputNames => new List<string> { "Sum", "Difference" , "Division", "Multiplication"};

        public override string PluginDescription => "Math plugin as an example";

        public override string PluginMetadata => "";

        public override string PluginURL => "";

        public override string PluginAuthor => "";

        public override string PluginCategory => "Math";
        public override string PluginPath => "Math/Add Subtract";

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

            RegisterFunction("Division", (inputs, lastResult) =>
            {
                var a = Convert.ToSingle(inputs["A"]);
                var b = Convert.ToSingle(inputs["B"]);
                return a / b;
            });

            RegisterFunction("Multiplication", (inputs, lastResult) =>
            {
                var a = Convert.ToSingle(inputs["A"]);
                var b = Convert.ToSingle(inputs["B"]);
                return a * b;
            });



        }

        public override object[] Run(Dictionary<string, object> inputs, object lastResult)
        {
            // Not used; we handle execution in specific functions.
            throw new NotImplementedException();
        }
    }


    // Make a Sin plugin
    public class MathSinPlugin : Plugin
    {
        public override string PluginId => "math_plugin_sin";
        public override string PluginName => "Sin Plugin";
        public override FXPluginType PluginType => FXPluginType.Sequential;

        public override List<string> InputNames => new List<string> { "Angle" };
        public override List<string> OutputNames => new List<string> { "Value" };

        public override string PluginDescription => "Sin plugin as an example";

        public override string PluginMetadata => "";

        public override string PluginURL => "";

        public override string PluginAuthor => "";

        public override string PluginCategory => "Math";
        public override string PluginPath => "Math/Sin";

        public MathSinPlugin()
        {
            RegisterFunction("Value", (inputs, lastResult) =>
            {
                var angle = Convert.ToSingle(inputs["Angle"]);
                return Math.Sin(angle);
            });
        }

        public override object[] Run(Dictionary<string, object> inputs, object lastResult)
        {
            // Not used; we handle execution in specific functions.
            throw new NotImplementedException();
        }
    }

    // Make a Cos plugin
    public class MathCosPlugin : Plugin
    {
        public override string PluginId => "math_plugin_cos";
        public override string PluginName => "Cos Plugin";
        public override FXPluginType PluginType => FXPluginType.Sequential;

        public override List<string> InputNames => new List<string> { "Angle" };
        public override List<string> OutputNames => new List<string> { "Value" };

        public override string PluginDescription => "Cos plugin as an example";

        public override string PluginMetadata => "";

        public override string PluginURL => "";

        public override string PluginAuthor => "";

        public override string PluginCategory => "Math";
        public override string PluginPath => "Math/Cos";

        public MathCosPlugin()
        {
            RegisterFunction("Value", (inputs, lastResult) =>
            {
                var angle = Convert.ToSingle(inputs["Angle"]);
                return Math.Cos(angle);
            });
        }

        public override object[] Run(Dictionary<string, object> inputs, object lastResult)
        {
            // Not used; we handle execution in specific functions.
            throw new NotImplementedException();
        }
    }

    // Make a Tan plugin
    public class MathTanPlugin : Plugin
    {
        public override string PluginId => "math_plugin_tan";
        public override string PluginName => "Tan Plugin";
        public override FXPluginType PluginType => FXPluginType.Sequential;

        public override List<string> InputNames => new List<string> { "Angle" };
        public override List<string> OutputNames => new List<string> { "Value" };

        public override string PluginDescription => "Tan plugin as an example";

        public override string PluginMetadata => "";

        public override string PluginURL => "";

        public override string PluginAuthor => "";

        public override string PluginCategory => "Math";
        public override string PluginPath => "Math/Tan";

        public MathTanPlugin()
        {
            RegisterFunction("Value", (inputs, lastResult) =>
            {
                var angle = Convert.ToSingle(inputs["Angle"]);
                return Math.Tan(angle);
            });
        }

        public override object[] Run(Dictionary<string, object> inputs, object lastResult)
        {
            // Not used; we handle execution in specific functions.
            throw new NotImplementedException();
        }
    }
}

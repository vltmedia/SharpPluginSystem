using PluginSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BravoCMSLibrary.PluginSystem
{

    public class PluginManager 
    {
        private List<IPlugin> plugins = new List<IPlugin>();

        public void Initialize()
        {
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            // Search for all types implementing IPlugin in the current assembly
            var pluginTypes = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in pluginTypes)
            {
                try
                {
                    // Create an instance of the plugin and initialize it
                    var plugin = (IPlugin)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                    plugin.Initialize();
                    Console.WriteLine($"Loaded plugin: {plugin.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load plugin {type.Name}: {ex.Message}");
                }
            }
        }

        public object ExecutePluginOperation(string pluginId, string operationName, Dictionary<string, object> args)
        {
            var plugin = plugins.FirstOrDefault(p => p.Id == pluginId || p.Name == pluginId);
            if (plugin == null)
            {
                Console.WriteLine($"Plugin with ID or Name '{pluginId}' not found.");
                return null;
            }

            try
            {
                return plugin.ExecuteOperation(operationName, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute operation '{operationName}' in plugin '{pluginId}': {ex.Message}");
                return null;
            }
        }

        private void OnApplicationQuit()
        {
            foreach (var plugin in plugins)
            {
                plugin.Shutdown();
            }
        }
    }
}

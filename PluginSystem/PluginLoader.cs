
using System.Reflection;


namespace PluginSystem
{
    // PluginLoader.cs


    public class PluginLoader
    {
        protected List<IPlugin> loadedPlugins = new List<IPlugin>();
        protected List<IUpdatablePlugin> activePlugins = new List<IUpdatablePlugin>();

        /// <summary>
        /// Load all plugins implementing IPlugin in the current assembly.
        /// </summary>
        public void LoadPlugins()
        {
            var pluginTypes = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in pluginTypes)
            {
                try
                {
                    var plugin = (IPlugin)Activator.CreateInstance(type);
                    loadedPlugins.Add(plugin);
                    plugin.Initialize();
                    Console.WriteLine($"Loaded plugin: {plugin.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load plugin {type.Name}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Activate a plugin for periodic updates.
        /// </summary>
        public void ActivatePlugin(string pluginId)
        {
            var plugin = GetPlugin(pluginId);
            if (plugin is IUpdatablePlugin updatablePlugin && !activePlugins.Contains(updatablePlugin))
            {
                activePlugins.Add(updatablePlugin);
                Console.WriteLine($"Activated plugin for updates: {pluginId}");
            }
        }

        /// <summary>
        /// Deactivate a plugin from periodic updates.
        /// </summary>
        public void DeactivatePlugin(string pluginId)
        {
            var plugin = GetPlugin(pluginId);
            if (plugin is IUpdatablePlugin updatablePlugin && activePlugins.Contains(updatablePlugin))
            {
                activePlugins.Remove(updatablePlugin);
                Console.WriteLine($"Deactivated plugin from updates: {pluginId}");
            }
        }

        /// <summary>
        /// Execute an operation on a specific plugin.
        /// </summary>
        public object ExecutePluginOperation(string pluginId, string operationName, Dictionary<string, object> args)
        {
            var plugin = GetPlugin(pluginId);
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
                Console.WriteLine($"Error executing operation '{operationName}' on plugin '{pluginId}': {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Periodically update active plugins.
        /// </summary>
        public void Update()
        {
            foreach (var plugin in activePlugins)
            {
                plugin.Update();
            }
        }

        /// <summary>
        /// Shut down all loaded plugins.
        /// </summary>
        public void ShutdownAllPlugins()
        {
            foreach (var plugin in loadedPlugins)
            {
                plugin.Shutdown();
            }

            loadedPlugins.Clear();
            activePlugins.Clear();
        }

        /// <summary>
        /// Get a plugin by its ID or name.
        /// </summary>
        private IPlugin GetPlugin(string pluginId)
        {
            return loadedPlugins.FirstOrDefault(p => p.Id == pluginId || p.Name == pluginId);
        }
    }


}
